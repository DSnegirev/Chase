using
System;
using
System.Collections.Generic;
using
System.Linq;
using
System.Text;
namespace
smileyFace
{
    class chase
    {   // W I N D O W  S I Z E  V A L U E S
        const int SCREEN_WIDTH = 80, SCREEN_HEIGHT = 50;

       
        static Random rnd = new Random();
        static Random rnd2 = new Random();

        static int level = 1;
        static int points = 0;

        static int[] enemy_x = new int[100000];
        static int[] enemy_y = new int[100000];

        static Boolean Taken; 
      
        static int x = 40, y = 20;

        static int sonicnum;
        
        static Boolean[] chaserAlive = new Boolean[100000];

        static ConsoleKeyInfo entry;

        static void Main(string[] args)
        {

            
        // S C R E E N S I Z E
            
            Console.SetWindowSize(SCREEN_WIDTH,SCREEN_HEIGHT);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;


            
        // I N I T I A L I Z E P L A Y E R S
            Initalize();
      
                    


            //******************************************************************************************
            //
            //
            //                          G A M E  L O O P
            //
            //
            //******************************************************************************************
            do
            {  
  
            // D R A W
                
                DisplayStats();
                // Smiley
                PlaceSmiley("☺"); // alt + 1

                //Enemy
                DrawChasers("☻"); // alt + 2
                                   
            // G E T K E Y
               entry = Console.ReadKey(true);
                              
            // H E L P  M E N U
               if (entry.Key == ConsoleKey.F1)
               {
                   HelpMenu();
                   continue;
               }


            // E R A S E 

                //Erase Smiley
                PlaceSmiley(" "); // alt + 1

                //Erase Enemy
                DrawChasers(" "); // alt + 2

            // M O V E 

                // Move Smiley
                HandleKeys(entry);

                if (entry.Key != ConsoleKey.P)
                {

                    // Move Enemy
                      // so that W, A, S, D doesn't move enemies
                    if (entry.Key != ConsoleKey.W && entry.Key != ConsoleKey.A && entry.Key != ConsoleKey.S && entry.Key != ConsoleKey.D)
                    {
                      MoveEnemy(entry);  
                    }
                    


                    Collision();
                    Chaser_on_Chaser();


                    NextLevelCheck();
                }
                
            } while (entry.Key != ConsoleKey.Escape);// do


         }// static void Main(string[] args)

              
            
/********************************
 * 
 *       M E T H O D S
 * 
 * ******************************/




     /////////////////////////////////////////////////
     /////////     M O V E  A N D  D R A W      //////
     /////////////////////////////////////////////////
       
        static void MoveEnemy(ConsoleKeyInfo entry)
            {
                // Move Enemy
                for (int i = 0; i < level * 5; i++)
                {
                if (chaserAlive[i]== true)
                {
                    
                    

                    if (enemy_x[i] > x)
                    {
                        enemy_x[i]--;
                    }
                    if (enemy_x[i] < x)
                    {
                        enemy_x[i]++;
                    }
                    if (enemy_y[i] > y)
                    {
                        enemy_y[i]--;
                    }
                    if (enemy_y[i] < y)
                    {
                        enemy_y[i]++;
                    }
                }

                }

            }//static void MoveEnemy
        
        static void HandleKeys(ConsoleKeyInfo entry)
        {  
            
            // M O V E M E N T  K E Y S
                switch (entry.Key)
                {
                    

                    case ConsoleKey.UpArrow:
                        y--;
                        break;

                    case ConsoleKey.LeftArrow:
                        x--;
                        break;

                    case ConsoleKey.DownArrow:
                        y++;
                        break;

                    case ConsoleKey.RightArrow:
                        x++;
                        break;

                    case ConsoleKey.Escape:
                        break;
             
                    case ConsoleKey.NumPad1:
                        x--;
                        y++;
                        break;

                    case ConsoleKey.End:    
                        x--;
                        y++;
                        break;

                    case ConsoleKey.NumPad2:
                        y++;
                        break;

                    case ConsoleKey.NumPad3:
                        x++;
                        y++;
                        break;

                    case ConsoleKey.PageDown:
                        x++;
                        y++;
                        break;

                    case ConsoleKey.NumPad4:
                        x--;
                        break;

                    case ConsoleKey.NumPad5:
                        break;

                    case ConsoleKey.NumPad6:
                        x++;
                        break;

                    case ConsoleKey.NumPad7:
                        y--;
                        x--;
                        break;

                    case ConsoleKey.Home:
                        y--;
                        x--;
                        break;

                    case ConsoleKey.NumPad8:
                        y--;
                        break;

                    case ConsoleKey.NumPad9:
                        y--;
                        x++;
                        break;
                                           
                    case ConsoleKey.PageUp:
                        y--;
                        x++;
                        break;
                        
                // C H E A T  K E Y S
                   // W,A,S,D doesnt' move enemy
                    case ConsoleKey.W:
                        y--;
                        break;

                    case ConsoleKey.A:
                        x--;
                        break;

                    case ConsoleKey.S:
                        y++;
                        break;

                    case ConsoleKey.D:
                        x++;
                        break;

                   // P skips to the next level
                    case ConsoleKey.P:
                        NextLevel();
                        System.Threading.Thread.Sleep(1);
                        break;


                // H Y P E R - J U M P
                     case ConsoleKey.Spacebar:
                       x = rnd.Next(SCREEN_WIDTH- 2) + 1;
                       y = rnd.Next(SCREEN_HEIGHT - 2) + 1;
                       break;

                // S O N I C - B L A S T
                    case ConsoleKey.Delete:
                    case ConsoleKey.Backspace:
                       SonicBlast();
                       break;
                        

                    default:
                        break;

                } //  switch (entry.Key)

            

            // O F F S C R E E N
                
                if (x < 1) // Left Side
                {
                    x = 1;
                }
                else if (x >= Console.WindowWidth - 2) // Right
                {
                    x = Console.WindowWidth - 2;
                }
                if (y < 1) // Top 
                {
                    y = 1;
                }
                
                else if (y >= Console.WindowHeight - 2)// Bottom
                {
                    y = Console.WindowHeight - 2;

                }
                
            
    } // static void HandleKeys
        
        static void PlaceSmiley(string ch)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(x, y);
            Console.Write(ch);// alt + 1
        }//static void PlaceSmiley()

        static void DrawChasers(string ch)
        {
            for (int i = 0; i < level * 5; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(enemy_x[i], enemy_y[i]);
                if (chaserAlive[i])
                {
                    Console.Write(ch); // alt + 2
                }
                else
                {
                    Console.Write("∞");// alt + 236
                }
            }//for (int i = 0; i < level * 5; i++)
        }//static void DrawChasers(string ch)

            
            
        
     /////////////////////////////////////////////////
     ///////// B A C K G R O U N D & O T H E R   /////
     /////////////////////////////////////////////////

        
            static void DisplayStats()
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;

                Console.SetCursorPosition(36, 0);
                Console.Write(" Level {0} ", level);

                Console.SetCursorPosition(5, 49);
                Console.Write(" Points: {0} ", points);

                Console.SetCursorPosition(3, 0);
                Console.Write(" F1 for Help ");

                Console.SetCursorPosition(65, 0);
                Console.Write(" Esc To Quit ");

                SonicBlastIndicator();
                       
            }
        
            static void Initalize()
            {
         // I N I T A L I Z E  P L A Y E R S

                // S M I L E Y  P O S I T I O N
                
                chaserAlive[level * 5]= true;

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Clear();
                points = 0;
                DrawBorder(0, 0, SCREEN_WIDTH - 1, SCREEN_HEIGHT - 1);
                level = 1;
                sonicnum = 1;
                              
                x = rnd.Next(SCREEN_WIDTH - 2) + 1;
                y = rnd.Next(SCREEN_HEIGHT - 2) + 1;

                // S P A W N
                Boolean Taken = false;
                do
                {
                    enemy_x[0] = rnd.Next(SCREEN_WIDTH - 2) + 1;
                    enemy_y[0] = rnd.Next(SCREEN_HEIGHT - 2) + 1;
                    chaserAlive[0] = true;
                    Taken = false;

                    if (enemy_x[0] == x && enemy_y[0] == y)
                        Taken = true;

                } while (Taken);
 

                

                for (int i = 0; i < level * 5; i++)
                {

                    do
                    {
                        enemy_x[i] = rnd.Next(SCREEN_WIDTH - 2) + 1;
                        enemy_y[i] = rnd.Next(SCREEN_HEIGHT - 2) + 1;
                        chaserAlive[i] = true;
                        Taken = false;

                        if (enemy_x[i] == x && enemy_y[i] == y)
                            Taken = true;

                        for (int j = 0; j < i; j++)
                            if (enemy_x[i] == enemy_x[j] && enemy_y[i] == enemy_y[j])
                                Taken = true;
                                                                               
                    } while (Taken);
                }


            


            }//static void Initalize()
                
            static void DrawBackground(int x1, int y1, int x2, int y2)
            {

                for (int row = y1; row <= y2 ; row++)
                {
                    for (int col = x1; col <= x2; col++)
                    {
                        Console.SetCursorPosition(col, row);
                        Console.Write(" ");

                    }//for (int col = x1; col <= x2; col++)

                }//for (int row = y1; row <= y2 ; row++)


            }//static void DrawBackground(int x1, int y1, int x2, int y2)
          
            static void DrawBorder(int x1, int y1, int x2, int y2)
      {


        // T O P / B O T T O M
            for (int i = x1; i < x2; i++)
			{
             Console.ForegroundColor = ConsoleColor.Blue;
			 Console.SetCursorPosition(i,y1);
             Console.Write("═"); // alt + 205

             
             Console.SetCursorPosition(i, y2);
             Console.Write("═"); // alt + 205

            }// for (int i = x1; i < x2; i++)

        // S I D E S
            for (int i = y1; i < y2; i++)
            {
                Console.SetCursorPosition(x1,i);
                Console.Write("║");// alt + 186
                Console.SetCursorPosition(x2,i);
                Console.Write("║");// alt + 186
                
            }
            

        // C O R N E R S

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(x1, y1);
            Console.Write("╝"); // ALT + 188
            Console.MoveBufferArea(x1, y1, 1, 1, x2, y2);

            Console.SetCursorPosition(x1, y1);
            Console.Write("╔"); // ALT + 201

            Console.SetCursorPosition(x1, y2);
            Console.Write("╚"); // ALT + 200

            Console.SetCursorPosition(x2, y1);
            Console.Write("╗"); // ALT + 187
         
            
        }// static void DrawBorder
        

            

     /////////////////////////////////////////////////
     /////////     C O L L I S I O N            //////
     /////////////////////////////////////////////////


            static void CaptureBox(int x1=25, int y1=17, int x2=55, int y2=28)
            {

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                DrawBackground(15, 15, 65, 22);
                DrawBorder(15, 15, 65, 22);

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(23, 18);
                Console.Write("Y O U  W E R E  C A P T U R E D !  ");
                Console.SetCursorPosition(23, 20);
                Console.Write("Press ENTER To Continue...");


            }// static void CaptureBox(int x1, int y1, int x2, int y2)

            static void Chaser_on_Chaser()
            {

                for (int i = 0; i < level * 5 - 1; i++)
                {
                    for (int j = i + 1; j < level * 5; j++)
                    {
                        if (enemy_x[i] == enemy_x[j] && enemy_y[i] == enemy_y[j])
                        {
                            if (chaserAlive[i])
                            {
                                points = points + level * 10;

                            }
                            if (chaserAlive[j])
                            {
                                points = points + level * 10;

                            }
                            chaserAlive[i] = false;
                            chaserAlive[j] = false;

                        } // if





                    }// for

                }// for
            }// static void Chaser_on_Chaser

            static void Collision()
            { 
                // C O L L I S I O N

                for (int i = 0; i < level * 5; i++)
                {
                    if (x == enemy_x[i] && y == enemy_y[i])
                    {
                        PlaceSmiley("☺"); // alt + 1
                        DrawChasers("☻"); // alt + 2

                        CaptureBox();
                        do
                        {
                            entry = Console.ReadKey(true);

                        } while (entry.Key != ConsoleKey.Enter);

                        if (entry.Key == ConsoleKey.Enter)
                        {

                            Initalize();
                        }



                    }//if (x == enemy_x[i]&& y == enemy_y[i])

                }//for (int i = 0; i < level * 5; i++)
            }


     /////////////////////////////////////////////////
     /////////     N E X T  L E V E L           //////
     /////////////////////////////////////////////////

            static void LevelBox()
            {
                DisplayStats();

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                DrawBackground(15, 15, 65, 22);
                DrawBorder(15, 15, 65, 22);

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(23, 18);
                Console.Write("Y o u  B e a t  L e v e l  {0}", level);
                Console.SetCursorPosition(23, 20);
                Console.Write("Press ENTER To Continue...");
 
                do
                {
                    entry = Console.ReadKey(true);
                }while (entry.Key != ConsoleKey.Enter); 

                NextLevel();

            }// LevelBox
        
            static void NextLevel()
            {
                               
                chaserAlive[level * 5] = true;

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Clear();
                DrawBorder(0, 0, SCREEN_WIDTH - 1, SCREEN_HEIGHT - 1);
                level = level + 1;
                sonicnum++;


                x = rnd.Next(SCREEN_WIDTH - 2) + 1;
                y = rnd.Next(SCREEN_HEIGHT - 2) + 1;

                // enemy
                for (int i = 0; i < level * 5; i++)
                {
                    chaserAlive[i] = true;
                    enemy_x[i] = rnd.Next(SCREEN_WIDTH - 2) + 1;
                    enemy_y[i] = rnd.Next(SCREEN_HEIGHT - 2) + 1;

                }// for (int i = 0; i < level * 5; i++)



            } // NextLevel

            static void NextLevelCheck()
            {
                int count = 0;

                for (int i = 0; i < level * 5 - 1; i++)
                {



                    if (chaserAlive[i])
                    {
                        count++;

                    }

                }// for

                if (count == 0)
                {
                    LevelBox();

                }

            }// static void NextLevelCheck()

     /////////////////////////////////////////////////
     /////////     M E C H A N I C S            //////
     /////////////////////////////////////////////////
                        
            static void SonicBlast()
        {
          if (sonicnum >= 1)
	         {
		 
	
                               
                for (int i = 0; i < level * 5 ; i++)
                {
                      //upper left               
                    if (enemy_x[i] == x - 1 && enemy_y[i] == y - 1)
                    {
                        if (chaserAlive[i] == true)
                        {
                            points = points + level * 10;
                        }
                        chaserAlive[i] = false;
                        
                    }
                    //upper right
                    if (enemy_x[i] == x - 1 && enemy_y[i] == y + 1)
                    {
                        if (chaserAlive[i] == true)
                        {
                            points = points + level * 10;
                        }
                        chaserAlive[i] = false;
                    }
                    //lower right
                     if (enemy_x[i] == x + 1 && enemy_y[i] == y + 1 )
                     {
                         if (chaserAlive[i] == true)
                         {
                             points = points + level * 10;
                         }
                         chaserAlive[i] = false;
                     }
                    //lower left
                     if (enemy_x[i] == x + 1 && enemy_y[i] == y - 1 )
                     {
                         if (chaserAlive[i] == true)
                         {
                             points = points + level * 10;
                         }
                         chaserAlive[i] = false;
                     }
                    //right
                     if (enemy_x[i] == x  && enemy_y[i] == y + 1 )
                     {
                         if (chaserAlive[i] == true)
                         {
                             points = points + level * 10;
                         }
                         chaserAlive[i] = false;
                     }
                    //left
                     if (enemy_x[i] == x && enemy_y[i] == y - 1 )
                     {
                         if (chaserAlive[i] == true)
                         {
                             points = points + level * 10;
                         }
                         chaserAlive[i] = false;
                     }
                    //up
                     if (enemy_x[i] == x - 1 && enemy_y[i] == y  )
                     {
                         if (chaserAlive[i] == true)
                         {
                             points = points + level * 10;
                         }
                         chaserAlive[i] = false;
                     }
                    //bottom
                     if (enemy_x[i] == x + 1 && enemy_y[i] == y )
                     {
                         if (chaserAlive[i] == true)
                         {
                             points = points + level * 10;
                         }
                         chaserAlive[i] = false;
                     }
                }
                                       
                }// for
                  
                sonicnum = sonicnum - 1;
                
             }// SonicBlastDetails

            static void HelpMenu()
            {
                Console.Clear();
                PlaceSmiley("☺");// alt + 1
                DrawChasers("☻");// alt + 1


                //Game Border
                DrawBorder(0, 0, SCREEN_WIDTH - 1, SCREEN_HEIGHT - 1);
                DisplayStats();


                //Help Box
                Console.BackgroundColor = ConsoleColor.Gray;
                DrawBackground(10,10,70,40);
                DrawBorder(10,10,70,40);

                Console.SetCursorPosition(35, 11);
                Console.Write("- H E L P -");

                Console.SetCursorPosition(13, 13);
                Console.Write("The Object of CHASE is for you, the player (☺) to avoid ");
                Console.SetCursorPosition(13, 14);
                Console.Write("the chasers (☻)!!! ");
                Console.SetCursorPosition(13, 16);
                Console.Write("If two chasers run into each other, they die and  create ");
                Console.SetCursorPosition(13, 17);
                Console.Write("a trap (φ), which kills other chasers that run into it.");
                Console.SetCursorPosition(13, 20);
                Console.Write("S P E C I A L  K E Y S :");
                Console.SetCursorPosition(13, 22);
                Console.Write("SPACEBAR:  Randomly jumps the player to a new location ");
                Console.SetCursorPosition(13, 23);
                Console.Write("(Hyperjump).");
                Console.SetCursorPosition(13, 25);
                Console.Write("DELETE or BACKSPACE:  Sonic Blast!! Kills all chasers ");
                Console.SetCursorPosition(13, 26);
                Console.Write("that are adjacent to the player. You get one for each");
                Console.SetCursorPosition(13, 27);
                Console.Write("level, an indicator ( ) on the border displays the ");
                Console.SetCursorPosition(13, 28);
                Console.Write("number remaining.");
                Console.SetCursorPosition(13, 31);
                Console.Write("Use the Arrow Keys or the Number Pad to Move Around.");
                Console.SetCursorPosition(25, 36);
                Console.Write("Press E N T E R to Continue...");
                Console.SetCursorPosition(34, 27);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("▒");
                

                do
                {
                    entry = Console.ReadKey(true);

                } while (entry.Key != ConsoleKey.Enter);


                //Redraw Everything
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                DrawBorder(0, 0, SCREEN_WIDTH - 1, SCREEN_HEIGHT - 1);

            }// HelpMenu

            static void SonicBlastIndicator()
            {
                // SONIC BLAST INDICATOR

                // sides    

                if (entry.Key != ConsoleKey.P)
                {
                    
                
                for (int i = 0; i < SCREEN_HEIGHT - 2; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(0, SCREEN_HEIGHT - 2 - i);
                    Console.Write("▒");
                    Console.SetCursorPosition(79, SCREEN_HEIGHT - 2 - i);
                    Console.Write("▒");

                }
                Console.ForegroundColor = ConsoleColor.Magenta;

                for (int i = 0; i < sonicnum; i++)
                {
                    Console.SetCursorPosition(0, SCREEN_HEIGHT - 2 - i);
                    Console.Write("▒");
                    Console.SetCursorPosition(79, SCREEN_HEIGHT - 2 - i);
                    Console.Write("▒");

                }
                }
            }// static void SonicBlastIndicator()
                
    }// class chase
    
}// namespace smileyFace