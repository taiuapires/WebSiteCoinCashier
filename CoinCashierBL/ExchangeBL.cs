using CommonClasses.DTO;
using CommonClasses.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCashierBL
{
    public static class ExchangeBL
    {
        public static List<CoinChangeDTO> PerformSale(CashierDTO cashier, int totalChange)
        {
            cashier.coinBalanceDTOs = cashier.coinBalanceDTOs.OrderByDescending(it => it.coinValue).ToList();

            List<CoinBalanceDTO> newCoinBalances = null;

            if (PerformExchange(cashier.coinBalanceDTOs, totalChange, Int32.MaxValue, out newCoinBalances) == false)
            {
                throw new CannotProcessExchange();
            }

            List<CoinChangeDTO> change = CalculateChange(cashier, newCoinBalances);

            CoinCashierDAL.CoinBalanceDAL.UpdateCashierBalance(cashier.idCashier, newCoinBalances.Where(it => change.Any(ite => ite.coinValue == it.coinValue)).ToList());

            cashier.coinBalanceDTOs = newCoinBalances;

            return change;
        }

        private static bool PerformExchange(List<CoinBalanceDTO> coinBalanceDTOs, int requiredChange, int skipValue, out List<CoinBalanceDTO> newCoinBalances)
        {
            newCoinBalances = DeepCopyBalances(coinBalanceDTOs);

            var validCoins = newCoinBalances.Where(it => it.coinValue <= requiredChange && it.coinValue < skipValue);

            if (validCoins.Count() == 0)
            {
                return false;
            }

            CoinBalanceDTO coinBalance = coinBalance = validCoins.First();

            if (requiredChange % coinBalance.coinValue == 0 && coinBalance.totalValue >= requiredChange)
            {
                int decrementedQuantity = requiredChange / coinBalance.coinValue;
                int decrementedValue = decrementedQuantity * coinBalance.coinValue;
                coinBalance.quantity -= decrementedQuantity;
                return true;
            }
            else
            {
                int decrementedQuantity = requiredChange / coinBalance.coinValue;
                int decrementedValue = decrementedQuantity * coinBalance.coinValue;
                coinBalance.quantity -= decrementedQuantity;

                bool pathSuccessful = PerformExchange(newCoinBalances, requiredChange - decrementedValue, skipValue, out newCoinBalances);

                if (pathSuccessful == false)
                {
                    // retry skipping coins of the current coin value
                    return PerformExchange(coinBalanceDTOs, requiredChange, coinBalance.coinValue, out newCoinBalances);
                }
                else
                {
                    return true;
                }
            }
        }

        private static List<CoinChangeDTO> CalculateChange(CashierDTO cashier, List<CoinBalanceDTO> newCoinBalances)
        {
            List<CoinChangeDTO> change = new List<CoinChangeDTO>();

            foreach (CoinBalanceDTO cashierCoinBalance in cashier.coinBalanceDTOs)
            {
                CoinBalanceDTO newCoinBalance = newCoinBalances.Single(it => it.idCoinBalance.Value == cashierCoinBalance.idCoinBalance.Value);

                if (newCoinBalance.quantity != cashierCoinBalance.quantity)
                {
                    change.Add(new CoinChangeDTO()
                    {
                        coinValue = cashierCoinBalance.coinValue,
                        quantity = (cashierCoinBalance.quantity - newCoinBalance.quantity)
                    });
                }
            }

            return change;
        }

        private static List<CoinBalanceDTO> DeepCopyBalances(List<CoinBalanceDTO> coinBalanceDTOs)
        {
            return coinBalanceDTOs.Select(it => new CoinBalanceDTO()
            {
                idCoinBalance = it.idCoinBalance,
                coinValue = it.coinValue,
                quantity = it.quantity
            }).ToList();
        }
    }
}
