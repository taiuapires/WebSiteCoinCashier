using CommonClasses.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCashierBL
{
    public static class BalanceBL
    {
        public static CashierDTO LoadBalance(int idCashier)
        {
            return CoinCashierDAL.CashierDAL.GetCashier(idCashier);
        }

        public static void AddFunds(int idCashier, int coinValue, int quantity)
        {
            CashierDTO cashier = CoinCashierDAL.CashierDAL.GetCashier(idCashier);

            CoinBalanceDTO coinBalance = cashier.coinBalanceDTOs.SingleOrDefault(it => it.coinValue == coinValue);

            if (coinBalance == null || coinBalance.idCoinBalance.HasValue == false)
            {
                CoinCashierDAL.CoinBalanceDAL.InsertCoinBalance(idCashier, new CoinBalanceDTO()
                {
                    coinValue = coinValue,
                    quantity = quantity
                });
            }
            else
            {
                coinBalance.quantity += quantity;

                CoinCashierDAL.CoinBalanceDAL.UpdateCoinBalance(idCashier, coinBalance);
            }
        }
    }
}
