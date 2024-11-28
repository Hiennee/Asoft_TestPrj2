using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestPrj2.Models;
using TestPrj2.Services;

namespace TestPrj2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;

        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoices();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(string id)
        {
            try
            {
                var invoice = await _invoiceService.GetInvoiceById(id);
                if (invoice == null)
                {
                    return NotFound("Invoice not found.");
                }
                return Ok(invoice);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpGet("date")]
        public async Task<IActionResult> GetInvoicesByDate(int year, int month, int day)
        {
            try
            {
                var invoices = await _invoiceService.GetInvoiceByDateTime(year, month, day);
                return Ok(invoices);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetInvoicesByCustomer(string customerId)
        {
            try
            {
                var invoices = await _invoiceService.GetInvoiceByCustomer(customerId);
                return Ok(invoices);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoice([FromBody] Invoice invoice)
        {
            try
            {
                _invoiceService.AddInvoice(invoice);
                return CreatedAtAction(nameof(GetInvoiceById), new { id = invoice.InvoiceId }, invoice);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(string id, int year, int month, int day)
        {
            try
            {
                _invoiceService.UpdateInvoice(id, year, month, day);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(string id)
        {
            try
            {
                _invoiceService.DeleteInvoice(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }
    }
}
