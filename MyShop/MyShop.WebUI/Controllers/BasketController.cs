using MyShop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        //use Ibasketservice to expose the formattting
        IBasketService basketService;

        public BasketController(IBasketService BasketService)
        {
            this.basketService = BasketService;
        }

        // GET: Basket
        public ActionResult Index()
        {
            //get the model

            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        //2 end points for adding and removing from the basket
        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext,Id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);//Id is basket item id
            return RedirectToAction("Index");
        }

        //Basket Summary will be a partial view, not an action
        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummary);
        }

    }
}