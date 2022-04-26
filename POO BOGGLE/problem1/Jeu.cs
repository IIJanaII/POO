using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace problem
{
    public class Jeu
    {
        //Attributs
        private static Dictionnaire mondico = new Dictionnaire("Français");
        private static Plateau monplateau = new Plateau();


        static void Menu(ref string pseudoA, ref string pseudoB)
        {
            Console.WriteLine("\t\t\tBienvenue au jeu du BOOGLE\n\n\n" +
                "1. Nouvelle partie de BOOGLE\n" +
                "2. Règles du BOOGLE\n" +
                "3. Quitter le jeu");

            // try                 //Try and Catch pour blinder la saisie dans le menu
            // {

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("\t\t\tNouvelle partie de BOOGLE\n\n");
                    Console.WriteLine("Saisir le pseudo du joueur A");
                    pseudoA = Console.ReadLine();
                    Console.WriteLine("Saisir le pseudo du joueur B");
                    pseudoB = Console.ReadLine();
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("\t\t\tRègles du BOOGLE\n\n");

                    Console.WriteLine("Durée d'une partie : 6 minutes\n");

                    Console.WriteLine("Chaque joueur joue l’un après l’autre pendant un laps de temps de 1 mn.  Chaque joueur cherche des mots pouvant être formés à partir de lettres adjacentes du plateau.\nPar adjacente, " +
                        "il est sous-entendu horizontalement, verticalement ou en diagonale. Les mots doivent être de 3 lettres au minimum, peuvent être au singulier ou au pluriel,\nconjugués ou non, mais ne doivent pas utiliser plusieurs " +
                        "fois le même dé pour le même mot.\nLes joueurs saisissent tous les mots qu’ils ont trouvés au clavier. Un score par joueur est mis à jour à chaque mot trouvé et validé.\n\n");
                    Console.WriteLine("Scores (en fonction de la longueur du mot) : \n" +
                        "3 lettres : 2 pts\n" +
                        "4 lettres : 3 pts\n" +
                        "5 lettres : 4 pts\n" +
                        "6 lettres : 5 pts\n" +
                        "7 et + lettres : 11 pts\n\n");


                    Console.WriteLine("Remarque :\n");
                    Console.WriteLine("Spécificités du dictonnaire selectionné :");
                    Console.WriteLine(mondico.ToString());

                    Console.WriteLine("\nAppuyer sur un caractère pour revenir au menu");
                    Console.ReadKey();
                    Console.Clear();
                    Menu(ref pseudoA, ref pseudoB);

                    break;

                case "3":
                    System.Environment.Exit(1);
                    break;

                default:
                    Console.Clear();
                    Menu(ref pseudoA, ref pseudoB);
                    break;


            }
            //   }

            /*  catch (FormatException)
              {
                  Console.Clear();
                  Menu(ref pseudoA, ref pseudoB);
              }*/




        }

        static void DeroulementJeu(Joueur joueurA, Joueur joueurB, DateTime tempsFiniPartie)
        {

            Stack<int> x = new Stack<int>();
            Stack<int> y = new Stack<int>();
            Stack<int> x2 = new Stack<int>();
            Stack<int> y2 = new Stack<int>();


            //Récupération de toutes les données nécessaire au Boogle
            Plateau monplateau = new Plateau();
            monplateau.AffichagePlateau(); //affichage du nouveau plateau

            Console.WriteLine();


            //initialisation du timer de une minute
            DateTime tempsFini = DateTime.Now.AddMinutes(1);
            DateTime TempsNow = DateTime.Now;
            TimeSpan zero = TimeSpan.FromSeconds(0);

            while (tempsFini - TempsNow > zero)
            {

                Console.WriteLine("C'est au tour de " + joueurA.Nom + " de jouer");
                Console.WriteLine("Saisissez un nouveau mot trouvé : ");
                string mot = Console.ReadLine().ToUpper();

                TempsNow = DateTime.Now;

                if (tempsFiniPartie - TempsNow <= zero)              //Verificatin délai de 6 minutes de la partie
                {
                    break;
                }

                if ((mot.Length >= 3) && (mondico.RechDichoRecursif(0, mondico.Mondico.Length, mot) == true) && (monplateau.Test_Plateau(mot, 1, 0, false, x, y, 0, 0, 0, false, x2, y2, 0, 0) == true))
                {
                    if ((joueurA.Contain(mot) == true) || (joueurB.Contain(mot) == true))
                    {
                        Console.WriteLine("Vous avez déjà joué ce mot");
                    }

                    if ((joueurA.Contain(mot) == false) && (joueurB.Contain(mot) == false))
                    {

                        if ((tempsFini - TempsNow) > zero)
                        {
                            joueurA.Add_Mot(mot);
                            joueurA.ComptageScore(mot);
                            Console.WriteLine("Bien joué " + joueurA.Nom + " votre mot est valide et votre score actuel est de " + joueurA.Score + " grâce aux mots suivants: ");

                            foreach (string element in joueurA.MotTrouve)       //Affichage des mots trouvés par le joueur
                            {
                                if (element != null)
                                {
                                    Console.Write(element + " ");
                                }

                            }

                        }
                        else
                        {

                            Console.WriteLine("Le délai d'une minute est écoulé");
                            Console.WriteLine("Votre mot n'est donc pas pris en compte");
                        }
                    }

                    Console.WriteLine(" ");
                }

                else
                {
                    if ((tempsFini - TempsNow) <= zero)
                    {
                        Console.WriteLine("Le délai d'une minute est écoulé");
                    }

                    if (mot.Length <= 2)
                    {
                        Console.WriteLine("La taille du mot trouvé doit être supérieure ou égale à 3");
                    }

                    else
                    {
                        Console.WriteLine("Mot incorrect");
                    }

                    Console.WriteLine("Votre mot n'est donc pas pris en compte");



                }

                Console.WriteLine();
            }

            string[] NewtabMot = new string[20];            //on initialise un nouveau tableau pour stocker les mots trouvés par le joueur à chaque tour
            joueurA.MotTrouve = NewtabMot;

            Console.WriteLine("Le tour de " + joueurA.Nom + " est terminé");

        }


        public static void Main(string[] args)
        {


            string pseudoA = null;
            string[] motTrouveA = new string[1000];
            string pseudoB = null;
            string[] motTrouveB = new string[1000];

            Menu(ref pseudoA, ref pseudoB);

            Joueur joueurA = new Joueur(pseudoA, 0, motTrouveA);
            Joueur joueurB = new Joueur(pseudoB, 0, motTrouveB);


            //initialisation du timer de 6 minutes
            DateTime tempsFiniPartie = DateTime.Now.AddMinutes(3);
            DateTime TempsNowPartie = DateTime.Now;
            TimeSpan zero = TimeSpan.FromSeconds(0);


            Console.Clear();

            Random tirage = new Random();       //tirage du joueur qui commence
            int nb = tirage.Next(1, 3);

            if (nb == 1)            //Le joueur A commence
            {
                int i = 0;

                Console.WriteLine("C'est " + joueurA.Nom + " qui commence, BONNE CHANCE !");

                while (tempsFiniPartie - TempsNowPartie > zero)
                {
                    i++;            //Comptage de tours

                    TempsNowPartie = DateTime.Now;

                    if ((tempsFiniPartie - TempsNowPartie) <= zero)
                    {
                        break;
                    }

                    else
                    {
                        Console.WriteLine("\n\nTour " + i + "\n");
                        DeroulementJeu(joueurA, joueurB, tempsFiniPartie);
                        i++;
                    }

                    TempsNowPartie = DateTime.Now;

                    if ((tempsFiniPartie - TempsNowPartie) <= zero)
                    {
                        break;
                    }

                    else
                    {
                        Console.WriteLine("\n\nTour " + i + "\n");
                        DeroulementJeu(joueurB, joueurA, tempsFiniPartie);
                    }


                }

                Console.WriteLine("\n\n");
            }

            if (nb == 2)            //Le joueur B commence
            {
                int i = 0;

                Console.WriteLine("C'est " + joueurB.Nom + " qui commence, BONNE CHANCE !");

                while (tempsFiniPartie - TempsNowPartie > zero)
                {
                    i++;            //Comptage de tours

                    TempsNowPartie = DateTime.Now;

                    if ((tempsFiniPartie - TempsNowPartie) <= zero)
                    {
                        break;
                    }

                    else
                    {
                        Console.WriteLine("\n\nTour " + i + "\n");
                        DeroulementJeu(joueurB, joueurA, tempsFiniPartie);
                        i++;
                    }

                    TempsNowPartie = DateTime.Now;

                    if ((tempsFiniPartie - TempsNowPartie) <= zero)
                    {
                        break;
                    }

                    else
                    {
                        Console.WriteLine("\n\nTour " + i + "\n");
                        DeroulementJeu(joueurA, joueurB, tempsFiniPartie);
                    }
                }

                Console.WriteLine("\n\n");
            }

            Console.WriteLine("\n\nLe délai de 6 min est écoulé, la partie est terminée !\n\n");

            Console.WriteLine("\t\t\tRésulats :\n\n");

            Console.WriteLine(pseudoA + " a obtenu " + joueurA.Score + " points !");
            Console.WriteLine((pseudoB + " a obtenu " + joueurB.Score + " points !\n\n"));

            if (joueurA.Score > joueurB.Score)
            {
                Console.WriteLine("Le vainqueur est " + joueurA.Nom + ", FELICITATIONS !");
            }

            else
            {
                Console.WriteLine("Le vainqueur est " + joueurB.Nom + ", FELICITATIONS !");
            }


        }
    }
}
