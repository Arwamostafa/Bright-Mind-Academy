using Domain.DTO;
using Domain.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contract
{
    public interface IUnitRepository
    {
        Task<IEnumerable<Unit>> GetAllAsync();
        Task<Unit?> GetAsync(int id);

        Task AddAsync(Unit entity);

        void Update(Unit entity);

        void Delete(Unit entity);

        Task SaveAsync();
        public Task<Unit?> GetUnitByLessonId(int lessonId);
        public Task<List<Unit>> GetUnitsBySubjectId(int Subjectid);
        public Task<List<Unit>> GetUnitsBySubjectName(string subjectname);
    }
}