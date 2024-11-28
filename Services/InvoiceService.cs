using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Linq.Expressions;
using TestPrj2.Models;

namespace TestPrj2.Services
{
    public class InvoiceService
    {
        private readonly AsoftContext _context;
        public InvoiceService(AsoftContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Invoice>> GetAllInvoices()
        {
            return await _context.Invoices.ToListAsync();
        }
        public async Task<Invoice> GetInvoiceById(string id)
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            return await _context.Invoices.FirstOrDefaultAsync(i => i.InvoiceId.Equals(id));
        }
        public async Task<ICollection<Invoice>> GetInvoiceByDateTime(int year, int month, int day)
        {
            if (year == null || month == null || day == null)
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            return await _context.Invoices.Where(i => i.InvoiceDate == new DateTime(year, month, day)).ToListAsync();
        }
        public async Task<ICollection<Invoice>> GetInvoiceByCustomer(string customerId)
        {
            if (customerId.IsNullOrEmpty())
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            return await _context.Invoices.Where(i => i.CustomerId.Equals(customerId)).ToListAsync();
        }
        public async void AddInvoice(Invoice i)
        {
            if (i.InvoiceId.IsNullOrEmpty() || i.CustomerId.IsNullOrEmpty())
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            await _context.Invoices.AddAsync(i);
            await _context.SaveChangesAsync();
        }
        public async void UpdateInvoice(string id, int year, int month, int day)
        {
            if (id.IsNullOrEmpty() ||  year == null || month == null || day == null)
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            var checkInvoice = await _context.Invoices.Where(i => i.InvoiceId.Equals(id)).FirstOrDefaultAsync();
            if (checkInvoice == null)
            {
                throw new Exception("INVALID_INVOICE");
            }
            checkInvoice.InvoiceDate = new DateTime(year, month, day);
            await _context.SaveChangesAsync();
        }
        public async void DeleteInvoice(string id)
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            var checkInvoice = await _context.Invoices.Where(i => i.InvoiceId.Equals(id)).FirstOrDefaultAsync();
            if (checkInvoice == null)
            {
                throw new Exception("INVALID_INVOICE");
            }
            _context.Invoices.Remove(checkInvoice);
            await _context.SaveChangesAsync();
        }
    }
}
