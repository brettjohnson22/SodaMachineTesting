using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaTesting
{
    public class Customer
    {
        public Wallet wallet;
        public Backpack backpack;

        public Customer()
        {
            wallet = new Wallet();
            backpack = new Backpack();
        }

        //This large method relies on user input through Console.ReadLines, so it is not easily testable.
        //However, the many submethods that are called within it only rely on parameters being passed in
        //So they can each be individually tested by calling them with appropriate parameters
        private List<Coin> ChooseCoinsToDeposit()
        {
            List<Coin> deposit = null;
            bool input = true;
            int coinChoice;
            while (input)
            {
                bool success = Int32.TryParse(UserInterface.DisplayCoinOptions(), out coinChoice);
                Console.Clear();
                if (success && coinChoice > 0 && coinChoice < 5)
                {
                    if (wallet.ContainsCoin(coinChoice))
                    {
                        wallet.RemoveCoin(coinChoice);
                        deposit = DepositSingleCoin(coinChoice, deposit);
                    }
                    else
                    {
                        UserInterface.NoCoinMessage(coinChoice);
                    }
                }
                else if (coinChoice == 5)
                {
                    input = false;
                }
                else if (coinChoice == 6)
                {
                    wallet.AcceptCoins(deposit);
                }
                UserInterface.DisplayValue("deposited", deposit);
            }
            return deposit;
        }

        private List<Coin> DepositSingleCoin(int coinChoice, List<Coin> deposit)
        {
            if (deposit == null)
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
            List<Coin> coins = ChooseCoinsToDeposit();
        }

    }
}
