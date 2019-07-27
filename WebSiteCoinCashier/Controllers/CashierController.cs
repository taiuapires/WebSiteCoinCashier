using CommonClasses.DTO;
using CommonClasses.Exceptions;
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
            if (coinValue <= 0)
            {
                return Json(new
                {
                    resultCode = 1 // Invalid Coin Value
                });
            }
            else if (quantity <= 0)
            {
                return Json(new
                {
                    resultCode = 2 // Invalid Quantity
                });
            }

            CoinCashierBL.BalanceBL.AddFunds(1, coinValue, quantity);

            return Json(new
            {
                resultCode = 0 // no error
            });
        }

        public ActionResult Exchange()
        {
            return View();
        }

        public JsonResult ExchangeFunds(int totalChange)
        {
            CashierDTO cashier = CoinCashierBL.BalanceBL.LoadBalance(1);

            try
            {
                List<CoinChangeDTO> coinChanges = CoinCashierBL.ExchangeBL.PerformSale(cashier, totalChange);

                return Json(new
                {
                    resultCode = 0, // no error
                    exchangeResult = new ExchangeResultDTO()
                    {
                        resultChange = coinChanges,
                        cashier = cashier
                    }
                });
            }
            catch (CannotProcessExchange)
            {
                return Json(new
                {
                    resultCode = 1, // can't make change
                });
            }
        }

        public ActionResult ExchangeResult(ExchangeResultDTO exchangeResult)
        {
            return View(exchangeResult);
        }

        public ActionResult Withdraw()
        {
            return View();
        }

        public JsonResult WithdrawFunds(int coinValue, int quantity)
        {
            int resultCode = 0; // no error

            try
            {
                CoinCashierBL.BalanceBL.RemoveFunds(1, coinValue, quantity);
            }
            catch (InvalidCoinValue)
            {
                resultCode = 1;
            }
            catch (NotEnoughFunds)
            {
                resultCode = 2;
            }

            return Json(new
            {
                resultCode
            });
        }

        public JsonResult ResetCashier()
        {
            CoinCashierBL.BalanceBL.ResetCashier(1);

            return Json(new
            {
                resultCode = 0 // no error
            });
        }
    }
}
