using Assignment2.Data;
using Assignment2.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MyDbContext db = new MyDbContext();
        // GET: Product
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 3;
            int pageIndex = (page ?? 1);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            var products = db.Products.OrderBy(p => p.Id);
            IPagedList<Product> pagedProduct = products.ToPagedList(pageIndex, pageSize);
            if (Request.IsAjaxRequest())
            {
                return PartialView("IndexAjax", pagedProduct);
            }
            return View(pagedProduct);
        }

        [HttpGet]     
        public ActionResult FindProducts(string keyword, int CategoryId, int? page)
        {
            IQueryable<Product> products = db.Products;
            if(keyword != "")
            {
               products = products.Where(p => p.Name.Contains(keyword));
            }
            var allCategory = db.Categories.Where(p => p.Name == "All").FirstOrDefault().Id;
            if(CategoryId != allCategory)
            {
               products = products.Where(p => p.CategoryId == CategoryId);
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            if(page == null) page = 1;
            int pageSize = 3;
            int pageIndex = (page ?? 1);
            products = products.OrderBy(p => p.Id);
            IPagedList<Product> pagedProduct = products.ToPagedList(pageIndex, pageSize);
            return PartialView("IndexAjax", pagedProduct);
        }

        public int AddToCart(int Id)
        {
            int quantity = 0;
            if (Session["cart"] == null)
            {
                var product = db.Products.Find(Id);
                var cartItems = new List<CartItem>
                {
                    new CartItem {Id= 1, Product = product, Quantity = 1, TotalPrice =  product.Price}
                };
                quantity += 1;
                Session["cart"] = cartItems;
            }
            else
            {
                var cartItems = (List<CartItem>)Session["cart"];
                int count = 0;
                List<int> listId = new List<int>();
                foreach (CartItem item in cartItems.ToList())
                {
                    listId.Add(item.Id);
                    if(item.Product.Id == Id)
                    {
                        item.Quantity += 1;
                        item.TotalPrice = item.Quantity * item.Product.Price;
                        count += 1;
                    }
                    quantity += item.Quantity;
                }
                if (count == 0)
                {
                    var product = db.Products.Find(Id);
                    cartItems.Add(new CartItem { Id = listId.Max() + 1, Product = product, Quantity = 1, TotalPrice = product.Price });
                    quantity += 1;
                }
                Session["cart"] = cartItems;
            }
            return quantity;
        }
    }
}