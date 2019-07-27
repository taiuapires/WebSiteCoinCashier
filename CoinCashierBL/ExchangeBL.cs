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
        public static List<CoinChangeDTO> PerformSale(int idCashier, int saleValue)
        {
            CashierDTO cashier = CoinCashierDAL.CashierDAL.GetCashier(idCashier);

            if (CanPerformExchange(cashier, saleValue) == false)
            {
                throw new CannotProcessExchange();
            }

            List<CoinChangeDTO> change = PerformExchange(cashier, saleValue);

            return change;
        }

        private static bool CanPerformExchange(CashierDTO cashier, int saleValue)
        {
            return false;
        }

        private static List<CoinChangeDTO> PerformExchange(CashierDTO cashier, int saleValue)
        {
            List<CoinChangeDTO> change = new List<CoinChangeDTO>();



            return change;
        }
    }
}
