using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SodaTesting
{
    public class SodaMachine
    {
        public List<Coin> register;
        public List<Can> inventory;

        public SodaMachine()
        {
            register = new List<Coin>();
            inventory = new List<Can>();
            FillRegister();
            FillStock();
        }

        private void FillRegister()
        {
            AddCoinsToRegister(10, new Dime());
            AddCoinsToRegister(20, new Quarter());
            AddCoinsToRegister(20, new Nickel());
            AddCoinsToRegister(50, new Penny());
        }

        private void FillStock()
        {
            for (int i = 0; i < 12; i++)
            {
                inventory.Add(new Cola());
                inventory.Add(new RootBeer());
                inventory.Add(new OrangeSoda());
            }
        }

        private bool ContainsCoin(string coinName)
        {
            bool found = false;
            foreach(Coin coin in register)
            {
                if(coin.name == coinName)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        private bool ContainsCan(string canName)
        {
            bool found = false;
            foreach (Can can in inventory)
            {
                if (can.name == canName)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        private void AddCoinsToRegister(int numOfCoins, Coin coin)
        {
            for (int i = 0; i < numOfCoins; i++)
            {
                register.Add(coin);
            }
        }


        public void AcceptPayment(List<Coin> deposit)
        {
            foreach(Coin coin in deposit)
            {
                register.Add(coin);
            }
        }


        public bool ProcessSale(Customer customer, string sodaChoice, List<Coin> deposit)
        {
            bool successfulSale = false;
            AcceptPayment(deposit);
            if (ContainsCan(sodaChoice))
            {
                //check deposit vs price
                if(MoneyValue.CheckValue(deposit) == )


            }
            else
            {
                //No soda available, refund change
                customer.wallet.AcceptCoins(deposit);
            }


            //Check for soda stock
            //If in stock:
            //Check deposit vs price
            //If price over, make change
            //If not in stock, reprompt

            return successfulSale;
        }




    }
}
