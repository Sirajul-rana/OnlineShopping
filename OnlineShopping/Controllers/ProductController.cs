using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using OnlineShopping.Context;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class ProductController : Controller
    {
        private OnlineShopping_DBContext Context = new OnlineShopping_DBContext();
        // GET: Product
        public ActionResult Index()
        {

            return View(Context.Products.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewData["Size_Id"] = new SelectList(Context.Sizes, "Size_Id", "Size_name");
            ViewData["Sub_category"] = new SelectList(Context.SubCategories, "Sub_Category_Id", "Sub_Category_name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            string size_id = Request.Form["Size"].ToString();
            string sub_category_id = Request.Form["sub_category"].ToString();
            product.Size_Id = Convert.ToInt32(size_id);
            product.Sub_Category_Id = Convert.ToInt32(sub_category_id);
            Context.Products.Add(product);
            Context.SaveChanges();
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Product product = Context.Products.SingleOrDefault(pro => pro.Product_Id == id);
            Size size = Context.Sizes.SingleOrDefault(s => s.Size_Id== product.Size_Id);
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product product = Context.Products.SingleOrDefault(pro => pro.Product_Id == id);
            if (product.Size_Id.ToString() == null && product.Sub_Category_Id.ToString() == null)
            {
                ViewData["Size_Id"] = new SelectList(Context.Sizes, "Size_Id", "Size_name");
                ViewData["Sub_category"] = new SelectList(Context.SubCategories, "Sub_Category_Id", "Sub_Category_name");
            }
            else
            {
                ViewData["Size_Id"] = new SelectList(Context.Sizes, "Size_Id", "Size_name", product.Size_Id);
                ViewData["Sub_category"] = new SelectList(Context.SubCategories, "Sub_Category_Id", "Sub_Category_name", product.Sub_Category_Id);
            }

            ViewData["Sub_category"] = new SelectList(Context.SubCategories, "Sub_Category_Id", "Sub_Category_name", product.Sub_Category_Id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            Product updateProduct = Context.Products.SingleOrDefault(pro => pro.Product_Id == product.Product_Id);
            string sizeId = Request.Form["Size"].ToString();
            string subCategoryId = Request.Form["sub_category"].ToString();
            if (updateProduct != null)
            {
                updateProduct.Product_name = product.Product_name;
                updateProduct.Product_price = product.Product_price;
                updateProduct.Product_vat = product.Product_vat;
                updateProduct.Product_discount = product.Product_discount;
                updateProduct.Product_picture_path = product.Product_picture_path;
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

    }
}