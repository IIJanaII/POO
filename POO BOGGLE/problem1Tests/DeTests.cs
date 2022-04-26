using Microsoft.VisualStudio.TestTools.UnitTesting;
using problem;
using System;
using System.Collections.Generic;
using System.Text;

namespace problem.Tests
{
    [TestClass()]
    public class DeTests
    {
       

        /*[TestMethod()]
        public void LanceTest()
        {
            Assert.Fail();
        }*/

        [TestMethod()]
        public void ToStringTest()
        {

            char[] face = new char[] { 'B', 'A', 'J', 'O', 'Q', 'M' };
            De de = new De(face);
            de.Facetiree = face[5];
            
            Assert.AreEqual("face tirée :" + face[5], de.ToString());
            
        }
    }
}