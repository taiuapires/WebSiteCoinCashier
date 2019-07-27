using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClasses.DTO
{
    public class CoinBalanceDTO
    {
        public int? idCoinBalance { get; set; }
        public int coinValue { get; set; }
        public int quantity { get; set; }
        public int totalValue
        {
            get
            {
                return coinValue * quantity;
            }
        }
    }
}
