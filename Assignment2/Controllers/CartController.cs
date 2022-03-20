using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChangeQuantity(int Id, string ChangeType)
        {
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            foreach(CartItem item in cart)
            {
                if(item.Id == Id)
                {
                    switch (ChangeType)
                    {
                        case "minus":
                            if(item.Quantity - 1 > 0)
                            {
                                int oldQuantity = item.Quantity;
                                item.Quantity = oldQuantity - 1;
                            }
                            else
                            {
                                item.Quantity = 1;
                            }
                            break;
                        case "plus":
                            item.Quantity += 1;
                            break;
                    }
                    item.TotalPrice = item.Quantity * item.Product.Price;
                }
            }
            Session["cart"] = cart;
            return PartialView("IndexAjax");
        }

        public ActionResult Remove(int Id)
        {
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            foreach (CartItem item in cart.ToList())
            {
                if (item.Id == Id)
                {
                    cart.Remove(item);
                }
            }
            Session["cart"] = cart;
            return PartialView("IndexAjax");
        }

        public int UpdateCartQuantity()
        {
            int quantity = 0;
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            foreach (CartItem item in cart.ToList())
            {
                quantity += item.Quantity;
            }
            return quantity;
        }
    }
}