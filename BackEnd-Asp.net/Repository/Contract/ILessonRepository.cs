using Domain.Models;
using Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contract
{
    public interface ILessonRepository
    {
        Task<IEnumerable<Lesson>> GetAllAsync();
        Task<Lesson?> GetAsync(int id);

        Task AddAsync(Lesson entity);

        void Update(Lesson entity);

        void Delete(Lesson lesson);

        Task SaveAsync();
        public Task<IEnumerable<Lesson>> GetLessonsByUnitId(int unitId);

        public Task<IEnumerable<Lesson>> GetLessonsByUnitName(string unitname);

    }
}