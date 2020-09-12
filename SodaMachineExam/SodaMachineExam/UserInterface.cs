using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachineExam
{
    static class UserInterface
    {
        public static void InjectedMessage(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
        public static void OutOfStock()
        {
            Console.WriteLine("Out of stock, refunding payment.");
            Console.ReadLine();
        }
        public static void NotEnough()
        {
            Console.WriteLine("Not enough depostied, refunding payment.");
            Console.ReadLine();
        }
        public static void OutOfChange()
        {
            Console.WriteLine("Not enough change, refunding payment.");
            Console.ReadLine();
        }
    }
}
