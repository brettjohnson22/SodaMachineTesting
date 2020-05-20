using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaTesting
{
    static class UserInterface
    {
        public static string DisplayCoinOptions()
        {
            Console.WriteLine("Please input desired coins"
                + "\nPress 1 for Quarter"
                + "\nPress 2 for Dime"
                + "\nPress 3 for Nickel"
                + "\nPress 4 for Penny"
                + "\nPress 5 when you are done"
                + "\nPress 6 to cancel and refund"
                );
            string coinChoice = Console.ReadLine();
            return coinChoice;
        }
        public static string ChooseSoda()
        {
            Console.WriteLine("Sodas offered: Cola, Orange, Root Beer"
                + "\nPress 1 for Cola"
                + "\nPress 2 for Orange"
                + "\nPress 3 for Root Beer"
                );
            string sodaChoice = Console.ReadLine();
            return sodaChoice;
        }
        public static string AskBuyAnotherSoda()
        {
            Console.WriteLine("Do you want to purchase another soda?"
                + "\n Press 1 for yes"
                + "\n Press 2 for no");
            string buyAnotherChoice = Console.ReadLine();
            return buyAnotherChoice;
        }
        public static void DisplayValue(string message, List<Coin> coins)
        {
            Console.WriteLine($"Total Amount {message}: {MoneyValue.CheckValue(coins)}");
        }
        public static void NoCoinMessage(int coinChoice)
        {
            string coinName = UserInterface.DecodeCoinSelection(coinChoice);
            Console.WriteLine($"You don't have any coins of type: {coinName}!");
        }

        public static string DecodeCoinSelection(int consoleChoice)
        {
            string coinName = "";
            switch (consoleChoice)
            {
                case 1:
                    coinName = "quarter";
                    break;
                case 2:
                    coinName = "dime";
                    break;
                case 3:
                    coinName = "nickel";
                    break;
                case 4:
                    coinName = "penny";
                    break;
            }
            return coinName;
        }
    }
}
