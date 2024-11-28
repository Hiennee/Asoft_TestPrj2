using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestPrj2.Models;
using TestPrj2.Services;

namespace TestPrj2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly InvoiceDetailService _service;

        public InvoiceDetailController(InvoiceDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoiceDetails()
        {
            var result = await _service.GetAllInvoiceDetails();
            return Ok(result);
        }

        [HttpGet("ByInvoice/{invoiceId}")]
        public async Task<IActionResult> GetInvoiceDetailsByInvoiceID(string invoiceId)
        {
            try
            {
                var result = await _service.GetInvoiceDetailsByInvoiceID(invoiceId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceDetailById(string id)
        {
            try
            {
                var result = await _service.GetInvoiceDetailById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoiceDetail([FromBody] InvoiceDetailDto invoiceDetailDto)
        {
            try
            {
                _service.AddInvoiceDetail(invoiceDetailDto.InvoiceDetailId, invoiceDetailDto.InvoiceId,
                                          invoiceDetailDto.ProductId, invoiceDetailDto.Quantity);
                return Ok(new { message = "InvoiceDetail added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInvoiceDetail([FromBody] InvoiceDetailDto invoiceDetailDto)
        {
            try
            {
                _service.UpdateInvoiceDetail(invoiceDetailDto.InvoiceId, invoiceDetailDto.ProductId, invoiceDetailDto.Quantity);
                return Ok(new { message = "InvoiceDetail updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInvoiceDetail([FromQuery] string invoiceId, [FromQuery] string productId)
        {
            try
            {
                _service.DeleteInvoiceDetail(invoiceId, productId);
                return Ok(new { message = "InvoiceDetail deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
    public class InvoiceDetailDto
    {
        public string InvoiceDetailId { get; set; }
        public string InvoiceId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
