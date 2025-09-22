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
//        public async Task<List<Student>> GetAllAsync()
//        {
//            return await _appDbContext.Students.ToListAsync();
//        }

//        public async Task<Student> GetByIdAsync(int id)
//        {
//            return await _appDbContext.Students.FindAsync(id);
//        }

//        public async Task AddAsync(Student student)
//        {
//            await _appDbContext.AddAsync(student);
//            await _appDbContext.SaveChangesAsync();
//        }

//        public async Task UpdateAsync(Student student)
//        {
//            _appDbContext.Students.Update(student);
//            await _appDbContext.SaveChangesAsync();
//        }

//        public async Task DeleteAsync(int id)
//        {
//            var student = await _appDbContext.Students.FindAsync(id);
//            if (student != null)
//            {
//                _appDbContext.Students.Remove(student);
//                await _appDbContext.SaveChangesAsync();
//            }
//        }
//        public async Task EnrollStudentInSubjectAsync(int studentId, int subjectId)
//        {
//            var enrollment = new SubjectStudent
//            {
//                StudentId = studentId,
//                SubjectId = subjectId
//            };

//            _appDbContext.SubjectStudents.Add(enrollment);
//            await _appDbContext.SaveChangesAsync();
//        }

//        public async Task<Payment> GetPaymentBySubjectAndStudent(int subjectId, int studentId)
//        {
//          return   await _appDbContext.Payments.Include(p => p.Subject).Include(p => p.Student).FirstOrDefaultAsync(p => p.SubjectID == subjectId && p.StudentID == studentId);
             
//        }
//    }

//    }
