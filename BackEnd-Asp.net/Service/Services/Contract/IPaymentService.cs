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
        Task<PaymentDTO> CreateOrUpdatePaymentAsync(int subjectId, int studentId);



    }
}
