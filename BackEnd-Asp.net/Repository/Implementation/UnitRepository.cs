using Domain.DTO;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Implementations
{
    public class UnitRepository : IUnitRepository
    {
        private readonly AppDbContext _appDbContext;

        public UnitRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<IEnumerable<Unit>> GetAllAsync()
        {
            return await _appDbContext.Units.Include(u => u.Subject).ToListAsync();
        }



        public async Task<Unit?> GetAsync(int id)
        {
            return await _appDbContext.Units
    .Include(u => u.Subject)
    .Include(u => u.Lessons)
    .FirstOrDefaultAsync(u => u.Id == id);
        }



        public async Task AddAsync(Unit entity)
        {
            await _appDbContext.Units.AddAsync(entity);
        }


        public void Delete(Unit entity)
        {
            _appDbContext.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public void Update(Unit entity)
        {
            _appDbContext.Update(entity);
        }
        public async Task<Unit?> GetByIdWithSubjectAsync(int id)
        {
            return await _appDbContext.Units
                .Include(u => u.Subject)
                .FirstOrDefaultAsync(u => u.Id == id);

        }


        public async Task<Unit?> GetUnitByLessonId(int lessonId)
        {
            return await _appDbContext.Lessons
       .Where(l => l.Id == lessonId)
       .Select(l => l.Unit)
       .FirstOrDefaultAsync();
        }

        public async Task<List<Unit>> GetUnitsBySubjectId(int subjectId)
        {
            return await _appDbContext.Subjects
                .Where(s => s.SubjectID == subjectId)
                .SelectMany(s => s.Units)
                .ToListAsync();
        }

        public async Task<List<Unit>> GetUnitsBySubjectName(string subjectname)
        {
            return await _appDbContext.Subjects
                .Where(s => s.SubjectName == subjectname)
                .SelectMany(s => s.Units)
                .ToListAsync();
        }

        public async Task<int> GetUnitsCountBySubjectId(int subjectId)
        {
            return await _appDbContext.Units
                .CountAsync(u => u.SubjectId == subjectId);
        }

        //public async Task<Unit> GetUnitWithLessonsAsync(int unitId)
        //{
        //    return await _appDbContext.Units
        //.Include(u => u.Lessons)
        //.FirstOrDefaultAsync(u => u.Id == unitId);
        //}




    }
}