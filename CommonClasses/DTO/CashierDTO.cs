using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClasses.DTO
{
    public class CashierDTO
    {
        public int idCashier { get; set; }
        public string description { get; set; }
        public List<CoinBalanceDTO> coinBalanceDTOs { get; set; }
        public int totalCoinValueSum
        {
            get
            {
                if (coinBalanceDTOs != null)
                {
                    return coinBalanceDTOs.Sum(it => it.totalValue);
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
