using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProducRepository repository;

        public ProductController(IProducRepository repo)
        {
            repository = repo;
        }

        public ViewResult List() => View(repository.Products);
    }
}