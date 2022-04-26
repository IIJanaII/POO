using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace problem
{
    public class Dictionnaire
    {
        //attributs
        private string langue;
        private string[] mondico;

        //constructeur
        public Dictionnaire(string langue)
        {
            this.langue = langue;

            int n = 0;      //index initialisé à 0

            string s = File.ReadAllText("Francais.txt");

            string[] mot = s.Split(' ');            // on récupère chaque mot séparé par un espace

            string[] dico = new string[mot.Length];            //Création d'un tableau de dimension correspondant au nb de mots du dico

            foreach (string element in mot)
            {
                dico[n] = element;
                n++;
            }

            mondico = dico;                     //récupération des données pour remplir l'attribut mondico
        }

        //propriétés
        public string[] Mondico
        {
            get { return this.mondico; }
            set { this.mondico = value; }
        }

        //méthodes

        public override string ToString()
        {
            int i = 15; // initialise à 15 maximum de lettres =15     

            int j = 0; //indique le numero du compteur 
            int cpt = 0; //initialise à 0
            int[] TabCompteur = new int[14]; //tableau de compteur a 13 car (15-1), un compteur pour chaque taille de mot

            for (int k = mondico.Length - 1; k >= 0; k--) //parcourt le tableau de mot à l'envers 
            {
                cpt++;

                if (mondico[k].Contains(i.ToString()))  // lorsque la case contient un chiffre on réinitialise le compteur   
                {

                    TabCompteur[j] = cpt;  //sauvegarde du nombre de mot comptes précédent
                    cpt = -1; // car compteur déjà incrémenté avant de rentrer dans la boucle if 
                    j++; //change de compteur 


                    i--;

                }
            }

            string s = "Langue du dictionnaire : " + langue + "\n\n";
            for (int m = 0; m < 14; m++)
            {
                s += "Nombre de mots à " + (15 - m) + " lettres : " + TabCompteur[m] + "\n";
            }
            return s;
        }

        public bool VerificationChiffre(string mot)                 //On créé une fonction de vérification dans le cas où la case contenant le mot contiendrait aussi un chiffre
        {
            string[] tabChiffre = new string[13];
            int a = 2;

            for (int i = 0; i < 13; i++)            //On remplit le tableau avec tous les chiffres possibles du fichier "francais.txt" c'est-à-dire un chiffre compris entre 2 et 15 
            {
                tabChiffre[i] = Convert.ToString(a);
                a++;
            }

            for (int j = 0; j < 13; j++)
            {
                if (Convert.ToString(mot[0]).Contains(tabChiffre[j]))                //Si la première lettre du mot contenu dans le dico correspondant au mot choisit par le joueur est un chiffre alors on renvoit true;
                {
                    return true;
                }
            }

            return false;               //return false si le mot du dico correspondant au mot choisit par le joueur ne contient pas de chiffre
        }

        public bool RechDichoRecursif(int debut, int fin, string mot)
        {
            bool present = false;

            int m = (debut + fin) / 2;


            if (mondico[m].Contains(mot))         //On a contains car il se peut qu'un chiffre soit contenu dans la case et on vérifie que ce soit bien le même mot en vérifiant la longueur
            {                                                                              //Dans ce cas là le mot contient un chiffre donc la longueur du mot doit être égale à Length - 1 (étant donné qu'il y a un chiffre en plus)
                if ((VerificationChiffre(mondico[m]) == true) && (mot.Length == mondico[m].Length - 1))          //si le mot du dico contient un chiffre
                {
                    return true;
                }
                //Dans ce cas là le mot ne contient pas de chiffre donc la longueur du mot doit être égale à Length du mot
                if ((VerificationChiffre(mondico[m]) == false) && (mot.Length == mondico[m].Length))          //si le mot du dico ne contient pas de chiffre
                {
                    return true;
                }
            }

            for (int i = 0; i < m; i++)
            {
                if (mondico[i].Contains(mot))         //On a contains car il se peut qu'un chiffre soit contenu dans la case et on vérifie que ce soit bien le même mot en vérifiant la longueur
                {                                                                              //Dans ce cas là le mot contient un chiffre donc la longueur du mot doit être égale à Length - 1 (étant donné qu'il y a un chiffre en plus)
                    if ((VerificationChiffre(mondico[i]) == true) && (mot.Length == mondico[i].Length - 1))          //si le mot du dico contient un chiffre
                    {
                        present = true;
                    }
                    //Dans ce cas là le mot ne contient pas de chiffre donc la longueur du mot doit être égale à Length du mot
                    if ((VerificationChiffre(mondico[i]) == false) && (mot.Length == mondico[i].Length))          //si le mot du dico ne contient pas de chiffre
                    {
                        present = true;
                    }
                }

            }

            if (present == true)
            {
                if (m == 0)
                {
                    return false;
                }
                return RechDichoRecursif(debut, m - 1, mot);
            }

            if (present == false)
            {
                if (m == mondico.Length - 1)
                {
                    return false;
                }
                return RechDichoRecursif(m + 1, fin, mot);
            }

            return false;
        }




    }
}

