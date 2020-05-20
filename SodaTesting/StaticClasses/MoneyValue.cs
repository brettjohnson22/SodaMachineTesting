using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaTesting
{
    public static class ValueCheck
    {
        public static double CheckValue(List<Coin> coins)
        {
            double totalValue = 0;
            foreach(Coin coin in coins)
            {
                totalValue += coin.Value;
            }
            return totalValue;
        }

        public static double CheckPrice(List<Can> cans)
        {
            double totalValue = 0;
            foreach (Can can in cans)
            {
                totalValue += can.Cost;
            }
            return totalValue;
        }

    }
}
