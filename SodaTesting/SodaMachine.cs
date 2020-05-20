using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        //Overload the constructor with a string to simulate empty register/inventory
        public SodaMachine(string emptyMachine)
        {
            register = new List<Coin>();
            inventory = new List<Can>();
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
            foreach (Coin coin in register)
            {
                if (coin.name == coinName)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        //Checks to see if a certain type of can exists in inventory
        private bool ContainsCan(Can selection)
        {
            bool found = false;
            foreach (Can can in inventory)
            {
                if (can.name == selection.name)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        //Used by constructor to add a certain amount of any coin
        private void AddCoinsToRegister(int numOfCoins, Coin coin)
        {
            for (int i = 0; i < numOfCoins; i++)
            {
                register.Add(coin);
            }
        }

        //Takes a list of coins and adds them to internal register
        public double AcceptPayment(List<Coin> deposit)
        {
            foreach (Coin coin in deposit)
            {
                register.Add(coin);
            }
            return UserInterface.CheckValue(deposit);
        }

        //Main method that allows a customer to try to purchase a soda
        //Will result in 5 possibilities depending on parameters
        public void Execute(Customer customer, string sodaChoice, List<Coin> deposit)
        {
            double payment = AcceptPayment(deposit);

            Can selection = PrepareCan(sodaChoice);

            double change = DetermineChange(selection, payment);

            int statusCode = AttemptSale(selection, change);

            switch (statusCode)
            {
                case 1:
                    //No stock, return deposit to customer's wallet
                    EjectCoins(deposit);
                    customer.wallet.AcceptCoins(deposit);
                    break;
                case 2:
                    //Insufficient payment, return deposit to customer's wallet
                    EjectCoins(deposit);
                    customer.wallet.AcceptCoins(deposit);
                    break;
                case 3:
                    //Exact change, dispense soda to customer's backpack
                    DispenseSodaToCustomer(customer, selection);
                    break;
                case 4:
                    //Overpayed, dispense soda to backpack and change to wallet
                    DispenseSodaToCustomer(customer, selection);
                    break;
                case 5:
                    //Machine out of change, return deposit
                    EjectCoins(deposit);
                    customer.wallet.AcceptCoins(deposit);
                    break;
                default:
                    customer.wallet.AcceptCoins(deposit);
                    break;
            }

            //Displays console message informing user of results
            UserInterface.DecodeStatusCode(statusCode);
        }

        //Compares payment to price and returns the difference, positive, negative, or zero
        public double DetermineChange(Can can, double payment)
        {
            double change = can.Cost - payment;
            return change;
        }

        //Returns 1-5 as status code of attempted sale
        public int AttemptSale(Can selection, double change)
        {
            int statusCode = 0;
            if (selection == null)
            {
                statusCode = 1;
            }
            else
            {
                if (change < 0)
                {
                    statusCode = 2;
                }
                if (change == 0)
                {
                    statusCode = 3;
                }
                else if (change > 0)
                {
                    //if machine has change, statuscode 4
                    //if not, statuscode 5

                }
            }
            return statusCode;
        }

        //Takes in a string and returns a can of that type if available.
        //If stock does not contain that type of can, this will return null
        public Can PrepareCan(string canChoice)
        {
            Can actualCan = null;
            switch (canChoice)
            {
                case "cola":
                    Can cola = new Cola();
                    if (ContainsCan(cola))
                    {
                        actualCan = cola;
                    }
                    break;
                case "orange":
                    Can orange = new OrangeSoda();
                    if (ContainsCan(orange))
                    {
                        actualCan = orange;
                    }
                    break;
                case "rootbeer":
                    Can rootbeer = new RootBeer();
                    if (ContainsCan(rootbeer))
                    {
                        actualCan = rootbeer;
                    }
                    break;
            }
            return actualCan;
        }

        public void DispenseSodaToCustomer(Customer customer, Can selection)
        {
            customer.backpack.cans.Add(selection);
            inventory.Remove(selection);
        }
        //Takes in a list of coins and removes the from register
        //This does NOT test to see if register actually contains those coins
        public void EjectCoins(List<Coin> coins)
        {
            foreach(Coin coin in coins)
            {
                register.Remove(coin);
            }
        }

        public bool CheckForChange(double amount)
        {
            while (amount != 0)
            {





            }
            return false;
        }

        public List<Coin> CreateChange(double changeAmount)
        {
            List<Coin> refund = new List<Coin>();

            while(changeAmount > 0)
            {
                if(changeAmount > .25)
                {
                    refund.Add(new Quarter());
                }
                else if(changeAmount > .10)
                {
                    refund.Add(new Dime());
                }
                else if (changeAmount > .05)
                {
                    refund.Add(new Nickel());
                }
                else if (changeAmount > .01)
                {
                    refund.Add(new Penny());
                }
            }
            return refund;

        }

    }
}
