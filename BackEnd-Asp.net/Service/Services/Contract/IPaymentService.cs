using Domain.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Contract
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> CreatePaymentAsync(CreatePaymentDto dto);
        Task<string> HandlePaymentCallbackAsync(dynamic callbackData);
        public string computeHmacSha256(string message, string secret);
        Task<SubjectStudent> UpdatepaymentSuccess(string specialRefrence, decimal AmountPaid);
        Task<SubjectStudent> UpdatepaymentFaild(string specialRefrence, decimal AmountPaid);
        Task<PaymentDTO> GetPaymentDetailsAsync(string transactionId);

        Task<List<PaymentDTO>> GetAllPayments();

        Task<PaymentDTO> GetPaymentsByStudentIdAndSubjectId(int studentId, int SubjectId);

        Task<int> NumberOfStudentInSubject(int subjectId);




    }
}
