// Titre de l'application : Jeu du Morpion
// Nom du développeur : Vincent SILES

/// Objectif : Établir un jeu du Morpion en C# à l'aide de classe , boucle et fonctions .


using System;

namespace JeuDuMorpion
{
    class Programme
    {
        // Les variables 
        public static bool quitterPartie=false; 
        public static bool tourJoueur = true; // Correspond au tour du joueur 
        public static char[,] board; // Correspond au plateau de jeu 

        // Fonction Main
        static void Main(string[]args)
        {
            // Boucle de jeu 
            while(!quitterPartie) // tant que le jeu du morpion tourne
            {
                board = new char[3, 3] // On initialise le plateau avec 3 lignes et 3 colonnes 
                {
                    {' ',' ',' ' },
                    {' ',' ',' ' },
                    {' ',' ',' ' },
                };
                while(!quitterPartie)
                {
                    //tour du Joueur
                    if(tourJoueur)
                    {
                        TourJoueur();
                        if(CheckLines('X'))
                        {
                            EndGame("Vous avez gagné !");
                                break;
                        }
                    }
                    // Tour de l'ordinateur
                    else
                    {
                        TourOrdinateur();
                        if (CheckLines('O'))
                        {
                            EndGame("Vous avez Perdu !");
                            break;
                        }
                    }

                    // Changement du joueur
                    tourJoueur = !tourJoueur; // Changement ! = inverse de 
                                              // Cas de Match Nul
                    if (CheckDraw()) // Draw signifie Match Nul
                    {
                        EndGame("Match Nul !!!");
                        break;
                    }


                }
                if(!quitterPartie) // Si on quitte la partie 
                {
                    Console.WriteLine("Appuyez sur echap pour quitter la partie , ENTRER pour rejouer");
                    GetKey: // Récupération de la touche du clavier
                    switch(Console.ReadKey(true).Key)
                    {
                        //Rejouer 
                        case ConsoleKey.Enter:
                            break;
                        
                            // Quitter la partie 
                        case ConsoleKey.Escape: 
                            quitterPartie = true;
                            Console.Clear();
                            break;
                        
                            

                            

                    }
                }
            }

        } // Fin du programme
          //Fonctions

        // Tour du joueur
        public static void TourJoueur()
        {
            // Permet de situer le joueur sur une ligne ou colonnes 
            var (row, colum) = (0, 0);
            // Le curseur du joueur a bougé (Cela permet de rafraichir l'écran) 
            bool moved = false;
            while(!quitterPartie && !moved) // Boucle qui permet de déplacer le curseur à l'écran
            {
                Console.Clear(); // permet de nettoyer la console 
                RenderBoard(); // Fonction qui permet de generer le plateau à l'écran 
                Console.WriteLine("Saisir une case vide et appuyer sur le bouton ENTRER");
                //Affichage Curseur
                Console.SetCursorPosition(colum * 6 + 1, row * 4 + 1);
                //En attente de l'utilisateur saississant une action au clavier 
                switch(Console.ReadKey(true).Key)
                {
                    //Quitter le jeu 
                    case ConsoleKey.Escape:
                        quitterPartie = true;
                        Console.Clear();
                        break;
                    // Gérer les flèches du clavier afin de déplacer le curseur à l'écran 
                    case ConsoleKey.RightArrow:
                        
                        if(colum >= 2) // Si nous sommes sur la col 2
                        {
                            
                            colum = 0; //On retourne dans la col 0
                        }
                        else 
                        {
                            colum = colum + 1; // Sinon on va à droite 
                        }
                        break;

                    case ConsoleKey.LeftArrow: // Inverse de La fleche de droite

                        if (colum <= 0) 
                        {

                            colum = 2; 
                        }
                        else
                        {
                            colum = colum - 1; 
                        }
                        break;

                        case ConsoleKey.UpArrow: // Fleche du Haut Ligne

                        if (row <= 0)
                        {

                            row = 2;
                        }
                        else
                        {
                            row = row - 1;
                        }
                        break;

                    case ConsoleKey.DownArrow: // Fleche du Bas Ligne

                        if (row >= 2)
                        {

                            row = 0;
                        }
                        else
                        {
                            row = row + 1;
                        }
                        break;

                    // Jouer dans la classe Saisi
                    case ConsoleKey.Enter:
                        if (board[row, colum]is ' ')
                        {
                            board[row, colum] = 'X';
                            moved = true;
                        }
                        break;


                }

            }

        }
        // Tour de l'ordinateur
        public static void TourOrdinateur()
        {
            // Liste des cases vides 
            var emptyBox=new List<(int X,int Y)>();
            //Double boucle pour parcourir les cases 
            for(int i=0;i<3;i++)
            {
                for (int j=0;j<3;j++)
                {
                    // Vérifie si la case est vide
                    if(board[i,j] == ' ')
                    {
                        emptyBox.Add((i,j));
                    }

                }
            }
            // Position ou l'ordinateur va jouer
            var (X, Y) = emptyBox[new Random().Next(0, emptyBox.Count)];
            board[X, Y] = '0';
        }

        // Affichage du plateau de jeu
        public static void RenderBoard() // Dessiner le plateau à l'écran via caractere ASCII
        {
            Console.WriteLine();
            Console.WriteLine($" {board[0,0]}  |  {board[0, 1]} | {board[0,2]}");
            Console.WriteLine("    |    |");
            Console.WriteLine("----+----+----");
            Console.WriteLine("    |    |");
            Console.WriteLine($" {board[1, 0]}  |  {board[1, 1]} | {board[1, 2]}");
            Console.WriteLine("    |    |");
            Console.WriteLine("----+----+----");
            Console.WriteLine("    |    |");
            Console.WriteLine($" {board[2, 0]}  |  {board[2, 1]} | {board[2, 2]}");
        }
        // Vérifie si le joueur a gagné 

        public static bool CheckLines(char C) =>
            board[0, 0] == C && board[1, 0] == C && board[2, 0] == C ||
            board[0, 1] == C && board[1, 1] == C && board[2, 1] == C ||
            board[0, 2] == C && board[1, 2] == C && board[2, 2] == C ||
            board[0, 0] == C && board[0, 1] == C && board[0, 2] == C ||
            board[1, 0] == C && board[1, 1] == C && board[1, 2] == C ||
            board[2, 0] == C && board[2, 1] == C && board[2, 2] == C ||
            board[0, 0] == C && board[1, 1] == C && board[2, 2] == C ||
            board[2, 0] == C && board[1, 1] == C && board[0, 2] == C;

        // Vérifier si il y a un cas de match Nul 
        public static bool CheckDraw() => // Vérifie toute les cases 
            board[0, 0] != ' ' && board[1, 0] != ' ' && board[2, 0] != ' ' &&
            board[0, 1] != ' ' && board[1, 1] != ' ' && board[2, 1] != ' ' &&
            board[0, 2] != ' ' && board[1, 2] != ' ' && board[2, 2] != ' ';


        // Fin de Partie

        public static void EndGame(string msg) // Fonction de fin de partie 
        {
            Console.Clear();
            RenderBoard();
            Console.WriteLine(msg);
        }
    }
}
