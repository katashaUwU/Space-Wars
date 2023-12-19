using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace space_wars
{
    internal class Alien
    {     
        public static List<int> aliens = new List<int>();   //list of alien's positions               
        bool MoveLeft = true;   //At the start, aliens start moving to the left
            
        //Aliens drawing//
        public void Draw_Aliens()
        {                         
            for (int i = 36; i <= 78; i += 6)   //Starting positions i += 6 -> because size of the alien is 6
            {
                aliens.Add(i);
            }

            foreach (int position in aliens)    
            {
                Program.Cursor_Position(position, 0);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[----]");
            }           
        }
        /////////////////
 
        //Aliens movement to left and right//
        public void Aliens_Movement()
        {          
            if(MoveLeft == true)    //Move to the left -> overwrite alien's positions
            {
                for (int i = aliens.Count - 1; i >= 0; i--)
                {
                    aliens[i] -= 6;
                    if (aliens[i] == 6) MoveLeft = false;       //6 -> minimum x position 
                }
            }
            else   //Move to the right -> overwrite alien's positions
            {
                for (int i = 0; i < aliens.Count; i++)
                {
                    aliens[i] += 6;
                    if (aliens[i] == 114) MoveLeft = true;      //114 -> maximum x position for the console of Window Width 120
                }               
            }                     
            foreach(int position in aliens)     //Draw aliens in new positions
            {
                Program.Cursor_Position(position, 0);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[----]");
            }                                          
        }  
        /////////////////////////////////////
    }
}

