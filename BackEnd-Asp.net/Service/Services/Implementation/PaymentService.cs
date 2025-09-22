using Domain.DTO;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Repository.Contract;
using Repository.Implementation;
using Service.Services.Contract;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Implementation
{
    public class PaymentService 
        //: IPaymentService
    {
        //private readonly IConfiguration _iconfiguration;
        //private readonly ISubjectRepository _ISubjectRepository;
        //private readonly IStudentRepository _studentRepository;

        //public PaymentService(IConfiguration Iconfiguration, ISubjectRepository IsubjectRepository, IStudentRepository studentRepository)
        //{
        //    _iconfiguration = Iconfiguration;
        //    _ISubjectRepository = IsubjectRepository;
        //    _studentRepository = studentRepository;
        //}

        //public async Task<PaymentDTO> CreateOrUpdatePaymentAsync(int SubjectId , int StudentId )
        //{
        //    StripeConfiguration.ApiKey = _iconfiguration["StripSetting:SecretKey"];

        //    var subject = _ISubjectRepository.GetByIdWithInstructorAndPayment(SubjectId);
        //    if (subject == null) return null;
        //    var student = await _studentRepository.GetByIdAsync(StudentId);
        //    if (student == null) return null;

        //    var subtotal = subject.Price;
        //    if (subtotal < 0.5m)
        //        throw new Exception("Amount is below the minimum allowed by Stripe.");

        //    var service = new PaymentIntentService();
        //    PaymentIntent paymentIntent;

        //    var existingPayment = await _studentRepository.GetPaymentBySubjectAndStudent(SubjectId, StudentId);
        //    if (existingPayment == null)
        //    {
        //        // Create new PaymentIntent
        //        var createOptions = new PaymentIntentCreateOptions()
        //        {
        //            Amount = (long)(subtotal * 100),
        //            Currency = "usd",
        //            PaymentMethodTypes = new List<string> { "card" },
        //        };

        //        paymentIntent = await service.CreateAsync(createOptions);

        //        var newPayment = new Payment
        //        {
        //            PaymentIntentId = paymentIntent.Id,
        //            ClientSecret = paymentIntent.ClientSecret,
        //            TotalPayMent = subtotal,
        //            SubjectID = subject.SubjectID,
        //            StudentID = StudentId,
        //            IsSuccessful = false
        //        };

        //        _ISubjectRepository.AddPayment(newPayment);

        //        return new PaymentDTO
        //        {
        //            PaymentID = newPayment.PaymentID,
        //            PaymentIntentId = newPayment.PaymentIntentId,
        //            ClientSecret = newPayment.ClientSecret,
        //            TotalPayMent = newPayment.TotalPayMent,
        //            IsSuccessful = newPayment.IsSuccessful,
        //            SubjectID = subject.SubjectID,
        //            SubjectName = subject.SubjectName,
        //            StudentID = StudentId,
        //            StudentName = student.FirstName + " " + student.LastName,
        //            InstructorId=subject.InstructorID,
        //            InstructorName = subject.Instructor?.Name
        //        };
        //    }
        //    else
        //    {
        //        var updateOptions = new PaymentIntentUpdateOptions()
        //        {
        //            Amount = (long)(subtotal * 100),
        //        };

        //        paymentIntent = await service.UpdateAsync(existingPayment.PaymentIntentId, updateOptions);

        //        existingPayment.ClientSecret = paymentIntent.ClientSecret;
        //        existingPayment.TotalPayMent = subtotal;

        //        _ISubjectRepository.UpdatePayment(existingPayment);

        //        return new PaymentDTO
        //        {
        //            PaymentID = existingPayment.PaymentID,
        //            PaymentIntentId = existingPayment.PaymentIntentId,
        //            ClientSecret = existingPayment.ClientSecret,
        //            TotalPayMent = existingPayment.TotalPayMent,
        //            IsSuccessful = existingPayment.IsSuccessful,
        //            SubjectID = subject.SubjectID,
        //            SubjectName = subject.SubjectName,
        //            StudentID = StudentId,
        //            StudentName = student.FirstName + " " + student.LastName,
        //            InstructorId = subject.InstructorID,
        //            InstructorName = subject.Instructor?.Name
        //        };
        //    }
        //}
    }
}



