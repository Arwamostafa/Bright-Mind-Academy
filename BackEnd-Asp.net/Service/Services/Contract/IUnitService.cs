using Domain.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Contract
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitDto>> GetAllAsync();
        Task<UnitWithSubjectAndLessonsDto?> GetByIdAsync(int id);

        Task AddAsync(UnitCreateDto UnitDto);

        Task Update(UnitCreateDto UnitDto, int id);

        Task Delete(int id);
        Task<UnitWithSubjectAndLessonsDto> GetUnitByLessonId(int lessonId);
        Task<List<Unit>> GetUnitsBySubjectId(int subjectId);

        Task<List<Unit>> GetUnitsBySubjectName(string subjectname);

        Task <int> GetNumberOfUnitsBySubjectId(int subjectId);

        //public Task<UnitDto?> GetByIdWithSubjectAsync(int id);
    }
}