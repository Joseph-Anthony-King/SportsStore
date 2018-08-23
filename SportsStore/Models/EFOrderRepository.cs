using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;

        public EFOrderRepository(ApplicationDbContext cntxt)
        {
            context = cntxt;
        }

        public IQueryable<Order> Orders => context.Orders
                                .Include(order => order.Lines)
                                .ThenInclude(line => line.Product);

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(line => line.Product));

            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }

            context.SaveChanges();
        }
    }
}