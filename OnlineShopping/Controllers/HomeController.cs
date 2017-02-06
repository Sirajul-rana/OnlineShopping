using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopping.Context;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class HomeController : Controller
    {
        private OnlineShopping_DBContext Context = new OnlineShopping_DBContext();
        // GET: Home
        public ActionResult Index()
        {
            return View(Context.Products.ToList());
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Product product = Context.Products.SingleOrDefault(pro => pro.Product_Id == id);
            Size size = Context.Sizes.SingleOrDefault(s => s.Size_Id == product.Size_Id);
            Sub_Category subCategory = Context.SubCategories.SingleOrDefault(sub => sub.Sub_Category_Id == product.Sub_Category_Id);

            if (size != null)
            {
                ViewBag.sizename = size.Size_name;
            }

            if (subCategory != null)
            {
                ViewBag.subcategoryname = subCategory.Sub_Category_name;
            }
            return View(product);
        }

        [HttpPost]
        public string Login(string username, string password)
        {
            return "We got to Test01"+username+password;
        }
    }
}