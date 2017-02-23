using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using OnlineShopping.Context;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class HomeController : Controller
    {
        private OnlineShopping_DBContext Context = new OnlineShopping_DBContext();
        private byte[] _bytes;
        private String _image;
        // GET: Home
        public ActionResult Index()
        {
            var dir = Server.MapPath("~/App_Data/Images/Products/");
            List<Product> products = new List<Product>();

            foreach (var product in Context.Products.ToList())
            {
                _bytes = System.IO.File.ReadAllBytes(dir + product.Product_picture_path);
                _image = Convert.ToBase64String(_bytes);
                Product p = new Product()
                {
                    Product_Id = product.Product_Id,
                    Product_name = product.Product_name,
                    Product_price = product.Product_price,
                    Product_vat = product.Product_vat,
                    Product_discount = product.Product_discount,
                    Product_picture_path = _image,
                    Product_quantity = product.Product_quantity,
                    Product_description = product.Product_description
                };
                products.Add(p);
            }
            return View(products);
        }


        [HttpGet]
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Product product = Context.Products.SingleOrDefault(pro => pro.Product_Id == id);
                Size size = Context.Sizes.SingleOrDefault(s => s.Size_Id == product.Size_Id);
                Sub_Category subCategory = Context.SubCategories.SingleOrDefault(sub => sub.Sub_Category_Id == product.Sub_Category_Id);
                string[] description_li = product.Product_description.Split(',');
                ViewBag.description = description_li;
                var path = Server.MapPath("~/App_Data/Images/Products/");
                _bytes = System.IO.File.ReadAllBytes(path + product.Product_picture_path);
                _image = Convert.ToBase64String(_bytes);

                if (size != null)
                {
                    ViewBag.sizename = size.Size_name;
                }

                if (subCategory != null)
                {
                    ViewBag.subcategoryname = subCategory.Sub_Category_name;
                }

                if (product.Product_quantity == 0)
                {
                    ViewBag.stockStatus = "Out of Stock";
                    ViewBag.colorStatus = "#FF0000";
                }
                else
                {
                    ViewBag.stockStatus = "In Stock";
                    ViewBag.colorStatus = "#74DF00";
                }

                product.Product_picture_path = _image;
                return View(product);
            }
            
        }

        [HttpGet]
        public ActionResult CartView()
        {
            return View(Context.Products.ToList());
        }


        [HttpGet]
        public ActionResult Registration()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {

            if (ModelState.IsValid)
            {
                if (user.User_password == user.User_ConfirmPassword)
                {
                    return RedirectToAction("SuccessPage", "Home");
                }
                else
                {
                    return View(user);
                }
            }
            else
            {
                return View(user);
            }
            
        }

        [HttpGet]
        public ActionResult SuccessPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            User user = Context.Users.SingleOrDefault(u => u.User_name == username && u.User_password == password);
            Session["User"] = user;
            return RedirectToAction("Index", "Product");
        }
    }
}