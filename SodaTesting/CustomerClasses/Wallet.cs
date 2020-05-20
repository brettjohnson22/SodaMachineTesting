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

        private void PickUpChange()
        {
            for (int i = 0; i < 5; i++)
            {
                coins.Add(new Dime());
                coins.Add(new Quarter());
                coins.Add(new Nickel());
                coins.Add(new Penny());
            }
        }

        public bool ContainsCoin(int coinChoice)
        {
            bool found = false;
            string coinName = UserInterface.DecodeCoinSelection(coinChoice);
            foreach (Coin coin in coins)
            {
                if (coin.name == coinName)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        public void RemoveCoin(int coinChoice)
        {
            string coinName = UserInterface.DecodeCoinSelection(coinChoice);
            for (int i = 0; i < coins.Count; i++)
            {
                if(coins[i].name == coinName)
                {
                    coins.RemoveAt(i);
                    break;
                }
            }
        }

        public void AcceptCoins(List<Coin> returnedAmount)
        {
            foreach(Coin coin in returnedAmount)
            {
                coins.Add(coin);
            }
        }

    }
}
