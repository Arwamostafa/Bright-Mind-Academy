//using Domain.Models;
//using Microsoft.EntityFrameworkCore;
//using Repository.Contract;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Repository.Implementation
//{
//    public class StudentRepository : IStudentRepository
//    {

//        private readonly AppDbContext _appDbContext;

//        public StudentRepository(AppDbContext appDbContext)
//        {
//            _appDbContext = appDbContext;
//        }
//        public async Task<List<StudentProfile>> GetAllAsync()
//        {
//            return await _appDbContext.StudentProfiles.Include(s=>s.User).ToListAsync();
//        }

//        public async Task<StudentProfile> GetByIdAsync(int id)
//        {
//            return await _appDbContext.StudentProfiles.Include(s=>s.User).FirstOrDefaultAsync(s=>s.UserId==id);
//        }

//        public async Task AddAsync(StudentProfile student)
//        {
//            await _appDbContext.StudentProfiles.AddAsync(student);
//            await _appDbContext.SaveChangesAsync();
//        }

//        public async Task UpdateAsync(StudentProfile student)
//        {
//            _appDbContext.StudentProfiles.Update(student);
//            await _appDbContext.SaveChangesAsync();
//        }

//        public async Task DeleteAsync(int id)
//        {
//            var student = await _appDbContext.StudentProfiles.FindAsync(id);
//            if (student != null)
//            {
//                _appDbContext.StudentProfiles.Remove(student);
//                await _appDbContext.SaveChangesAsync();
//            }
//        }
//    }

//}
