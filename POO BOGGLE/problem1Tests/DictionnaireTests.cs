using Microsoft.VisualStudio.TestTools.UnitTesting;
using problem;
using System;
using System.Collections.Generic;
using System.Text;

namespace problem.Tests
{
    [TestClass()]
    public class DictionnaireTests
    {
       

        [TestMethod()]
        public void RechDichoRecursifTest()
        {
            Dictionnaire mondico = new Dictionnaire("Français");
            int debut = 0;
            int fin = mondico.Mondico.Length;
            string mot = "SODA";
            mondico.RechDichoRecursif(0, mondico.Mondico.Length, mot);
            Assert.AreEqual(true,mondico.RechDichoRecursif(debut, fin, mot));
        }
    }
}