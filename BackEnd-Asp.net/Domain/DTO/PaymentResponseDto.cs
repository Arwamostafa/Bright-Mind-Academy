using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class PaymentResponseDto
    {
        public string TransactionId { get; set; }

        public string PaymentUrl { get; set; } = "";
    }
}
