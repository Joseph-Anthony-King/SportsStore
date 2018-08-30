using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

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

        public Product DeleteProduct(int productID, out bool wasDeletionSuccessful)
        {
            wasDeletionSuccessful = true;
            Product dbEntry = context.Products.FirstOrDefault(prod => prod.ProductID == productID);

            if (dbEntry != null)
            {
                try
                {
                    context.Products.Remove(dbEntry);
                    context.SaveChanges();
                }
                catch
                {
                    wasDeletionSuccessful = false;
                }
            }

            return dbEntry;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.FirstOrDefault(prod => prod.ProductID == product.ProductID);

                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }

            context.SaveChanges();
        }
    }
}