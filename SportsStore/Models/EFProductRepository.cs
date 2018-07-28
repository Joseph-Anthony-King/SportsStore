using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;

        public EFProductRepository(ApplicationDbContext cntxt)
        {
            context = cntxt;
        }

        public IQueryable<Product> Products => context.Products;
    }
}