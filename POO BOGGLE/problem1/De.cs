using System;
using System.Collections.Generic;
using System.Text;

namespace problem
{
    public class De
    {
        //attributs
        private char faceTiree;
        private char[] face;


        //Constructeurs
        public De(char[] face)
        {
            char[] tabInter = new char[face.Length];

            for (int i = 0; i < face.Length; i++)
            {
                tabInter[i] = face[i];
            }

            this.face = tabInter;


        }
        //propriete
        public char Facetiree
        {
            get { return this.faceTiree; }
            set { this.faceTiree = value; }
        }


        //methodes
        public void Lance(Random r)
        {
            faceTiree = face[r.Next(0, 6)];

        }
        public override string ToString()
        {
            return "face tirée :" + faceTiree;
        }


    }
}
