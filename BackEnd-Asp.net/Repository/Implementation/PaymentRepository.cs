using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class PaymentRepository : IPaymentRepository
    {

        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context )
        {
            _context = context;
        }
        public async Task AddPayment(SubjectStudent subjectStudent)
        {
           await  _context.SubjectStudents.AddAsync(subjectStudent);
        }

      

        public async Task<IEnumerable<SubjectStudent>> GetAllPayments()
        {
            return await _context.SubjectStudents
                .Include(ps => ps.Student)
                .ThenInclude(s => s.User)
                .Include(ps => ps.Subject)
                .ToListAsync();
        }

       

        public async Task<SubjectStudent?> GetPaymentByStudentIdAndSubjectId(int studentId, int subjectId)
        {
           return  await _context.SubjectStudents
               .Include(ps => ps.Student)
               .ThenInclude(s => s.User)
               .Include(ps => ps.Subject)
               .ThenInclude(sub => sub.Instructor)
               .ThenInclude(i => i.User)
               .Where(ps => ps.StudentId == studentId && ps.SubjectId == subjectId)
               .FirstOrDefaultAsync();

        }

        public async Task<SubjectStudent?> GetPaymentsDetailsByTransactionId(string transactionId)
        {
            return await _context.SubjectStudents
                .Include(ss => ss.Student).ThenInclude(s => s.User)
                .Include(ss => ss.Subject)
                .ThenInclude(sub => sub.Instructor).ThenInclude(i => i.User).FirstOrDefaultAsync(ss => ss.TransactionId == transactionId);
        }

        public async Task<int> NumberOfStudentInSubject(int subjectId)
        {
            return await _context.SubjectStudents
                .CountAsync(ss => ss.SubjectId == subjectId && ss.IsPaid);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        

        public async Task UpdatePaymentAsync(SubjectStudent subjectStudent)
        {
            var ExistingPayment = await _context.SubjectStudents.FirstOrDefaultAsync(ps => ps.StudentId == subjectStudent.StudentId && ps.SubjectId == subjectStudent.SubjectId);

          if(ExistingPayment==null)
                throw new KeyNotFoundException("Payment record not found.");

          var properties = typeof(SubjectStudent).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(subjectStudent);
                if (newValue != null && !Equals(newValue, property.GetValue(ExistingPayment)))
                {
                    property.SetValue(ExistingPayment, newValue);
                }
            }
            _context.SubjectStudents.Update(ExistingPayment);
            await _context.SaveChangesAsync();
        }
    }
}
