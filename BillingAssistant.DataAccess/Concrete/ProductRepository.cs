using BillingAssistant.Core.DataAccess.EntityFramework;
using BillingAssistant.DataAccess.Abstract;
using BillingAssistant.DataAccess.Concrete.Contexts;
using BillingAssistant.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.DataAccess.Concrete
{
    public class ProductRepository : EfBaseRepository<Product, ApplicationDbContext>, IProductRepository
    {
        public async Task<List<Product>> GetProductsByUserIdAsync(int userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.Products
                    .Include(p => p.Invoice)
                    .Where(p => p.Invoice.UserId == userId)
                    .ToListAsync();
            }
        }
    }
}
