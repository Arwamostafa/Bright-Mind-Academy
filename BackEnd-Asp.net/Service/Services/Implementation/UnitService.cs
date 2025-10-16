using Domain.DTO;
using Domain.Models;
using Repository.Repositories.Implementations;
using Repository.Contract;
using Service.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Implementation
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ILessonRepository _IlessonRepository;

        public UnitService(IUnitRepository unitRepository, ILessonRepository IlessonRepository)
        {
            _unitRepository = unitRepository;
            _IlessonRepository = IlessonRepository;
        }
        public async Task AddAsync(UnitCreateDto unitCreateDto)
        {
            Unit unit = new Unit
            {
                Id = unitCreateDto.Id
            ,
                Title = unitCreateDto.Title
            ,
                Description = unitCreateDto.Description,
                SubjectId = unitCreateDto.SubjectId,
            };
            await _unitRepository.AddAsync(unit);
            await _unitRepository.SaveAsync();
        }



        public async Task<IEnumerable<UnitDto>> GetAllAsync()
        {
            var unites = await _unitRepository.GetAllAsync();
            return unites.Select(MapToDto).ToList();

        }


        public async Task<UnitWithSubjectAndLessonsDto?> GetByIdAsync(int id)
        {
            Unit unit = await _unitRepository.GetAsync(id);

            if (unit == null)
                throw new Exception($"Unit with id {id} not found");

            return MapToDtoAll(unit);
        }

        public async Task Delete(int id)
        {
            Unit unit = await _unitRepository.GetAsync(id);
            if (unit == null)
                throw new Exception($"unit with id {id} not found");
            _unitRepository.Delete(unit);
            await _unitRepository.SaveAsync();

        }

        public async Task Update(UnitCreateDto UnitDto, int id)
        {
            Unit unit = await _unitRepository.GetAsync(id);
            if (unit == null)
                throw new Exception($"unit with id {id} not found");
            unit.Title = UnitDto.Title;
            unit.Description = UnitDto.Description;
            unit.SubjectId = UnitDto.SubjectId;
            _unitRepository.Update(unit);
            await _unitRepository.SaveAsync();
        }


        public async Task<UnitWithSubjectAndLessonsDto> GetUnitByLessonId(int lessonId)
        {
            var lesson = await _IlessonRepository.GetAsync(lessonId);
            if (lesson == null || lesson.Unit == null)
                throw new Exception($"No unit found for lesson ID {lessonId}");

            return MapToDtoAll(lesson.Unit);
        }

        public Task<List<Unit>> GetUnitsBySubjectId(int subjectId)
        {

            return _unitRepository.GetUnitsBySubjectId(subjectId);
        }

        public Task<List<Unit>> GetUnitsBySubjectName(string subjectname)
        {
            return _unitRepository.GetUnitsBySubjectName(subjectname);
        }



public async Task<int> GetNumberOfUnitsBySubjectId(int subjectId)
        {
            return await _unitRepository.GetUnitsCountBySubjectId(subjectId);
        }

        private UnitDto MapToDto(Unit unit)
        {
            return new UnitDto
            {
                Id = unit.Id,
                Title = unit.Title,
                Description = unit.Description,
                SubjectId = unit.SubjectId,
                SubjectName = unit.Subject?.SubjectName,


            };
        }


        private UnitWithSubjectAndLessonsDto MapToDtoAll(Unit unit)
        {
            return new UnitWithSubjectAndLessonsDto
            {
                Id = unit.Id,
                Title = unit.Title,
                Description = unit.Description,
                SubjectId = unit.SubjectId,
                SubjectName = unit.Subject?.SubjectName,
                Lessons = unit.Lessons?.Select(lesson => new LessonDto
                {
                    Id = lesson.Id,
                    Title = lesson.Title,
                    Description = lesson.Description,
                    VideoUrl = lesson.VideoUrl,
                    PdfUrl = lesson.PdfUrl,
                    AssigmentUrl = lesson.AssigmentUrl,
                    AssigmentDeadLine = lesson.AssigmentDeadLine,
                    UnitId = lesson.UnitId,
                    UnitName = unit.Title
                }).ToList() ?? new List<LessonDto>()


            };
        }

        
    }

}