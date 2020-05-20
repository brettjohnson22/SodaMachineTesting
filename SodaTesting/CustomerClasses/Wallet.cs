using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaTesting
{
    public class Wallet
    {
        public List<Coin> coins;

        public Wallet()
        {
            coins = new List<Coin>();
            PickUpChange();
        }


        public void PickUpChange()
        {
            for (int i = 0; i < 20; i++)
            {
                coins.Add(new Dime());
                coins.Add(new Quarter());
                coins.Add(new Nickel());
                coins.Add(new Penny());
            }
        }

    }
}
