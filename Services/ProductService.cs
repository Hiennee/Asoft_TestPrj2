using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TestPrj2.Models;

namespace TestPrj2.Services
{
    public class ProductService
    {
        private readonly AsoftContext _context;
        public ProductService(AsoftContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<ICollection<Product>> GetProductsByName(string name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new Exception("INVALID_ARGUMENT");
            }
            return await _context.Products.Where(c => c.ProductName.Contains(name)).ToListAsync();
        }
        public async Task<Product> GetProductById(string id)
        {
            if (id.IsNullOrEmpty())
            {
                throw new Exception("INVALID_ARGUMENT");
                
            }
            return await _context.Products.FirstOrDefaultAsync(c => c.ProductId.Equals(id));
        }
        public async void AddProduct(Product p)
        {
            if (p.ProductId.IsNullOrEmpty() || p.ProductName.IsNullOrEmpty() || p.Price == 0)
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            await _context.Products.AddAsync(p);
            await _context.SaveChangesAsync();
        }
        public async void UpdateProduct(string id, string name, double price)
        {
            if (id.IsNullOrEmpty() || name.IsNullOrEmpty() || price == 0)
            {
                throw new ArgumentException("INVALID_ARGUMENT");
            }
            var p = await _context.Products.FirstOrDefaultAsync(p => p.ProductId.Equals(id));
            if (p == null)
            {
                throw new Exception("INVALID_PRODUCT");
            }
            p.ProductName = name;
            p.Price = price;
            await _context.SaveChangesAsync();
        }
    }
}
