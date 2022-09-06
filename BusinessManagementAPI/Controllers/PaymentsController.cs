using BusinessManagementAPI.DTOs;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayments(int id)
        {
            IEnumerable<Payment> payments = await _paymentRepository.GetPayments(id);
            return Utilities.IsAny(payments) ? Ok(payments) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePayments(List<PaymentDTO> payments)
        {
            var updatedPayments = await _paymentRepository.UpdatePayment(payments);
            return updatedPayments is not null ? Ok(updatedPayments) : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            return await _paymentRepository.DeletePayment(id) ? Ok() : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayments(List<CreatePaymentDTO> payments)
        {
            return await _paymentRepository.CreatePayments(payments) ? Ok() : NotFound();
        }
    }
}