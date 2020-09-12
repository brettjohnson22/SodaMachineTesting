using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachineExam
{
    class SodaMachine
    {
        //membervariables (HAS A)
        private List<Coin> register;
        private List<SodaCan> stock;

        //constructor (SPAWNER)
        public SodaMachine()
        {
            GenerateRegister();
            GenerateStock();
        }

        //member methods (CAN DO)
        private void GenerateRegister()
        {
            register = new List<Coin>();
            for (int i = 0; i < 50; i++)
            {
                if (i < 10)
                {
                    register.Add(new Dime());
                }
                if (i < 20)
                {
                    register.Add(new Quarter());
                    register.Add(new Nickel());
                }
                if (i < 50)
                {
                    register.Add(new Penny());
                }
            }
        }
        private void GenerateStock()
        {
            stock = new List<SodaCan>();
            for(int i = 0; i < 12; i++)
            {
                stock.Add(new GrapeSoda());
                stock.Add(new OrangeSoda());
                stock.Add(new LemonSoda());
            }
        }
        private int DetermineValue(List<Coin> money)
        {
            int value = 0;
            foreach (Coin coin in money)
            {
                value += coin.coinValue;
            }
            return value;
        }
        private bool ContainsType<T>(T thing, List<T> group)
        {
            bool found = false;
            foreach(T item in group)
            {
                if (thing.GetType() == item.GetType())
                {
                    found = true;
                }
            }
            return found;
        }
        private void DispenseSoda(SodaCan choice, List<Coin> deposit, string message)
        {
            if (ContainsType(choice, stock))
            {
                int removeIndex = stock.FindIndex(c => c.name == choice.name);
                stock.RemoveAt(removeIndex);
                foreach (Coin coin in deposit)
                {
                    register.Add(coin);
                }
                UserInterface.InjectedMessage(message);
            }
            else
            {
                UserInterface.OutOfStock();
            }
        }
        private int GiveChange(int changeValue)
        {
            Quarter quarter = new Quarter();
            Dime dime = new Dime();
            Nickel nickel = new Nickel();
            Penny penny = new Penny();
            List<Coin> change = new List<Coin>();
            while (changeValue > 0)
            {
                if (changeValue >= quarter.coinValue && ContainsType(quarter, register))
                {
                    changeValue = UpdateChange(quarter, change, changeValue);
                }
                else if (changeValue >= dime.coinValue && ContainsType(dime, register))
                {
                    changeValue = UpdateChange(dime, change, changeValue);
                }
                else if (changeValue >= nickel.coinValue && ContainsType(nickel, register))
                {
                    changeValue = UpdateChange(nickel, change, changeValue);
                }
                else if (changeValue >= penny.coinValue && ContainsType(penny, register))
                {
                    changeValue = UpdateChange(penny, change, changeValue);
                }
                else if (changeValue < register.Min(c => c.coinValue)) //Rare instance where machine doesn't have small enough coins to refund.
                {
                    changeValue = -1;
                    change.Clear();
                }
            }
            return DetermineValue(change);
        }
        private int UpdateChange<T>(T coin, List<Coin> change, int changeValue) where T : Coin, new()
        {
            int removeIndex = register.FindIndex(c => c.coinValue == coin.coinValue);
            register.RemoveAt(removeIndex);
            change.Add(new T());
            changeValue -= coin.coinValue;
            return changeValue;
        }
        private void OverPayment(SodaCan choice, List<Coin> deposit, int payment)
        {
            int changeValue = payment - choice.cost;
            if (changeValue <= DetermineValue(register))
            {
                int change = GiveChange(changeValue);
                if (change > 0)
                {
                    DispenseSoda(choice, deposit, $"Collect {change} cents change below. Enjoy your soda!");
                }
                else
                {
                    UserInterface.OutOfChange();
                }
            }
            else
            {
                UserInterface.OutOfChange();
            }
        }
        public void BuyASoda(SodaCan choice, List<Coin> deposit)
        {
            int payment = DetermineValue(deposit);
            if (payment < choice.cost)
            {
                UserInterface.NotEnough();
            }
            if (payment == choice.cost)
            {
                DispenseSoda(choice, deposit, "Thanks for using exact change. Enjoy your soda!");
            }
            if (payment > choice.cost)
            {
                OverPayment(choice, deposit, payment);
            }
        }
    }
}
