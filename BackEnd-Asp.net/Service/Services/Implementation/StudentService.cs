//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Domain.DTO;
//using Domain.Models;
//using Repository;
//using Repository.Contract;
//using Service.Services.Contract;

//namespace Service.Services.Implementation
//{
//    public class StudentService : IStudentService
//    {
//        private readonly IStudentRepository _repository;

//        public StudentService(IStudentRepository repository)
//        {
//            _repository = repository;
//        }

//        public async Task<List<Student>> GetAllAsync()
//        {
//            return await _repository.GetAllAsync();
//        }

//        public async Task<Student> GetByIdAsync(int id)
//        {
//            return await _repository.GetByIdAsync(id);
//        }

//        public async Task AddAsync(Student student)
//        {
//            await _repository.AddAsync(student);
//        }

//        public async Task UpdateAsync(Student student)
//        {
//            await _repository.UpdateAsync(student);
//        }

//        public async Task DeleteAsync(int id)
//        {
//            await _repository.DeleteAsync(id);
//        }
//        public async Task EnrollStudentInSubjectAsync(int studentId, int subjectId)
//        {
//            await _repository.EnrollStudentInSubjectAsync(studentId, subjectId);
//        }
//        public async Task<PaymentDTO> GetPaymentBySubjectAndStudent(int subjectId, int studentId)
//        {

//          var payment=   await _repository.GetPaymentBySubjectAndStudent(subjectId, studentId);

//            if (payment == null)
//                return null;

//            return new PaymentDTO
//            {
//                PaymentID = payment.PaymentID,
//                TotalPayMent = payment.TotalPayMent,

//                StudentID = payment.StudentID
//                ,
//                StudentName = payment.Student?.FirstName + " " + payment.Student?.LastName,
//                SubjectID = payment.SubjectID,
//                SubjectName = payment.Subject?.SubjectName,
//                InstructorId = payment.Subject.InstructorID,
//                InstructorName = payment.Subject.Instructor?.Name
//            };
//        }
//    }
//}
