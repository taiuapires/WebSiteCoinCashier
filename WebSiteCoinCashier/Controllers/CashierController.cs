using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class CashierController : Controller
    {
        public ActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddCoins()
        {
            return Json(new
            {
                data = "Hello World"
            });
        }

        public ActionResult Exchange()
        {
            return View();
        }

        public ActionResult Withdraw()
        {
            return View();
        }
    }
}