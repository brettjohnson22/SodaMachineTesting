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
            AddCoin(10, new Dime());
            AddCoin(20, new Quarter());
            AddCoin(20, new Nickel());
            AddCoin(50, new Penny());
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

        private void AddCoin(int numOfCoins, Coin coin)
        {
            for (int i = 0; i < numOfCoins; i++)
            {
                register.Add(coin);
            }
        }

        private List<Coin> ChooseCoins()
        {
            List<Coin> deposit = null;
            bool input = true;
            int coinChoice;
            while (input)
            {
                bool success = Int32.TryParse(UserInterface.DisplayOptions(), out coinChoice);
                if (success && coinChoice > 0 && coinChoice < 5)
                {
                    deposit = InputSingleCoin(coinChoice, deposit);
                }
                else if(coinChoice == 5)
                {
                    input = false;
                }
                Console.Clear();
                UserInterface.DisplayValue("deposited", deposit);
            }
            return deposit;
        }

        private List<Coin> InputSingleCoin(int coinChoice, List<Coin> deposit)
        {
            if(deposit == null)
            {
                deposit = new List<Coin>();
            }
            switch (coinChoice)
            {
                case 1:
                    deposit.Add(new Quarter());
                    break;
                case 2:
                    deposit.Add(new Dime());
                    break;
                case 3:
                    deposit.Add(new Nickel());
                    break;
                case 4:
                    deposit.Add(new Penny());
                    break;
                case 5:
                    break;
                default:
                    break;
            }
            return deposit;
        }





        public void AddSomeCoins()
        {
            //List<Coin> deposit = InputCoin(1);
            //deposit = InputCoin(2, deposit);
            //deposit = InputCoin(3, deposit);
            //UserInterface.DisplayValue("deposited", deposit);
            List<Coin> coins = ChooseCoins();
        }
    }
}
