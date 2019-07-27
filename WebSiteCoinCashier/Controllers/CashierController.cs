using CommonClasses.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class CashierController : Controller
    {
        public ActionResult Balance()
        {
            CashierDTO cashier = CoinCashierBL.BalanceBL.LoadBalance(1);

            return View(cashier);
        }

        public ActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddFunds(int coinValue, int quantity)
        {
            return Json(new
            {
                resultCode = 0 // no error
            });
        }

        public ActionResult Exchange()
        {
            return View();
        }

        public JsonResult PerformExchange(int saleValue)
        {
            return Json(new
            {
                resultCode = 0 // no error
            });
        }

        public ActionResult Withdraw()
        {
            return View();
        }

        public JsonResult WithdrawFunds(int coinValue, int quantity)
        {
            return Json(new
            {
                resultCode = 0 // no error
            });
        }

        public JsonResult ResetCashier()
        {
            return Json(new
            {
                resultCode = 0 // no error
            });
        }
    }
}