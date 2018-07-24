using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        //cretae new product
        public ActionResult Create()//to display the page only
        {
            ProductManagerViewModel ViewModel = new ProductManagerViewModel();
            ViewModel.Product = new Product();
            //get from the database
            ViewModel.ProductCategories = productCategories.Collection();
            return View(ViewModel);
        }

        [HttpPost]//getting info from a page
        public ActionResult Create(Product product)//to fill in the details
        {
            if (!ModelState.IsValid )
            {
                return View(product);//stay on the current page
            }
            else
            {
                context.Insert(product);//add product to cache memory
                context.Commit();//refresh cache memory

                return RedirectToAction("Index");//redirect to Index page, to view the updated list
            }

        }

        //edit product
        public ActionResult Edit(string Id)//to find the product
        {
            //find the product
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel ViewModel = new ProductManagerViewModel();
                ViewModel.Product = product;
                //get from the database
                ViewModel.ProductCategories = productCategories.Collection();

                return View(ViewModel);
            }

        }

        [HttpPost]//getting info from a page
        public ActionResult Edit(Product product, string Id)//to edit the product
        {

            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);//stay on the current page
                }
                else
                {
                    //update product to edit
                    productToEdit.Category = product.Category;
                    productToEdit.Description = product.Description;
                    productToEdit.Image = product.Image;
                    productToEdit.Name = product.Name;
                    productToEdit.Price = product.Price;

                    context.Commit();//refresh cache memory

                    return RedirectToAction("Index");//redirect to Index page, to view the updated list
                }

            }
        }

        public ActionResult Delete(string Id)//to find the product to delete
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Product product, string Id)//to find the product to delete
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();//refresh cache memory
                return RedirectToAction("Index");//redirect to Index page, to view the updated list
            }

        }





    }
}