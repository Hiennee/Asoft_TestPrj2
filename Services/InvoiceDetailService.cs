using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TestPrj2.Models;

namespace TestPrj2.Services
{
    public class InvoiceDetailService
    {
        private readonly AsoftContext _context;
        public InvoiceDetailService(AsoftContext context)
        {
            _context = context;
        }
        public async Task<ICollection<InvoiceDetail>> GetAllInvoiceDetails()
        {
            return await _context.InvoiceDetails.ToListAsync();
        }
        public async Task<ICollection<InvoiceDetail>> GetInvoiceDetailsByInvoiceID(string invoiceId)
        {
            if (invoiceId.IsNullOrEmpty())
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            return await _context.InvoiceDetails.Where(idl => idl.InvoiceId.Equals(invoiceId)).ToListAsync();
        }
        public async Task<InvoiceDetail> GetInvoiceDetailById(string id)
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            return await _context.InvoiceDetails.FirstOrDefaultAsync(idl => idl.InvoiceDetailId.Equals(id));
        }
        public async void AddInvoiceDetail(string invdelId, string invId,
                                           string productId, int quantity)
        {
            if (invdelId.IsNullOrEmpty() || invId.IsNullOrEmpty() ||
                productId.IsNullOrEmpty() || quantity == 0)
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            var checkInvoice = await _context.Invoices.FirstOrDefaultAsync(i => i.InvoiceId.Equals(invId));
            if (checkInvoice == null)
            {
                throw new Exception("INVALID_INVOICE");
            }
            var checkProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductId.Equals(productId));
            if (checkProduct == null)
            {
                throw new Exception("INVALID_INVOICE");
            }
            var checkInvoiceProduct = await _context.InvoiceDetails.Where(idl => idl.InvoiceId.Equals(invId))
                                                                   .Where(idl => idl.ProductId.Equals(productId))
                                                                   .FirstOrDefaultAsync();
            if (checkInvoiceProduct != null)
            {
                throw new Exception("10");
            }
            await _context.InvoiceDetails.AddAsync(new InvoiceDetail
            {
                InvoiceDetailId = invdelId,
                InvoiceId = invId,
                ProductId = productId,
                Quantity = quantity,
                TotalPrice = quantity * checkProduct.Price,
            });
            await _context.SaveChangesAsync();
        }
        public async void UpdateInvoiceDetail(string invId, string productId, int quantity)
        {
            if (invId.IsNullOrEmpty() || productId.IsNullOrEmpty() || quantity == 0)
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            var checkInvoice = await _context.Invoices.FirstOrDefaultAsync(i => i.InvoiceId.Equals(invId));
            if (checkInvoice == null)
            {
                throw new Exception("INVALID_INVOICE");
            }
            var checkProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductId.Equals(productId));
            if (checkProduct == null)
            {
                throw new Exception("INVALID_INVOICE");
            }
            var checkInvoiceProduct = await _context.InvoiceDetails.Where(idl => idl.InvoiceId.Equals(invId))
                                                                   .Where(idl => idl.ProductId.Equals(productId))
                                                                   .FirstOrDefaultAsync();
            if (checkInvoiceProduct == null)
            {
                throw new Exception("INVALID_INVOICEDETAIL");
            }
            checkInvoiceProduct.Quantity = quantity;
            checkInvoiceProduct.TotalPrice = quantity * checkProduct.Price;
        }
        public async void DeleteInvoiceDetail(string invId, string productId)
        {
            if (invId.IsNullOrEmpty() || productId.IsNullOrEmpty())
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            var checkInvoice = await _context.Invoices.FirstOrDefaultAsync(i => i.InvoiceId.Equals(invId));
            if (checkInvoice == null)
            {
                throw new Exception("INVALID_INVOICE");
            }
            var checkProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductId.Equals(productId));
            if (checkProduct == null)
            {
                throw new Exception("INVALID_INVOICE");
            }
            var checkInvoiceProduct = await _context.InvoiceDetails.Where(idl => idl.InvoiceId.Equals(invId))
                                                                   .Where(idl => idl.ProductId.Equals(productId))
                                                                   .FirstOrDefaultAsync();
            if (checkInvoiceProduct == null)
            {
                throw new Exception("INVALID_INVOICEDETAIL");
            }
            _context.InvoiceDetails.Remove(checkInvoiceProduct);
            await _context.SaveChangesAsync();
        }
    }
    
}
