using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachineProject;

namespace SodaMachineTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SodaMachine_Execute_PayForOrange_WithNickelAndPenny_GetStatus3()
        {

            //Arrange
            SodaMachine soda = new SodaMachine();
            Customer cust = new Customer();
            int expected = 3;
            int actual;

            //Act
            List<Coin> coins = new List<Coin>() { new Nickel(), new Penny() };

            actual = soda.Execute(cust, "Orange", coins);

            //Assert
            Assert.AreEqual(expected, actual);

        }
    }
}
