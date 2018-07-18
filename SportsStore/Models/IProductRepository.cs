using System.Linq;

namespace SportsStore.Models
{
    public interface IProducRepository
    {
        IQueryable<Product> Products { get; }
    }
}