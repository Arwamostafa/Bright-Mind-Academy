using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class PaymentPageInfoDto
    {
        public  string  id { get; set; }

        public string ErrorCode { get; set; }

        public string reason { get; set; }

        public string DateTime { get; set; }

        public decimal AmountCents { get; set; }

        public string paymentMethod { get; set; }
    }
}
