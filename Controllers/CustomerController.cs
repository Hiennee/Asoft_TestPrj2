using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestPrj2.Models;
using TestPrj2.Services;

namespace TestPrj2.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            return Ok(customers);
        }

        // GET: api/Customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            try
            {
                var customer = await _customerService.GetCustomerById(id);
                if (customer == null)
                {
                    return NotFound(new { message = "Customer not found" });
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Customer/Search?name={name}
        [HttpGet("Search")]
        public async Task<IActionResult> GetCustomersByName(string name)
        {
            try
            {
                var customers = await _customerService.GetCustomersByName(name);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            try
            {
                _customerService.AddCustomer(customer);
                return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Customer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, [FromBody] Customer customer)
        {
            try
            {
                _customerService.UpdateCustomer(id, customer.CustomerName, customer.Phone);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            try
            {
                _customerService.DeleteCustomer(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
