using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryImplementation
{
    public class LessonRepository : ILessonRepository
    {
        private readonly AppDbContext _Context;
        public LessonRepository(AppDbContext context)
        {
            _Context = context;
        }


        public async Task<IEnumerable<Lesson>> GetAllAsync()
        {
            return await _Context.Lessons.Include(l => l.Unit).ToListAsync();
        }


        public async Task AddAsync(Lesson entity)
        {
            await _Context.Lessons.AddAsync(entity);
        }


        public async Task<Lesson?> GetAsync(int id)
        {
            return await _Context.Lessons.Include(l => l.Unit).FirstOrDefaultAsync(l => l.Id == id);
        }

        public void Delete(Lesson entity)
        {
            _Context.Remove(entity);
        }


        public async Task SaveAsync()
        {
            await _Context.SaveChangesAsync();
        }

        public void Update(Lesson entity)
        {
            _Context.Lessons.Update(entity);

        }

        public async Task<IEnumerable<Lesson>> GetLessonsByUnitId(int unitId)
        {
            return await _Context.Lessons
                .Where(l => l.UnitId == unitId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Lesson>> GetLessonsByUnitName(string unitname)
        {
            return await _Context.Lessons
                .Where(l => l.Unit.Title == unitname)
                .ToListAsync();
        }


    }
}