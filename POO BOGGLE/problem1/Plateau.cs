using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace problem
{
    public class Plateau
    {
        //attributs
        private char[,] monplateau;

        //constructeur
        public Plateau()
        {
            int abs = 0;
            int ord = 0;
            int a = 0;


            char[,] tab = new char[4, 4];           //Initialisation d'un tableau intermédiaire pour remplir l'attibut mon plateau

            Random r = new Random();                //On initialise une variable aléatoire
            char[] face = new char[6];                  //Création d'un tableau de 6 cases contenant les 6 faces possibles d'un dé


            string[,] tableaudeDe = new string[16, 6];              //Tableau contenant toutes les possibilités de chacun des dés
            string[] lines = File.ReadAllLines("Des.txt");          //Stockage dans une case chaque ligne du fichier

            for (int k = 0; k < lines.Length; k++)          //On va parcourir les cases du tableau 'lines" une par une
            {
                string phrase = lines[k];           //On récupère la valeur d'une case contenant les possibilités pour un dé
                string[] TabInter = phrase.Split(';');              //On utilise split pour séparer les possibilités et on les stocke dans un tableau 

                if ((k % 6) == 0)               //Dès qu'on arrive à la 6ème possibilité du dé on passe au prochain dé
                {
                    a = 0;
                }

                for (int i = 0; i < 6; i++)
                {
                    tableaudeDe[a, i] = TabInter[i];            //On stocke les possibilités du dé dans une ligne du tableau
                    face[i] = Convert.ToChar(tableaudeDe[a, i]);            //On récupère chacune des possibilités et on les stocke dans le tableau de faces

                }

                De de = new De(face);           //On initialise une variable de de type DE qui va nous permettre de tirer une face au hasard
                de.Lance(r);

                if (ord == 4)           //Lorsqu'on arrive au 4ème dés de la ligne on passe à la ligne suivante
                {
                    abs++;
                    ord = 0;
                }

                tab[abs, ord] = de.Facetiree;       //On récupère la face tiréé dans la classe De et on la stocke dans le tableau intermédiaire


                ord++;
                a++;
            }

            monplateau = tab;           //On instancie enfin l'attribut monplateau
        }

        //propriétés
        public char[,] Monplateau
        {
            get { return this.monplateau; }
            set { this.monplateau = value; }
        }


        //méthodes
        public bool Test_Plateau(string mot, int n, int unique, bool res, Stack<int> x, Stack<int> y, int abs, int ord, int unique2, bool res2, Stack<int> x2, Stack<int> y2, int abs2, int ord2)       //n permet de parcourir le mot et la variable unique va permettre de parcourir une unique fois l'instruction ligne 33
        {                                                                        //bool res va permettre de vérifier s'il faut considérer une nouveau couple de coordonnées
                                                                                 //PAR RECURRENCE                                                          //les Stack stockent les coordonnées possibles
                                                                                 //abs et ord correspondent à la position étudiée
                                                                                 //dans l'appel mettre n = 1 unique = 0, res = false, initialiser les stacks, abs et ord valent 0 par défaut, unique2 = 0, res2 = false

            int a = 1;      //variable qui va servir de vérif ligne 151
            int absInter = 0;           //Variable intermédiaire pour récupérer les coordonnées de position
            int ordInter = 0;

            //Création de stacks qui vont stocker les différentes possibilités d'index du début du mot, car il peut y avoir 2 fois la meme lettre sur le plateau

            bool inter = false;         //variable bool intermédiaire          

            if (unique == 0)            //Cette condition va permettre de réaliser une seule fois cette instruction initiale
            {
                for (int i = 0; i < monplateau.GetLength(0); i++)
                {
                    for (int j = 0; j < monplateau.GetLength(0); j++)
                    {
                        if (monplateau[i, j] == mot[0])         //Determine la position de la première lettre du mot sur le plateau
                        {
                            x.Push(i);                  //A chaque fois qu'on a une potentielle position de début de mot, on empile les coordonnées dans une stack
                            y.Push(j);
                        }
                    }
                }

            }

            if (res == false)       //Si les coordonnées précédents n'ont pas fonctionné, on tente un nouveau couple
            {
                if (x.Count == 0 || y.Count == 0)
                {
                    return false;       //s'il n'y a plus de possibilité de couple de coordonnées on renvoit false
                }

                abs = x.Pop();          //Sinon on récupère la  possibilité situé en haut de la pile, puis grâce à "pop" on va pouvoir dépiler progressivement les possibilités de coordonnées
                ord = y.Pop();

            }

            //********************Deuxième phase de test****************************//

            if (unique2 == 0)            //Cette condition va permettre de réaliser une seule fois cette instruction
            {

                a = 0;              //variable qui va servir de vérif ligne 151

                for (int k = 0; k < monplateau.GetLength(0); k++)
                {
                    for (int l = 0; l < monplateau.GetLength(0); l++)
                    {
                        if ((monplateau[k, l] == mot[n]) && (k == (abs + 1) || (k == (abs - 1)) || (k == abs)) && (l == (ord + 1) || (l == (ord - 1)) || (l == ord)))
                        {
                            if (k != abs || l != ord)           // Vérifie qu'on ne rejoue pas sur la meme lettre         
                            {
                                if (n == mot.Length - 1)        //si on a vérifié la bonne position de la dernière lettre du mot, alors on renvoit true car le mot respecte l'adjacence
                                {
                                    return true;
                                }

                                x2.Push(k);                  //A chaque fois qu'on a une potentielle position de début de mot, on empile les coordonnées dans une stack puis on continue
                                y2.Push(l);

                                a = 1;      //variable de validation voir ligne 156

                                absInter = k;            //Récupération de la position de la lettre étudiée
                                ordInter = l;
                            }

                        }

                        else
                        {
                            res = false;               //sinon le mot n'est pas correct, ce qui va nous permettre de tirer de nouvelles coordonnées
                        }
                    }
                }

            }

            if (a == 0)      //Si la variable n'a pas été modifiée alors c'est que le mot ne valide pas les conditions
            {
                return Test_Plateau(mot, 1, 1, false, x, y, 0, 0, 0, false, x2, y2, 0, 0);              //Donc on return directement la fonction en testant une autre possibilité trouvée
            }       //unique = 1 pour ne pas refaire la boucle
                    //res = false pour tirer une nouvelle position de début de mot
                    //on garde x2 et y2 qui correspond à la position de mot[n]

            if (res2 == false)       //Si les coordonnées précédents n'ont pas fonctionné, on tente un nouveau couple
            {
                if (x2.Count == 0 || y2.Count == 0)
                {
                    return false;       //s'il n'y a plus de possibilité de couple de coordonnées on renvoit false
                }

                abs2 = x2.Pop();          //Sinon on récupère la  possibilité situé en haut de la pile, puis grâce à "pop" on va pouvoir dépiler progressivement les possibilités de coordonnées
                ord2 = y2.Pop();
            }

            for (int i = 0; i < monplateau.GetLength(0); i++)
            {
                for (int j = 0; j < monplateau.GetLength(1); j++)
                {
                    if ((monplateau[i, j] == mot[n + 1]) && (i == (abs2 + 1) || (i == (abs2 - 1)) || (i == abs2)) && (j == (ord2 + 1) || (j == (ord2 - 1)) || (j == ord2)))     //Vérifie la position des lettres du mot
                    {                                                                                                                                                //Verifie de même l'adjacence

                        if (i != abs2 || j != ord2)           // Vérifie qu'on ne rejoue pas sur la meme lettre         
                        {
                            if ((n + 1) == mot.Length - 1)        //si on a vérifié la bonne position de la dernière lettre du mot, alors on renvoit true car le mot respecte l'adjacence
                            {
                                return true;
                            }

                            inter = true;
                          

                            abs = absInter;            //Récupération de la position de la lettre étudiée
                            ord = ordInter;
                        }

                    }

                    else
                    {
                        res2 = false;       //on doit tirer de nouvelles coordonnées de 2ème lettre
                    }
                }
            }


            if (inter == true)
            {
                return Test_Plateau(mot, n + 1, 1, true, x, y, abs, ord, 0, false, x2, y2, 0, 0);   //On passe à la prochaine lettre (n+1), on renvoit unique 1 pour pas refaire l'instruction ligne 33, et on renvoit les stacks, on renvoit enfin la position où l'on se trouve
            }           //on rappelle la fonction avec unique = 1 et res = true pour ne pas refaire les boucles (on ne change de départ de mot)
                        //on met unique2 = 0 et res2 = false pour chercher a nouveau les possibilités de chemin autour de chacune des lettres qu'on va étudier

            if (res2 == false)       //Si la 2ème lettre du mot ne correspond pas aux conditions 
            {
                //alors on passe à l'index de première lettre suivant en renvoyant false, on renvoit n = 1 pour revenir au début du mot, et abs et ord valent 0 par défaut + on remet tous les tirages de X2 et y2 à 0
                return Test_Plateau(mot, 1, 1, true, x, y, 0, 0, 1, false, x2, y2, 0, 0);
                // unique2 = 1 pour ne pas refaire la pile des positions
            }       //on a res2 = false pour tirer une nouvelle possibilité de chemin

            if (res == false)       //Si le mot ne correspond pas aux conditions 
            {
                //alors on passe à l'index de première lettre suivant en renvoyant false, on renvoit n = 1 pour revenir au début du mot, et abs et ord valent 0 par défaut + on remet tous les tirages de X2 et y2 à 0
                return Test_Plateau(mot, 1, 1, false, x, y, 0, 0, 0, false, x2, y2, 0, 0);
            }


            return false;

        }

        public void AffichagePlateau()      //Nous avons ajusté la signature de la méthode public override string ToString() imposée dans le sujet
        {
            for (int i = 0; i < monplateau.GetLength(0); i++)
            {
                for (int j = 0; j < monplateau.GetLength(1); j++)
                {
                    Console.Write(monplateau[i, j]);
                }

                Console.WriteLine();
            }
        }
    }
}
