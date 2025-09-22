using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Repository.Repositories.Implementations;
using Repository.Contract;
using Service.Services.Contract;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Implementation
{
    public class LessonService : ILessonService
    {

        public LessonService(ILessonRepository IlessonRepository, IUnitRepository unitRepository)
        {
            _IlessonRepository = IlessonRepository;
            _IUnitRepository = unitRepository;
        }

        private readonly ILessonRepository _IlessonRepository;
        private readonly IUnitRepository _IUnitRepository;

        public async Task<IEnumerable<LessonDto>> GetAllAsync()
        {
            var lessons = await _IlessonRepository.GetAllAsync();
            return lessons.Select(MapToLessonDto).ToList();
        }


        public async Task<List<string>> SaveFileAsync(IFormFile zipFile, string folderName)
        {
            if (zipFile == null || zipFile.Length == 0 || Path.GetExtension(zipFile.FileName).ToLower() != ".zip")
                return new List<string>();

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string zipPath = Path.Combine(uploadsFolder, Guid.NewGuid().ToString() + ".zip");

            using (var stream = new FileStream(zipPath, FileMode.Create))
            {
                await zipFile.CopyToAsync(stream);
            }

            List<string> allowedUrls = new List<string>();
            string extractPath = Path.Combine(uploadsFolder, Guid.NewGuid().ToString());

            ZipFile.ExtractToDirectory(zipPath, extractPath);

            var allowedExtensions = new[] { ".pdf", ".mp4", ".mov", ".avi", ".mkv", ".png", ".jpg", ".jpeg" };

            foreach (var file in Directory.GetFiles(extractPath))
            {
                var ext = Path.GetExtension(file).ToLower();
                if (allowedExtensions.Contains(ext))
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + ext;
                    string finalPath = Path.Combine(uploadsFolder, uniqueFileName);
                    File.Move(file, finalPath);

                    string baseUrl = "https://localhost:7092";
                    string url = $"{baseUrl}/{folderName}/{uniqueFileName}";
                    allowedUrls.Add(url);
                }
            }

            File.Delete(zipPath);

            if (Directory.Exists(extractPath))
            {
                Directory.Delete(extractPath, true);
            }



            return allowedUrls;
        }

        public async Task AddAsync(LessonCreateDto lessonCreateDto)
        {
            List<string> videoUrls = new List<string>();
            List<string> pdfUrls = new List<string>();
            List<string> assignmentUrls = new List<string>();
            if (lessonCreateDto.VideoUrl != null)
            {
                var files = await SaveFileAsync(lessonCreateDto.VideoUrl, "Uploads/Videos");
                videoUrls.AddRange(files.Where(url => url.EndsWith(".mp4")));
            }
            if (lessonCreateDto.PdfUrl != null)
            {
                var files = await SaveFileAsync(lessonCreateDto.PdfUrl, "Uploads/Pdfs");
                pdfUrls.AddRange(files.Where(url => url.EndsWith(".pdf")));
            }

            if (lessonCreateDto.AssigmentUrl != null)
            {
                var files = await SaveFileAsync(lessonCreateDto.AssigmentUrl, "Uploads/Assignments");
                assignmentUrls.AddRange(files.Where(url => url.EndsWith(".pdf") || url.EndsWith(".docx")));
            }

            Lesson lesson = new Lesson
            {
                Id = lessonCreateDto.Id,
                Title = lessonCreateDto.Title,
                Description = lessonCreateDto.Description,
                UnitId = lessonCreateDto.UnitId,

                VideoUrl = videoUrls.FirstOrDefault(),
                AssigmentDeadLine = lessonCreateDto.AssigmentDeadLine,
                AssigmentUrl = assignmentUrls.FirstOrDefault(),
                PdfUrl = pdfUrls.FirstOrDefault(),




            };
            await _IlessonRepository.AddAsync(lesson);
            await _IlessonRepository.SaveAsync();
        }

        public async Task Delete(int id)
        {

            Lesson? lesson = await _IlessonRepository.GetAsync(id);
            if (lesson == null)
                throw new Exception($"Lesson with id {id} not found");
            _IlessonRepository.Delete(lesson);
            await _IlessonRepository.SaveAsync();
        }


        public async Task<LessonDto?> GetByIdAsync(int id)
        {

            Lesson? lesson = await _IlessonRepository.GetAsync(id);
            if (lesson == null)
                throw new Exception($"Lesson with id {id} not found");

            return MapToLessonDto(lesson);
        }



        public async Task Update(LessonCreateDto lessonCreateDto, int id)
        {
            List<string> videoUrls = new List<string>();
            List<string> pdfUrls = new List<string>();
            List<string> assignmentUrls = new List<string>();
            if (lessonCreateDto.VideoUrl != null)
            {
                var files = await SaveFileAsync(lessonCreateDto.VideoUrl, "Uploads/Videos");
                videoUrls.AddRange(files.Where(url => url.EndsWith(".mp4")));
            }
            if (lessonCreateDto.PdfUrl != null)
            {
                var files = await SaveFileAsync(lessonCreateDto.PdfUrl, "Uploads/Pdfs");
                pdfUrls.AddRange(files.Where(url => url.EndsWith(".pdf")));
            }

            if (lessonCreateDto.AssigmentUrl != null)
            {
                var files = await SaveFileAsync(lessonCreateDto.AssigmentUrl, "Uploads/Assignments");
                assignmentUrls.AddRange(files.Where(url => url.EndsWith(".pdf") || url.EndsWith(".docx")));
            }

            Lesson? lesson = await _IlessonRepository.GetAsync(id);
            if (lesson == null)
                throw new Exception($"Lesson with id {id} not found");
            lesson.Title = lessonCreateDto.Title;
            lesson.Description = lessonCreateDto.Description;
            lesson.AssigmentDeadLine = lessonCreateDto.AssigmentDeadLine;
            lesson.PdfUrl = pdfUrls.FirstOrDefault();
            lesson.AssigmentUrl = assignmentUrls.FirstOrDefault();
            lesson.VideoUrl = videoUrls.FirstOrDefault();
            lesson.UnitId = lessonCreateDto.UnitId;

            _IlessonRepository.Update(lesson);
            await _IlessonRepository.SaveAsync();

        }




        public Task<IEnumerable<Lesson>> GetLessonsByUnitId(int unitId)
        {
            return _IlessonRepository.GetLessonsByUnitId(unitId);
        }

        public Task<IEnumerable<Lesson>> GetLessonsByUnitName(string unitname)
        {
            return _IlessonRepository.GetLessonsByUnitName(unitname);
        }

        private LessonDto MapToLessonDto(Lesson lesson)
        {

            return new LessonDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                UnitId = lesson.UnitId,
                VideoUrl = lesson.VideoUrl,
                AssigmentDeadLine = lesson.AssigmentDeadLine,
                AssigmentUrl = lesson.AssigmentUrl,
                PdfUrl = lesson.PdfUrl,
                UnitName = lesson?.Unit?.Title ?? "no unit "
            };
        }



    }
}