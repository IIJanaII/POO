using Microsoft.VisualStudio.TestTools.UnitTesting;
using problem;
using System;
using System.Collections.Generic;
using System.Text;

namespace problem.Tests
{
    [TestClass()]
    public class PlateauTests
    {
       

        [TestMethod()]
        public void Test_PlateauTest()
        {
            Plateau Monplateau = new Plateau();
            Monplateau.Monplateau = new char[4, 4] { { 'S', 'B', 'C', 'D' }, { 'S', 'O', 'G', 'H' }, { 'I', 'J', 'D', 'L' }, { 'M', 'N', 'O', 'A' } };
            Stack<int> x = new Stack<int>();
            Stack<int> y = new Stack<int>();
            Stack<int> x2 = new Stack<int>();
            Stack<int> y2 = new Stack<int>();
            string mot = "SODA";
            Monplateau.Test_Plateau(mot, 1, 0, false, x, y, 0, 0, 0, false, x2, y2, 0, 0);
            Assert.AreEqual(true, Monplateau.Test_Plateau(mot, 1, 0, false, x, y, 0, 0, 0, false, x2, y2, 0, 0));
        }

     
    }
}