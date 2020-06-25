using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachineProject
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
            AddCoinsToWallet(5, "quarter");
            AddCoinsToWallet(5, "dime");
            AddCoinsToWallet(5, "nickel");
            AddCoinsToWallet(5, "penny");
        }

        public void AddCoinsToWallet(int numOfCoins, string coin)
        {
            for (int i = 0; i < numOfCoins; i++)
            {
                if (coin == "quarter")
                {
                    coins.Add(new Quarter());
                }
                else if (coin == "dime")
                {
                    coins.Add(new Dime());
                }
                else if (coin == "nickel")
                {
                    coins.Add(new Nickel());
                }
                else if (coin == "penny")
                {
                    coins.Add(new Penny());
                }
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
                if (coins[i].name == coinName)
                {
                    coins.RemoveAt(i);
                    break;
                }
            }
        }

        public void AcceptCoins(List<Coin> returnedAmount)
        {
            foreach (Coin coin in returnedAmount)
            {
                coins.Add(coin);
            }
        }
    }
}
