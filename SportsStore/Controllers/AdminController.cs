﻿using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            ViewBag.ViewTitle = "Edit Product";

            return View(repository.Products.FirstOrDefault(prod => prod.ProductID == productId));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            ViewBag.ViewTitle = "Edit Product";
            bool wasSaveSuccessful = false;

            if (ModelState.IsValid)
            {
                repository.SaveProduct(product, out wasSaveSuccessful);
                TempData["success"] = wasSaveSuccessful;
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

        public ViewResult Create()
        {
            ViewBag.ViewTitle = "Create Product";

            return View("Edit", new Product());
        }

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            bool wasDeletionSuccessful = false;

            Product deletedProduct = repository.DeleteProduct(productId, out wasDeletionSuccessful);

            if (deletedProduct != null)
            {
                if (wasDeletionSuccessful)
                {
                    TempData["success"] = wasDeletionSuccessful;
                    TempData["message"] = string.Format("{0} was deleted", deletedProduct.Name);
                }
                else
                {
                    TempData["success"] = wasDeletionSuccessful;
                    TempData["message"] = string.Format("Foreign key constraint prevented deletion of the {0} product", deletedProduct.Name);
                }
            }

            return RedirectToAction("Index");
        }
    }
}