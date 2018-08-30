using System.Linq;

namespace SportsStore.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        void SaveProduct(Product product, out bool wasSaveSuccessful);

        Product DeleteProduct(int productID, out bool wasDeletionSuccessful);
    }
}