using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;

        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }

        [Authorize]
        public ViewResult List() => View(repository.Orders.Where(order => !order.Shipped));

        [Authorize]
        public ViewResult Shipped() => View(repository.Orders.Where(order => order.Shipped));

        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = repository.Orders.FirstOrDefault(ord => ord.OrderID == orderID);

            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }

            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        [Authorize]
        public IActionResult MarkPending(int orderID)
        {
            Order order = repository.Orders.FirstOrDefault(ord => ord.OrderID == orderID);

            if (order != null)
            {
                order.Shipped = false;
                repository.SaveOrder(order);
            }

            return RedirectToAction(nameof(Shipped));
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction("Completed", order);
            } else {
                return View(order);
            }
        }

        public ViewResult Completed(Order order)
        {
            cart.Clear();
            return View(order);
        }
    }
}