using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaTesting
{
    static class UserInterface
    {
        public static string DisplayOptions()
        {
            Console.WriteLine("Please input desired coins"
                + "\nPress 1 for Quarter"
                + "\nPress 2 for Dime"
                + "\nPress 3 for Nickel"
                + "\nPress 4 for Penny"
                + "\nPress 5 when you are done"
                );
            string coinChoice = Console.ReadLine();
            return coinChoice;
        }
        public static string ChooseSoda()
        {
            Console.WriteLine("Sodas offered: Grape, Orange, Lemon"
                + "\nPress 1 for Grape"
                + "\nPress 2 for Orange"
                + "\nPress 3 for Lemon"
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
            Console.WriteLine($"Total Amount {message}: {ValueCheck.CheckValue(coins)}");
        }

        
    }
}
