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
    }
}
