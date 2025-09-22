using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Contract;

namespace E_LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        public async Task<ActionResult<PaymentDTO>> CreateOrUpdatePaymentAsync([FromQuery] int SubjectId , [FromQuery] int StudentId)
        {
            var subject = await _paymentService.CreateOrUpdatePaymentAsync(SubjectId , StudentId );
            if(subject == null)
            
             return   BadRequest( "Payment creation failed.");


            return Ok(subject);
        }
    }
}
