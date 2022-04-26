using System;
using System.Collections.Generic;
using System.Text;

namespace problem
{
    public class Joueur
    {
        //attributs
        private string nom;
        private int score = 0;
        private string[] motTrouve;
        //constructeur 
        public Joueur(string nom, int score, string[] motTrouve)
        {
            this.nom = nom;
            this.score = score;
            this.motTrouve = motTrouve;//ajouter le cas tableau null       

        }
        //Propriété
        public string Nom
        {
            get { return this.nom; }
            set { this.nom = value; }
        }
        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
        }
        public string[] MotTrouve
        {
            get { return this.motTrouve; }
            set { this.motTrouve = value; }
        }
        //methode
        public bool Contain(string mot)
        {
            foreach (string element in motTrouve)
            {
                if (mot == element)
                {
                    return true;
                }
            }

            return false;

        }
        public void Add_Mot(string mot)
        {
            for (int i = 0; i < motTrouve.Length; i++)
            {
                if (motTrouve[i] == null) // si la case est vide alors remplit la case par le mot trouve
                {
                    motTrouve[i] = mot;
                    break;
                }
            }
        }
        public bool Mot_Cite(string mot)
        {
            if (Contain(mot))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return "Le score de " + nom + " est de " + score + "grâce aux mots cités";
        }

        public void ComptageScore(string mot)
        {
            switch (mot.Length)
            {
                case 3:
                    this.score += 2;
                    break;
                case 4:
                    this.score += 3;
                    break;
                case 5:
                    this.score += 4;
                    break;
                case 6:
                    this.score += 5;
                    break;
                default:
                    this.score += 11;
                    break;
            }

        }







    }
}
