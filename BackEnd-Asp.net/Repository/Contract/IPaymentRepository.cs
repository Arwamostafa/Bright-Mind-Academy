using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contract
{
    public interface IPaymentRepository
    {
        Task AddPayment(SubjectStudent subjectStudent);
        Task<SubjectStudent?> GetPaymentsDetailsByTransactionId(string transactionId);
        Task <IEnumerable<SubjectStudent>> GetAllPayments();

        Task<SubjectStudent?> GetPaymentByStudentIdAndSubjectId(int studentId, int subjectId);

        Task <int> NumberOfStudentInSubject(int subjectId);
        Task UpdatePaymentAsync(SubjectStudent subjectStudent);

        Task SaveAsync();

    }
}
