using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopping.Context;

namespace OnlineShopping.Controllers
{
    public class SizeController : Controller
    {
        OnlineShopping_DBContext Context = new OnlineShopping_DBContext();
        // GET: Size
        public ActionResult Index()
        {
            return View(Context.Sizes.ToList());
        }
    }
}