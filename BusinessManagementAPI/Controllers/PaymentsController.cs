using BusinessManagementAPI.Models;
using BusinessManagementAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentsController : Controller
    {
        private IPaymentRepository _paymentRepository;

        public PaymentsController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayment(int id)
        {
            Payment payment = await _paymentRepository.GetPayment(id);
            return payment is not null ? Ok(payment) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            IEnumerable<Payment> payments = await _paymentRepository.GetPayments();
            return Utilities.IsAny(payments) ? Ok(payments) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePayment(Payment payment)
        {
            bool status = await _paymentRepository.UpdatePayment(payment);
            return status ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePayment(int id)
        {
            bool status = await _paymentRepository.DeletePayment(id);
            return status ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}