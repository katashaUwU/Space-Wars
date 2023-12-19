using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using System.Security.Cryptography.X509Certificates;
using System.Linq.Expressions;

namespace space_wars
{
    internal class Rocket
    {
        public bool lose_game = false;
        public bool win_game = false;

        public int score = 0;

        public int x_position = Console.WindowWidth / 2;    //Rocket starting x position
        public int y_position = Console.WindowHeight / 2;   //Rocket starting y position

        public int alien_bullet_y = 1;  //Alien's bullet starting y position
        public int alien_bullet_x;

        Random rnd = new Random();

        public List<string> rocket = new List<string>();    //rocket ASCII art
        public List<int> rocket_bullet_y = new List<int>(); //y position of rocket's bullet

        //Key response - player move left and right//       
        public void Rocket_Movement()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.RightArrow:
                    if (x_position < 111) x_position++; //111 -> maximum x position for the console of Window Width 120 
                    break;

                case ConsoleKey.LeftArrow:
                    if (x_position > 0) x_position--;   //0 -> minimum x position 
                    break;
            }
        }
        ////////////////////////////////////////////     


        //Rocket's bullet and Alien's bullet drawing//
        public void Draw_Bullet()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            //Rocket shooting - response to space bar//
            if (keyInfo.Key == ConsoleKey.Spacebar)
            {
                rocket_bullet_y.Add(Console.WindowHeight / 2);

                foreach (int y_position in rocket_bullet_y)
                {
                    Program.Cursor_Position(x_position + 4, y_position);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("|");
                }
                for (int i = 0; i == 0; i++)    //Changing bullet's position
                {
                    if (rocket_bullet_y[i] == 0)
                    {
                        rocket_bullet_y.Clear();
                        rocket_bullet_y.Add(Console.WindowHeight / 2);
                    }
                    rocket_bullet_y[i] -= 1;    //Speed of bullet
                }

                //Checking if the Rocket hits alien//
                for (int i = Alien.aliens.Count - 1; i >= 0; i--)
                {
                    for (int p = -3; p <= 3; p++)
                    {
                        if (Alien.aliens[i] == x_position + p && rocket_bullet_y[0] == 0)   //p -> area of alien's hitboxes
                        {
                            Alien.aliens.RemoveAt(i);
                            score++;
                            if (score == 8)
                                win_game = true;
                            break;
                        }
                    }
                }
                ////////////////////
            }
            //////////////////////////////////////////

            //Alien's shooting//           
            if (alien_bullet_y == 1)
                alien_bullet_x = rnd.Next(Alien.aliens.First(), Alien.aliens.Last());

            Program.Cursor_Position(alien_bullet_x, alien_bullet_y);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("|");
            alien_bullet_y += 1;

            if (alien_bullet_y == 23)
                alien_bullet_y = 1;     //Speed of bullet

            ///////////////////

            //Checking if the Alien hits rocket//
            for (int i = 0, j = 1; i <= 9 && j <= 3; i++)   //i <= 9 -> because size of the rocket is 9
            {
                if (alien_bullet_x == x_position + i && alien_bullet_y - 1 == y_position + j)   //i and j -> rocket's hitboxes
                {
                    rocket.Clear();
                    Thread.Sleep(200);  //For slower clear of the console
                    lose_game = true;
                }
            }
            ///////////////////////////////////////
        }
        ///////////////////////////////////////////////      

        //Rocket drawing//
        public void Draw_Rocket()
        {
            rocket.Add("    ˄");
            rocket.Add("   / \\");
            rocket.Add(" --   --");
            rocket.Add("|#######|");

            int i = 0;

            foreach (string a in rocket)
            {
                Program.Cursor_Position(x_position, y_position + i);  //y_position + i -> height of rocket
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(a);
                if (i < 3) i++;
            }
        }
        /////////////////

        public void Lose_Game()
        {
            Console.Clear();
            Program.Cursor_Position(30, 10);
            Console.ForegroundColor = ConsoleColor.Red;
            //ASCII art - lose screen//
            Console.Write(@"
                                     ____    _    __  __ _____    _____     _______ ____  
                                    / ___|  / \  |  \/  | ____|  / _ \ \   / / ____|  _ \ 
                                   | |  _  / _ \ | |\/| |  _|   | | | \ \ / /|  _| | |_) |
                                   | |_| |/ ___ \| |  | | |___  | |_| |\ V / | |___|  _ < 
                                    \____/_/   \_\_|  |_|_____|  \___/  \_/  |_____|_| \_\

                                                
                                                You have been hit by alien

                                      Press ESC to exit the game, or ENTER to restart
            ");
            ///////////////////////////
        }

        public void Win_Game()
        {
            Console.Clear();
            Program.Cursor_Position(30, 10);
            Console.ForegroundColor = ConsoleColor.Green;
            //ASCII art - win screen//
            Console.Write(@"
                                       __   _____  _   _  __        _____  _   _ 
                                       \ \ / / _ \| | | | \ \      / / _ \| \ | |
                                        \ V / | | | | | |  \ \ /\ / / | | |  \| |
                                         | || |_| | |_| |   \ V  V /| |_| | |\  |
                                         |_| \___/ \___/     \_/\_/  \___/|_| \_|

                                          
                                        You have succesfully defeated them all
                                    
                                    Press ESC to exit the game, or ENTER to restart
            ");
            //////////////////////////
        }
    }  
}

