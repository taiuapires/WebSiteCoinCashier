using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClasses.DTO
{
    public class CoinBalanceDTO
    {
        public decimal value { get; set; }
        public int ammount { get; set; }
        public decimal totalValue
        {
            get
            {
                return value * ammount;
            }
        }
    }
}
