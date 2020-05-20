using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachineProject
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

        //Overload of constructor to simulate different conditions for testing
        public SodaMachine(string testCondition)
        {
            register = new List<Coin>();
            inventory = new List<Can>();
            if (testCondition == "nomoney")
            {
                //only cans, no money
                FillStock();
            }
            else if (testCondition == "nosoda")
            {
                //only money, no cans
                FillRegister();
            }
            else if (testCondition == "nothing")
            {
                //no cans or money
            }

        }

        private void FillRegister()
        {
            AddCoinsToRegister(20, new Quarter());
            AddCoinsToRegister(10, new Dime());
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
        private double AcceptCoins(List<Coin> deposit)
        {
            foreach (Coin coin in deposit)
            {
                register.Add(coin);
            }
            return UserInterface.CheckValue(deposit);
        }

        //Main method that allows a customer to try to purchase a soda
        //Will result in the 5 possibilities mentioned in user story depending on parameters and state of sodamachine's inventory/register
        public int Execute(Customer customer, string sodaChoice, List<Coin> deposit)
        {
            double payment = AcceptCoins(deposit);

            Can selection = PrepareCan(sodaChoice);

            double change = DetermineAmountOfChange(selection, payment);

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
                    List<Coin> refund = CreateChange(change);
                    EjectCoins(refund);
                    customer.wallet.AcceptCoins(refund);
                    break;
                case 5:
                    //Machine out of change, return deposit
                    EjectCoins(deposit);
                    customer.wallet.AcceptCoins(deposit);
                    break;
                default:
                    //We shouldn't get here, but return deposit just incase
                    EjectCoins(deposit);
                    customer.wallet.AcceptCoins(deposit);
                    break;
            }

            //Displays console message informing user of results
            UserInterface.DecodeStatusCode(statusCode, sodaChoice, change, payment);

            //return value is not captured in program, but can be tested
            return statusCode;
        }

        //Compares payment to price of can and returns the difference, positive, negative, or zero
        private double DetermineAmountOfChange(Can can, double payment)
        {
            double change = payment - can.Cost;
            return change;
        }

        //Returns ints 1-5 as status code of attempted sale. See Execute method for status code explanations
        private int AttemptSale(Can selection, double change)
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
                    //Attempt to make change to see if register has enough coins, but can't return them yet so put them back in reg
                    List<Coin> refund = CreateChange(change);
                    if (refund.Count > 0)
                    {
                        statusCode = 4;
                        AcceptCoins(refund);
                    }
                    else
                    {
                        statusCode = 5;
                    }

                }
            }
            return statusCode;
        }

        //Takes in a string and returns a can of that type if available.
        //If stock does not contain that type of can, this will return null
        private Can PrepareCan(string canChoice)
        {
            Can actualCan = null;
            switch (canChoice)
            {
                case "Cola":
                    Can cola = new Cola();
                    if (ContainsCan(cola))
                    {
                        actualCan = cola;
                    }
                    break;
                case "Orange Soda":
                    Can orange = new OrangeSoda();
                    if (ContainsCan(orange))
                    {
                        actualCan = orange;
                    }
                    break;
                case "Root Beer":
                    Can rootbeer = new RootBeer();
                    if (ContainsCan(rootbeer))
                    {
                        actualCan = rootbeer;
                    }
                    break;
            }
            return actualCan;
        }

        private void DispenseSodaToCustomer(Customer customer, Can selection)
        {
            customer.backpack.cans.Add(selection);
            inventory.Remove(selection);
        }
        //Takes in a list of coins and removes the from register
        //This does NOT test to see if register actually contains those coins
        private void EjectCoins(List<Coin> coins)
        {
            foreach (Coin coin in coins)
            {
                register.Remove(coin);
            }
        }

        //Creates a list of Coins equal to the value of parameter
        //Will return empty list if insufficient change exists
        private List<Coin> CreateChange(double changeAmount)
        {
            List<Coin> refund = new List<Coin>();

            foreach (Coin coin in register.ToList())
            {
                changeAmount = Math.Round(changeAmount, 2);
                if (coin.Value == 0.25 && changeAmount >= 0.25)
                {
                    changeAmount -= 0.25;
                    register.Remove(coin);
                    refund.Add(coin);
                }
                else if (coin.Value == 0.10 && changeAmount >= 0.10)
                {
                    changeAmount -= 0.10;
                    register.Remove(coin);
                    refund.Add(coin);
                }
                else if (coin.Value == 0.05 && changeAmount >= 0.05)
                {
                    changeAmount -= 0.05;
                    register.Remove(coin);
                    refund.Add(coin);
                }
                else if (coin.Value == 0.01 && changeAmount >= 0.01)
                {
                    changeAmount -= 0.01;
                    register.Remove(coin);
                    refund.Add(coin);
                }
            }
            changeAmount = Math.Round(changeAmount, 2);
            if (changeAmount != 0)
            {
                AcceptCoins(refund);
                refund.Clear();
            }
            return refund;

        }

    }
}
