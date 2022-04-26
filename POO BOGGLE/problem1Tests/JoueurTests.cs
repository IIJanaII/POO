using problem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace problem.Tests
{
    [TestClass()]
    public class JoueurTests
    {


        [TestMethod()]
        public void ContainTest()
        {
            string[] motTrouve = { "soda","noel" };
            Joueur joueurA = new Joueur("joueurA", 0, motTrouve);
            string mot = "noel";

            Assert.IsTrue(joueurA.Contain(mot));
        }

      

        [TestMethod()]
        public void ComptageScoreTest()
        {
            string mot = "noel";
            string[] motTrouve = new string[5];
            Joueur joueurA = new Joueur("joueurA", 0, motTrouve);
            joueurA.ComptageScore(mot);

            Assert.AreEqual(3, joueurA.Score);
          
        }
    }
}