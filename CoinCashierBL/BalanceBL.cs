using CommonClasses.DTO;
using CommonClasses.Exceptions;
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

        public static void RemoveFunds(int idCashier, int coinValue, int quantity)
        {
            CashierDTO cashier = CoinCashierDAL.CashierDAL.GetCashier(idCashier);

            CoinBalanceDTO coinBalance = cashier.coinBalanceDTOs.SingleOrDefault(it => it.coinValue == coinValue);

            if (coinBalance == null || coinBalance.idCoinBalance.HasValue == false)
            {
                throw new InvalidCoinValue();
            }
            else if (coinBalance.quantity < quantity)
            {
                throw new NotEnoughFunds();
            }
            else
            {
                coinBalance.quantity -= quantity;

                CoinCashierDAL.CoinBalanceDAL.UpdateCashierBalance(idCashier, coinBalance);
            }
        }

        public static void ResetCashier(int idCashier)
        {
            CashierDTO cashier = CoinCashierDAL.CashierDAL.GetCashier(idCashier);

            foreach (CoinBalanceDTO coinBalance in cashier.coinBalanceDTOs)
            {
                coinBalance.quantity = 0;
            }

            CoinCashierDAL.CoinBalanceDAL.UpdateCashierBalance(idCashier, cashier.coinBalanceDTOs);
        }
    }
}
