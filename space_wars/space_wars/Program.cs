using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Timers;

namespace space_wars
{
    internal class Program
    {
        //ASCII text source: https://www.asciiart.eu/text-to-ascii-art  

        public int x_position = Console.WindowWidth / 2;
        public int y_position = Console.WindowHeight / 2;
        
        public static void Start_game()
        {
            //Class connecting//
            Rocket rocket = new Rocket();
            Alien alien = new Alien();
            ////////////////////

            rocket.lose_game = false;
            Console.CursorVisible = false;
            Console.Clear();

            //ASCII arts - main menu//
            Console.ForegroundColor = ConsoleColor.Magenta;
            Cursor_Position(70, 1);
            Console.WriteLine(@"
                                                      _                                        
                                        __      _____| | ___ ___  _ __ ___   ___ 
                                        \ \ /\ / / _ \ |/ __/ _ \| '_ ` _ \ / _ \
                                         \ V  V /  __/ | (_| (_) | | | | | |  __/
                                          \_/\_/ \___|_|\___\___/|_| |_| |_|\___|
            ");
            
            Cursor_Position(70, 7);
            Console.WriteLine(@"
                                                        _        
                                                       | |_ ___  
                                                       | __/ _ \ 
                                                       | || (_) |
                                                        \__\___/ 
            ");

            Cursor_Position(70, 12);
            Console.WriteLine(@"
 
           -----------------------------------------------------------------------------------------------
          |  ____    ____      _       _____    _______         ___        ___    _       ____     ____   |
          | / ___|  |  _ \    / \     /  ___|   |  ____|        \  \      /  /   / \     |  _ \   / ___|  |
          | \___ \  | |_) |  / _ \    | |       | |___|          \  \ /\ /  /   / _ \    | |_) |  \___ \  |
          |  ___) | |  __/  / ___ \   | |___    | |____           \  V  V  /   / ___ \   |  __/    ___) | |
          | |____/  |_|    /_/   \_\  \_____|   |______|           \__/\__/   /_/   \_\  |_| \_\  |____/  |
          | _____________________________________________________________________________________________ |


                        Move left - Left arrow, Move right - Right arrow, Shooting - Space bar
                                                

                                                Press any to continue...
            ");
            /////////////////////////       

            Console.ReadKey();
            Console.Clear();

            alien.Draw_Aliens();    //Draw aliens at the start
            rocket.Draw_Rocket();   //Draw rocket at the start

            //Frame//
            Console.ForegroundColor = ConsoleColor.White;
            Cursor_Position(0, 24);
            Console.Write("------------------------------------------------------------------------------------------------------------------------");
            /////////

            //Score//
            Cursor_Position(60, 25);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("SCORE: {0}00", rocket.score);
            ////////

            while (true)
            {
                rocket.Rocket_Movement();
                Console.Clear();
                rocket.Draw_Bullet();
                rocket.Draw_Rocket();
                alien.Aliens_Movement();

                //Frame//
                Console.ForegroundColor = ConsoleColor.White;
                Cursor_Position(0, 24);
                Console.Write("------------------------------------------------------------------------------------------------------------------------");
                ////////

                //Score//
                Cursor_Position(60, 25);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("SCORE: {0}00", rocket.score);
                ////////

                Thread.Sleep(70);   //For smoother drawing   

                //Loosing a game//
                if (rocket.lose_game == true)
                {
                    rocket.Lose_Game();

                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Enter:
                            Restart_Game();
                            break;

                        case ConsoleKey.Escape:
                            Environment.Exit(0);
                            break;
                    }
                }
                ///////////////////

                //Winning a game//
                if (rocket.win_game == true)
                {
                    rocket.Win_Game();

                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Enter:
                            Restart_Game();
                            break;

                        case ConsoleKey.Escape:
                            Environment.Exit(0);
                            break;
                    }
                }
                //////////////////
            }
        }

        static void Main(string[] args)
        {
            Start_game();
        }

        //Game restart//
        public static void Restart_Game()
        {
            Console.Clear();
            Rocket rocket = new Rocket();

            //Reset all variables, lists etc.//
            rocket.score = 0;
            rocket.lose_game = false;
            rocket.win_game = false;
            rocket.rocket.Clear();
            rocket.rocket_bullet_y.Clear();
            Alien.aliens.Clear();
            //////////////////////////////////

            Start_game();
        }
        ////////////////

        //Change console foreground color

        //Set cursor position//
        public static void Cursor_Position(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
        //////////////////////
    }
}