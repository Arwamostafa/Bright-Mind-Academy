using Domain.DTO;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Repository;
using Repository.Contract;
using Repository.Implementation;
using Service.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Implementation
{
    public class PaymentService : IPaymentService
        //: IPaymentService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly ISubjectRepository subjectRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly HttpClient _httpClient;

        public PaymentService(AppDbContext context, IConfiguration config , ISubjectRepository subjectRepository , IPaymentRepository paymentRepository)
        {
            _context = context;
            _config = config;
            this.subjectRepository = subjectRepository;
            _paymentRepository = paymentRepository;
            _httpClient = new HttpClient { BaseAddress = new Uri("https://accept.paymob.com/api/") };

        }

        public async Task<PaymentResponseDto> CreatePaymentAsync(CreatePaymentDto dto)
        {
            Console.WriteLine(dto == null ? "DTO is null" : "DTO is not null");

            var student = await _context.StudentProfiles.Include(s=>s.User).FirstOrDefaultAsync(s=>s.UserId==dto.StudentId);
            var subject = subjectRepository.GetById(dto.SubjectId);
            //var subject = await _context.Subjects.FindAsync(dto.SubjectId);

            if (student == null || subject == null)
                throw new Exception("Student or Subject not found.");

            var existingRecord = await _context.SubjectStudents.FirstOrDefaultAsync(ss => ss.StudentId == dto.StudentId && ss.SubjectId == dto.SubjectId);

            if (existingRecord != null)
                throw new Exception("Student is already enrolled in this subject.");

            var subjectStudent = new SubjectStudent
            {
                StudentId = dto.StudentId,
                SubjectId = dto.SubjectId,
                IsPaid = false
            };
           await _paymentRepository.AddPayment(subjectStudent);
            //_context.SubjectStudents.Add(subjectStudent);
          await  _paymentRepository.SaveAsync();
            //await _context.SaveChangesAsync();

            var authBody = new { api_key = _config["Paymob:ApiKey"] };
            var authResponse = await _httpClient.PostAsync(
                "auth/tokens",
                new StringContent(JsonConvert.SerializeObject(authBody), Encoding.UTF8, "application/json")
            );

            if (!authResponse.IsSuccessStatusCode)
                throw new Exception("Failed to authenticate with Paymob.");

            var authJson = await authResponse.Content.ReadAsStringAsync();
            dynamic authData = JsonConvert.DeserializeObject(authJson);
            string authToken = authData.token;

            var orderRequest = new
            {
                auth_token = authToken,
                delivery_needed = "false",
                amount_cents = ((int)(subject.Price * 100)).ToString(),
                currency = "EGP",
                shipping_data = new
                {
                    apartment = "NA",
                    floor = "NA",
                    building = "NA",
                    street = student.User.Address ?? "N/A",
                    city = "Cairo",
                    country = "EG",
                    email = student.User.Email ?? "test@example.com",
                    first_name = student.User.FirstName ?? "N/A",
                    last_name = student.User.LastName ?? "N/A",
                    phone_number = student.User.PhoneNumber ?? "01000000000",
                    postal_code = "NA",
                    state = "NA"
                },
                items = new[]
                {
            new
            {
                name = subject.SubjectName ?? "Course",
                amount_cents = (int)(subject.Price * 100),
                quantity = 1
            }
        }
            };

            var orderResponse = await _httpClient.PostAsync(
                "ecommerce/orders",
                new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json")
            );

            if (!orderResponse.IsSuccessStatusCode)
            {
                var err = await orderResponse.Content.ReadAsStringAsync();
                throw new Exception($"Order registration failed: {err}");
            }

            var orderJson = await orderResponse.Content.ReadAsStringAsync();
            dynamic orderData = JsonConvert.DeserializeObject(orderJson);
            int orderId = orderData.id;

            var paymentKeyRequest = new
            {
                auth_token = authToken,
                amount_cents = (int)(subject.Price * 100),
                expiration = 3600,
                order_id = orderId,
                billing_data = new
                {
                    apartment = "NA",
                    floor = "NA",
                    building = "NA",
                    street = student.User.Address ?? "N/A",
                    city = "Cairo",
                    country = "EG",
                    email = student.User.Email ?? "test@example.com",
                    first_name = student.User.FirstName ?? "N/A",
                    last_name = student.User.LastName ?? "N/A",
                    phone_number = student.User.PhoneNumber ?? "01000000000",
                    postal_code = "NA",
                    state = "NA"
                },
                currency = "EGP",
                integration_id = int.Parse(_config["Paymob:IntegrationId"])
            };

            var paymentKeyResponse = await _httpClient.PostAsync(
                "acceptance/payment_keys",
                new StringContent(JsonConvert.SerializeObject(paymentKeyRequest), Encoding.UTF8, "application/json")
            );

            if (!paymentKeyResponse.IsSuccessStatusCode)
            {
                var err = await paymentKeyResponse.Content.ReadAsStringAsync();
                throw new Exception($"Payment key request failed: {err}");
            }

            var paymentKeyJson = await paymentKeyResponse.Content.ReadAsStringAsync();
            dynamic paymentKeyData = JsonConvert.DeserializeObject(paymentKeyJson);
            string paymentToken = paymentKeyData.token;

            // 7️⃣ حفظ Transaction
            subjectStudent.TransactionId = orderId.ToString();
           await  _paymentRepository.UpdatePaymentAsync(subjectStudent);

            // 8️⃣ إنشاء iframe URL
            string iframeUrl = $"https://accept.paymob.com/api/acceptance/iframes/{_config["Paymob:IframeId"]}?payment_token={paymentToken}";

            return new PaymentResponseDto
            {
                PaymentUrl = iframeUrl,
                TransactionId = orderId.ToString()
            };
        }


        public async Task<string> HandlePaymentCallbackAsync(dynamic callbackData)
        {
            try
            {
                // ✅ 1. استخراج البيانات من الـ callback JSON
                string transactionId = callbackData?.obj?.id;
                bool isSuccess = callbackData?.obj?.success ?? false;
                string orderId = callbackData?.obj?.order?.id;

                if (string.IsNullOrEmpty(orderId))
                    return "Order ID is missing in callback data.";

                // ✅ 2. لو العملية فشلت
                if (!isSuccess)
                    return "Payment failed or was cancelled.";

                // ✅ 3. البحث عن الـ SubjectStudent المرتبط بالـ Order ID
                var subjectStudent = await _context.SubjectStudents
                    .FirstOrDefaultAsync(ss => ss.TransactionId == orderId);

                if (subjectStudent == null)
                    return $"No SubjectStudent found for order ID: {orderId}";

                // ✅ 4. تحديث البيانات بعد نجاح الدفع
                subjectStudent.IsPaid = true;
                subjectStudent.PaymentDate = DateTime.Now;
                subjectStudent.TransactionId = transactionId;

                await _context.SaveChangesAsync();

                return "Payment confirmed and data updated successfully.";
            }
            catch (Exception ex)
            {
                return $"Error while processing callback: {ex.Message}";
            }
        }


        public string computeHmacSha256(string message, string secret)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secret);
            var messageBytes = Encoding.UTF8.GetBytes(message);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                var hash = hmac.ComputeHash(messageBytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public async Task<SubjectStudent> UpdatepaymentSuccess(string specialRefrence, decimal AmountPaid)
        {
            var subjectStudent = await _paymentRepository.GetPaymentsDetailsByTransactionId(specialRefrence);
            if (subjectStudent == null)
                throw new KeyNotFoundException("No SubjectStudent found for the provided reference.");

            subjectStudent.IsPaid = true;
            subjectStudent.PaymentDate = DateTime.Now;
            subjectStudent.Amount = AmountPaid;
            await _paymentRepository.UpdatePaymentAsync(subjectStudent);
            return subjectStudent;
        }
        public async Task<SubjectStudent> UpdatepaymentFaild(string specialRefrence, decimal AmountPaid)
        {
            var subjectStudent = await _context.SubjectStudents.FirstOrDefaultAsync(ss => ss.TransactionId == specialRefrence);
            if (subjectStudent == null)
                throw new KeyNotFoundException("No SubjectStudent found for the provided reference.");

            subjectStudent.IsPaid = false;
            subjectStudent.PaymentDate = DateTime.Now;
            subjectStudent.Amount = AmountPaid;
            await _paymentRepository.UpdatePaymentAsync(subjectStudent);
            return subjectStudent;
        }


        public async Task<PaymentDTO> GetPaymentDetailsAsync(string transactionId)
        {
            var subjectStudent = await _paymentRepository.GetPaymentsDetailsByTransactionId(transactionId);
            if (subjectStudent == null)
                throw new KeyNotFoundException("No SubjectStudent found for the provided transaction ID.");
            return new PaymentDTO
            {
                TransactionId = subjectStudent.TransactionId,
                Amount = subjectStudent.Amount,
                StudentID = subjectStudent.StudentId,
                StudentName = subjectStudent.Student?.User?.FirstName + " " + subjectStudent.Student?.User?.LastName,
                SubjectID = subjectStudent.SubjectId,
                SubjectName = subjectStudent.Subject?.SubjectName,
                IsPaid = subjectStudent.IsPaid,
                PaymentDate = subjectStudent.PaymentDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "N/A",
                InstructorId= subjectStudent.Subject?.InstructorID,
                InstructorName= subjectStudent.Subject?.Instructor?.User?.FirstName + " " + subjectStudent.Subject?.Instructor?.User?.LastName,

            };
        }

        public async Task<List<PaymentDTO>> GetAllPayments()
        {
            var payments = await _paymentRepository.GetAllPayments();

            if (payments == null || !payments.Any())
                return new List<PaymentDTO>();

           
            return payments.Select(payment => new PaymentDTO
            {
              
                Amount = payment.Amount,
                StudentID = payment.StudentId,
                StudentName = payment.Student?.User?.FirstName + " " + payment.Student?.User?.LastName,
                SubjectID = payment.SubjectId,
                SubjectName = payment.Subject?.SubjectName,
                IsPaid = payment.IsPaid,
                PaymentDate = payment.PaymentDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "N/A",
                TransactionId = payment.TransactionId,

            }).ToList();
        }


        public async Task<PaymentDTO> GetPaymentsByStudentIdAndSubjectId(int studentId, int SubjectId)
        {
            var payment = await _context.SubjectStudents
                .Include(ps => ps.Student)
                .ThenInclude(s => s.User)
                .Include(ps => ps.Subject)
                .ThenInclude(sub=>sub.Instructor)
                .ThenInclude(i=>i.User)
                .Where(ps => ps.StudentId == studentId && ps.SubjectId == SubjectId)
                .FirstOrDefaultAsync();

            if (payment == null)
                return null;

            return new PaymentDTO
            {
                StudentID = payment.StudentId,
                StudentName = payment.Student?.User?.FirstName + " " + payment.Student?.User?.LastName,
                SubjectID = payment.SubjectId,
                SubjectName = payment.Subject?.SubjectName,
                IsPaid = payment.IsPaid,
                PaymentDate = payment.PaymentDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "N/A",
                TransactionId = payment.TransactionId,
                InstructorId = payment.Subject?.InstructorID,
                InstructorName = payment.Subject?.Instructor?.User?.FirstName + " " + payment.Subject?.Instructor?.User?.LastName,
                Amount = payment.Amount


            };
        }

        public async Task<int> NumberOfStudentInSubject(int subjectId)
        {
            var count = await _paymentRepository.NumberOfStudentInSubject(subjectId);
            return count;
        }

        

    }
}



