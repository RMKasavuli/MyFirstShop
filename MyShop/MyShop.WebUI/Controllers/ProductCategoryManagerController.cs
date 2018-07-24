using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryManagerController(IRepository<ProductCategory> context)
        {
            this.context = context;
        }
        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        //cretae new product category
        public ActionResult Create()//to display the page only
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]//getting info from a page
        public ActionResult Create(ProductCategory productCategory)//to fill in the details
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);//stay on the current page
            }
            else
            {
                context.Insert(productCategory);//add product category to cache memory
                context.Commit();//refresh cache memory

                return RedirectToAction("Index");//redirect to Index page, to view the updated list
            }
        }

        //edit product category
        public ActionResult Edit(string Id)//to find the product category
        {
            //find the product category
            ProductCategory productCategory = context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }

        [HttpPost]//getting info from a page
        public ActionResult Edit(ProductCategory productCategory, string Id)//to edit the product
        {
            ProductCategory productCategoryToEdit = context.Find(Id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategoryToEdit);//stay on the current page
                }
                else
                {
                    //update product category to edit
                    productCategoryToEdit.Category = productCategory.Category;
                    //productCategoryToEdit.Description = product.Description;
                    //productCategoryToEdit.Image = product.Image;
                    //productToEdit.Name = product.Name;
                    //productToEdit.Price = product.Price;

                    context.Commit();//refresh cache memory

                    return RedirectToAction("Index");//redirect to Index page, to view the updated list
                }
            }
        }

        public ActionResult Delete(string Id)//to find the product category to delete
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(ProductCategory productCategory, string Id)//to find the product category to delete
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
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