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
                + "\nPress 1 for Cola, .35"
                + "\nPress 2 for Orange, .06"
                + "\nPress 3 for Root Beer, .60"
                );
            string input = Console.ReadLine();
            string sodaChoice = "";
            while (sodaChoice == "")
            {
                switch (input)
                {
                    case "1":
                        sodaChoice = "cola";
                        break;
                    case "2":
                        sodaChoice = "orange";
                        break;
                    case "3":
                        sodaChoice = "rootbeer";
                        break;
                }
            }
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
            Console.WriteLine($"Total Amount {message}: {UserInterface.CheckValue(coins)}");
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

        public static void DecodeStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 1:
                    Console.WriteLine("Selection unavailable. Take your change and try again.");
                    break;
                case 2:
                    Console.WriteLine("Insufficient payment. Take your change and try again");
                    break;
                case 3:
                    Console.WriteLine("Thanks for using exact change. Enjoy your soda!");
                    break;
                case 4:
                    Console.WriteLine("Be sure to grab your change. Enjoy your soda!"); 
                    break;
                case 5:
                    Console.WriteLine("Insufficient change to complete payment, collect your change. We apologize for the inconvenience.");
                    break;
                default:
                    Console.WriteLine("ERROR: Status Unknown");
                        break;

            }
        }

        public static double CheckValue(List<Coin> coins)
        {
            double totalValue = 0;
            foreach (Coin coin in coins)
            {
                totalValue += coin.Value;
            }
            return Math.Round(totalValue, 2);
        }
    }
}
