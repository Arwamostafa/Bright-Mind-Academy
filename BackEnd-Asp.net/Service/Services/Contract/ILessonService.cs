using Domain.Models;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Service.Services.Contract
{
    public interface ILessonService
    {
        Task<IEnumerable<LessonDto>> GetAllAsync();
        Task<LessonDto?> GetByIdAsync(int id);

        Task<List<string>> SaveFileAsync(IFormFile zipFile, string folderName);

        Task AddAsync(LessonCreateDto lessonDto );

        Task Update(LessonCreateDto lessonDto, int id);

        public Task<IEnumerable<Lesson>> GetLessonsByUnitId(int unitId);

        public Task<IEnumerable<Lesson>> GetLessonsByUnitName(string unitname);
       
        Task Delete(int id);


    }
}
