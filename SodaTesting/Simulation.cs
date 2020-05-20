using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaTesting
{
    public class Simulation
    {
        public Customer customer;
        public SodaMachine sodaMachine;

        public Simulation()
        {
            customer = new Customer();
            sodaMachine = new SodaMachine();
        }

        public void RunSim()
        {
            sodaMachine.AddSomeCoins();
            Console.ReadLine();


        }
    }
}
