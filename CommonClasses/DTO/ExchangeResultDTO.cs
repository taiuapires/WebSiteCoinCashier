using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClasses.DTO
{
    public class ExchangeResultDTO
    {
        public CashierDTO cashier { get; set; }

        public List<CoinChangeDTO> resultChange { get; set; }

        public int saleValue { get; set; }
        public int totalChange
        {
            get
            {
                return resultChange.Sum(it => it.coinValue);
            }
        }

        public int totalCoins
        {
            get
            {
                return resultChange.Sum(it => it.quantity);
            }
        }
    }
}
