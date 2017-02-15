using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using OnlineShopping.Context;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class ProductController : Controller
    {
        private OnlineShopping_DBContext Context = new OnlineShopping_DBContext();
        private byte[] _bytes;
        private String _image;
        // GET: Product
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                User user = (User) Session["User"];
                ViewBag.some = user.User_name;
                return View(Context.Products.ToList());
            }
            else
            {
               return RedirectToAction("Index", "Home");
            }
            //User user = (User)Session["User"];
            //Console.WriteLine(user.User_name);
        }
        [HttpGet]
        public ActionResult LoadProducts()
        {
            var dir = Server.MapPath("~/App_Data/Images/Products/");
            List<Product> products = new List<Product>();
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                ViewBag.some = user.User_name;
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                ViewBag.some = user.User_name;
                ViewData["Size_Id"] = new SelectList(Context.Sizes, "Size_Id", "Size_name");
                ViewData["Sub_category"] = new SelectList(Context.SubCategories, "Sub_Category_Id", "Sub_Category_name");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                string size_id = Request.Form["Size"].ToString();
                string sub_category_id = Request.Form["sub_category"].ToString();
                var httpPostedFileBase = Request.Files["PhotoUpload"];
                var fileName = "";
                if (httpPostedFileBase != null)
                {
                    fileName = Path.GetFileName(httpPostedFileBase.FileName);
                }
                else
                {
                    TempData["Message"] = "Some thing wrong!";
                }
                var dir = Path.Combine(Server.MapPath("~/App_Data/Images/Products/"), fileName);
                if (httpPostedFileBase != null)
                {
                    httpPostedFileBase.SaveAs(dir);
                    product.Product_picture_path = httpPostedFileBase.FileName;
                }
                product.Size_Id = Convert.ToInt32(size_id);
                product.Sub_Category_Id = Convert.ToInt32(sub_category_id);
                Context.Products.Add(product);
                Context.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ViewData["Size_Id"] = new SelectList(Context.Sizes, "Size_Id", "Size_name");
                ViewData["Sub_category"] = new SelectList(Context.SubCategories, "Sub_Category_Id", "Sub_Category_name");
                return View(product);
            }
            
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Product product = Context.Products.SingleOrDefault(pro => pro.Product_Id == id);
            Size size = Context.Sizes.SingleOrDefault(s => s.Size_Id== product.Size_Id);
            Sub_Category subCategory = Context.SubCategories.SingleOrDefault(sub => sub.Sub_Category_Id == product.Sub_Category_Id);
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
            product.Product_picture_path = _image;
            return View(product);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product product = Context.Products.SingleOrDefault(pro => pro.Product_Id == id);
            ViewData["Size_Id"] = new SelectList(Context.Sizes, "Size_Id", "Size_name", product.Size_Id);
            ViewData["Sub_category"] = new SelectList(Context.SubCategories, "Sub_Category_Id", "Sub_Category_name", product.Sub_Category_Id);


            var path = Server.MapPath("~/App_Data/Images/Products/");
            _bytes = System.IO.File.ReadAllBytes(path + product.Product_picture_path);
            _image = Convert.ToBase64String(_bytes);
            product.Product_picture_path = _image;
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            var fileName = "";
            Product updateProduct = Context.Products.SingleOrDefault(pro => pro.Product_Id == product.Product_Id);
            string sizeId = Request.Form["Size"].ToString();
            string subCategoryId = Request.Form["sub_category"].ToString();
            var dir = Server.MapPath("~/App_Data/Images/Products/");
            var httpPostedFileBase = Request.Files["PhotoUpload"];

            if (httpPostedFileBase != null)
            {
                if (httpPostedFileBase.FileName == updateProduct.Product_picture_path)
                {
                    updateProduct.Product_picture_path = httpPostedFileBase.FileName;
                }
                else
                {
                    if (System.IO.File.Exists(dir + updateProduct.Product_picture_path))
                    {
                        System.IO.File.Delete(dir + updateProduct.Product_picture_path);
                    }
                    fileName = Path.GetFileName(httpPostedFileBase.FileName);
                    dir = Path.Combine(Server.MapPath("~/App_Data/Images/Products/"), fileName);
                    httpPostedFileBase.SaveAs(dir);
                }

            }
            else
            {
                TempData["Message"] = "Some thing wrong!";
            }

            if (updateProduct != null)
            {
                updateProduct.Product_name = product.Product_name;
                updateProduct.Product_price = product.Product_price;
                updateProduct.Product_vat = product.Product_vat;
                updateProduct.Product_discount = product.Product_discount;
                updateProduct.Product_picture_path = httpPostedFileBase.FileName;
                updateProduct.Product_quantity = product.Product_quantity;
                updateProduct.Product_description = product.Product_description;
                updateProduct.Size_Id = Convert.ToInt32(sizeId);
                updateProduct.Sub_Category_Id = Convert.ToInt32(subCategoryId);
            }
            
            Context.SaveChanges();
            return RedirectToAction("Index", "Product");
        }


        [HttpGet]
        public ActionResult Delete(int id)
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
        [ActionName("Delete")]
        public ActionResult Delete_Post(int id)
        {
            Product product = Context.Products.SingleOrDefault(pro => pro.Product_Id == id);
            Context.Products.Remove(product);
            Context.SaveChanges();
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
        }

    }
}