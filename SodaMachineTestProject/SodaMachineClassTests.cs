using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachineProject;

namespace SodaMachineTestProject
{
    [TestClass]
    public class SodaMachineClassTests
    {
        [TestMethod]
        public void Execute_PayForOrange_WithNickelAndPenny_GetStatus3()
        {
            //Arrange
            SodaMachine soda = new SodaMachine();
            Customer cust = new Customer();
            int expected = 3;
            int actual;

            //Act
            List<Coin> coins = new List<Coin>() { new Nickel(), new Penny() };

            actual = soda.Execute(cust, "Orange Soda", coins);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Execute_PayForOrange_WithTwoQuarters_RegisterHas95CoinsAfterSale()
        {
            //100 coins in starting reg + 2 quarters payment - 7 coins as change = 95 coins 
            //Arrange
            SodaMachine soda = new SodaMachine();
            Customer cust = new Customer();
            int expected = 95;
            int actual;

            //Act
            List<Coin> coins = new List<Coin>() { new Quarter(), new Quarter() };

            soda.Execute(cust, "Orange Soda", coins);

            actual = soda.register.Count;
           
            //Assert
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void Execute_e()
        {
            //100 coins in starting reg + 2 quarters payment - 7 coins as change = 95 coins 
            //Arrange
            SodaMachine soda = new SodaMachine();
            Customer cust = new Customer();
            int expected = 35;
            int actual;

            //Act
            List<Coin> coins = new List<Coin>() { new Quarter(), new Quarter() };

            soda.Execute(cust, "Orange Soda", coins);

            actual = soda.inventory.Count;

            //Assert
            Assert.AreEqual(expected, actual);

        }

    }
}
