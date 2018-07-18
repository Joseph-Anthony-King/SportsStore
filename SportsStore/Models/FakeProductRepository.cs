using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class FakeProductRepository : IProducRepository
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product { Name = "Football", Description = "An old fashioned American football.", Price = 25 },
            new Product { Name = "Surf Board", Description = "A classic Hawaiian surf board.", Price = 179 },
            new Product { Name = "Running Shoes", Description = "Useful and functional running shoes.", Price = 95 }
        }.AsQueryable<Product>();
    }
}