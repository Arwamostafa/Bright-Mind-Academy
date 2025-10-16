using Domain.DTO;
using Domain.Models;
using E_LearningPlatform.Helper;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository;
using Service.Services.Contract;
using Service.Services.Implementation;
using System.IO;         
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly ISubjectService subjectService;

        //private readonly ILogger<OrdersController> _logger;
        public PaymentsController(AppDbContext context, IPaymentService paymentService, IConfiguration config)
        {
            _paymentService = paymentService;
            _context = context;
            _config = config;
            this.subjectService = subjectService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto dto)
        {
            try
            {
                var result = await _paymentService.CreatePaymentAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("callback")]
        public async Task<IActionResult> PaymentCallback()
        {
            PaymentPageInfoDto paymentPageInfoDto = new PaymentPageInfoDto();

            var query = Request.Query;
            string[] fields = new[]
              {
            "amount_cents","created_at","currency","error_occured","has_parent_transaction","id","integration_id","is_3d_secure","is_auth","is_capture","is_refunded","is_standalone_payment","is_voided","order","owner","pending","source_data.pan","source_data.sub_type","source_data.type","success"
              };
            var stringConcate = new StringBuilder();
            //var result = await _paymentService.HandlePaymentCallbackAsync(callbackData);
            foreach (var field in fields)
            {
                if (query.TryGetValue(field, out var value))
                {
                    stringConcate.Append(value);
                }
                else
                {
                    return BadRequest($"Missing :{field}");
                }

            }

            string HmacRecived = query["hmac"];
            string calsulatedHmac = _paymentService.computeHmacSha256(stringConcate.ToString(), _config["Paymob:HmacSecret"]);

            if (HmacRecived.Equals(calsulatedHmac, StringComparison.OrdinalIgnoreCase))
            {
                bool.TryParse(query["success"], out bool Success);
                var TransactionId = query["order"];
                var errorCode = query["data.message"].ToString();
                var paymentDate = query["created_at"];

                var paymentMethod = query["source_data.type"];
                string reason = errorCode switch
                {
                    "ERR_PAYMENT_DECLINED" => "Insufficient Funds",
                    "SEC_AUTH_FAILED" => "3D Secure Verification Failed",
                    _ => "Unknown Error"
                };
                decimal amountInEgp = 0;
                if (decimal.TryParse(query["amount_cents"], out decimal amountCents))
                {
                    amountInEgp = amountCents / 100;
                }
                paymentPageInfoDto = new PaymentPageInfoDto
                {
                    id = TransactionId,
                    AmountCents = amountInEgp,
                    DateTime = paymentDate,
                    ErrorCode = errorCode,
                    reason = reason,
                    paymentMethod = paymentMethod,

                };
                if (Success)
                {
                    await _paymentService.UpdatepaymentSuccess(TransactionId, amountInEgp);
                    return Content(HtmlGenerator.GenerateSuccessHtml(paymentPageInfoDto), "text/html");
                }

                await _paymentService.UpdatepaymentFaild(TransactionId, amountInEgp);
                return Content(HtmlGenerator.GenerateFailedHtml(paymentPageInfoDto), "text/html");
            }

            return Content(HtmlGenerator.GenerateSecurityHtml(paymentPageInfoDto), "text/html");
            //if (result.StartsWith("Error"))
            //    return BadRequest(new { message = result });

            //return Ok(new { message = result });
        }


        [HttpGet("DownloadReceipt")]
        public async Task<IActionResult> DownloadReceipt(string receiptId)
        {

            var payment = await _context.SubjectStudents.Include(s => s.Subject).Include(s => s.Student).ThenInclude(s => s.User).FirstOrDefaultAsync(p => p.TransactionId == receiptId);

            if (payment == null)
            {
                return NotFound("Receipt not found.");
            }
            var studentName = payment.Student.User.FirstName + " " + payment.Student.User.LastName;
            var amount = payment.Amount;
            var date = payment.PaymentDate.ToString();

            // إنشاء Stream مؤقت
            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                // محتوى الفاتورة
                document.Add(new Paragraph("E-Learning Platform Receipt").SimulateBold().SetFontSize(18));
                document.Add(new Paragraph($"Receipt ID: {receiptId}"));
                document.Add(new Paragraph($"Student Name: {studentName}"));
                document.Add(new Paragraph($"Amount Paid: {amount:C}"));
                document.Add(new Paragraph($"Date: {date}"));
                document.Add(new Paragraph("Thank you for your payment!").SetFontSize(12));

                document.Close();

                var fileBytes = stream.ToArray();
                return File(fileBytes, "application/pdf", $"Receipt_{receiptId}.pdf");
            }
        }


        [HttpGet("GetAllPayments")]

        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPayments();
            return Ok(payments);


        }

        [HttpGet("GetPaymentDetalisBYTransactionId")]

        public async Task<IActionResult> GetPaymentDetalis(string transactionId)
        {
            var payment = await _paymentService.GetPaymentDetailsAsync(transactionId);
            if (payment == null)
                return NotFound("Payment not found.");
            return Ok(payment);
        }


        [HttpGet("GetPaymentDetalisBYStudentIdAndSubjectId")]
        public async Task<IActionResult> GetPaymentDetalis(int studentId, int subjectId)
        {
            var payment = await _paymentService.GetPaymentsByStudentIdAndSubjectId(studentId, subjectId);
            if (payment == null)
                return NotFound("Payment not found.");
            return Ok(payment);
        }

        [HttpGet("NumberOfStudentsEnrolledInSubject")]
        public async Task<IActionResult> NumberOfStudentsEnrolledInSubject(int subjectId)
        {
            var count = await _paymentService.NumberOfStudentInSubject(subjectId);
            return Ok(count);
        }


    }
    
}
