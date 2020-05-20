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

        //Takes in an int and checks to see if wallet contains appropriate coin
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

        //Takes in an int and removes a coin of the appropriate type
        //Does NOT check to see if wallet has the coin, that occurs in Customer's ChooseCoinstoDeposit method
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
