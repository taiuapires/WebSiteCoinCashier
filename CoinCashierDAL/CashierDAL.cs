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
                {
                    new CoinBalanceDTO()
                    {
                        quantity = 10,
                        coinValue = 50
                    },
                    new CoinBalanceDTO()
                    {
                        quantity = 7,
                        coinValue = 13
                    }
                }
            };
        }
    }
}
