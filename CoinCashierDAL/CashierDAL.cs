using CommonClasses.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCashierDAL
{
    public static class CashierDAL
    {
        public static CashierDTO GetCashier(int idCashier)
        {
            return new CashierDTO()
            {
                idCashier = idCashier,
                description = idCashier.ToString(),
                coinBalanceDTOs = new List<CoinBalanceDTO>()
            };
        }
    }
}
