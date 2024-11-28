using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TestPrj2.Models;

namespace TestPrj2.Services
{
    public class CustomerService
    {
        private readonly AsoftContext _context;

        public CustomerService(AsoftContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
        public async Task<ICollection<Customer>> GetCustomersByName(string name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            return await _context.Customers.Where(c => c.CustomerName.Contains(name)).ToListAsync();
        }
        public async Task<Customer> GetCustomerById(string id)
        {
            if (id.IsNullOrEmpty())
            {
                throw new Exception("INVALID_ARGUMENT");
            }
            return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId.Equals(id));
        }
        public async void AddCustomer(Customer c)
        {
            if (c.CustomerId.IsNullOrEmpty() || c.CustomerName.IsNullOrEmpty() || c.Phone.IsNullOrEmpty())
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            await _context.Customers.AddAsync(c);
            await _context.SaveChangesAsync();
        }
        public async void UpdateCustomer(string id, string name, string phone)
        {
            if (id.IsNullOrEmpty() || name.IsNullOrEmpty() || phone.IsNullOrEmpty())
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            var c = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId.Equals(id));
            c.CustomerName = name;
            c.Phone = phone;
            await _context.SaveChangesAsync();
        }
        public async void DeleteCustomer(string id)
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            _context.Customers.Remove(await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId.Equals(id)));
            await _context.SaveChangesAsync();
        }
    }
}
