
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using static System.ConsoleColor;
using static System.Console;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
// using EZInput;

namespace Space__Fighter
{
   
    class Program
    {
       
        class Coordinates
        {
            public int startX;
            public int startY;

        }
        static void Main(string[] args)
        {
            char[,] maze = new char[30, 90];
            readdata(maze);
            printMaze(maze);
            Console.ReadKey();
            char[,] spacecraft = new char[6, 12]
             {
                 {' ', ' ', ' ', ' ', ' ', '_', '_', ' ', ' ', ' ', ' ', ' '},
                 {' ', ' ', '_', '_', '_', '|', '|', '_', '_', '_', ' ', ' '},
                 {' ', ' ', '|', ' ', '_', '_', '_', '_', ' ', '|', ' ', ' '},
                 {' ', ' ', '|', ' ', '_', '_', '_', '_', ' ', '|', ' ', ' '},
                 {'|', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '|'},
                 {'|', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '|'}
             };


            char[,] enemyspacecraft = new char[6, 10]
            {
                  {'_', '_', '_', '_', '_', '_', '_', '_', '_', '_'},
                  {'_', '_', '_', '_', '_', '_', '_', '_', '_', '_'},
                  {' ', '|', ' ', '_', '_', '_', '_', ' ', '|', ' '},
                  {' ', '|', ' ', '_', '_', '_', '_', ' ', '|', ' '},
                  {' ', '_', '_', '_', '_', '_', '_', '_', '_', ' '},
                  {' ', ' ', ' ', ' ', '|', '|', ' ', ' ', ' ', ' '}
            };



            Coordinates obj = new Coordinates();
            obj.startX = 10; 
            obj.startY = 20;   

            int ex = 1;
            int ey = 3;

            string direction = "right";


         
            int prevX = obj.startX;
            int prevY = obj.startY;

         
            PrintSpacecraft(spacecraft,obj.startX, obj.startY);
            Printenemy(maze,enemyspacecraft, ex, ey);
           
           
            while (true)
            {
              
                    if (direction == "left" && (maze[ex,ey-1 ] == ' '))
            {
                eraseenemy(maze,enemyspacecraft, ex, ey);
                ey--;
                Printenemy(maze,enemyspacecraft, ex, ey);

              
                if (maze[ex, ey-1] == '#'||maze[ex, ey-1] == '|')
                {
                    direction = "right";
                }
            }
            else if (direction == "right" && (maze[ex, ey+10] == ' '))
            {

                eraseenemy(maze,enemyspacecraft,ex, ey);
                ey++;
                Printenemy(maze,enemyspacecraft, ex, ey);
                  if (maze[ex, ey+10] == '#'||maze[ex, ey+10] == '|')
                {
                    direction = "left";
                }

            }
                
               
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.RightArrow && obj.startX < maze.GetLength(1) + 1 && maze[obj.startY, obj.startX + 1] != '|')
                {
                    
                    prevX = obj.startX;
                    prevY = obj.startY;

                    obj.startX++;
                }
                if (keyInfo.Key == ConsoleKey.LeftArrow && obj.startX < maze.GetLength(1) - 1 && maze[obj.startY, obj.startX - 1] != '|')
                {
                    
                    prevX = obj.startX;
                    prevY = obj.startY;

                    obj.startX--;
                }
                if (keyInfo.Key == ConsoleKey.DownArrow && obj.startY < maze.GetLength(0) - 1 && maze[obj.startY + 1, obj.startX] != '#')
                {
                    
                    prevX = obj.startX;
                    prevY = obj.startY;

                    obj.startY++;
                }
                if (keyInfo.Key == ConsoleKey.UpArrow && obj.startY < maze.GetLength(0) - 1 && maze[obj.startY - 1, obj.startX] != '#')
                {
                    
                    prevX = obj.startX;
                    prevY = obj.startY;

                    obj.startY--;
                }

               
                for (int i = prevY; i < prevY + spacecraft.GetLength(0); i++)
                {
                    Console.SetCursorPosition(prevX, i);
                    for (int j = 0; j < spacecraft.GetLength(1); j++)
                    {
                        Console.Write(' ');
                    }
                }

                PrintSpacecraft(spacecraft, obj.startX, obj.startY);
              
            }


        }


        static void readdata(char[,] maze)
        {
            StreamReader Maze = new StreamReader("Maze.txt");
            string record;
            int row = 0;
            while ((record = Maze.ReadLine()) != null && row < maze.GetLength(0))
            {
                for (int x = 0; x < 90; x++)
                {
                    maze[row, x] = record[x];
                }
                row++;
            }

            Maze.Close();

        }
        static void printMaze(char[,] maze)
        {
            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    if (maze[x, y] == ' ')
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.Write(maze[x, y]);
                    }
                }
                Console.WriteLine();
            }
        }





        
        static void PrintSpacecraft(char[,] spacecraft, int x, int y)
        {
            for (int i = 0; i < spacecraft.GetLength(0); i++)
            {
                Console.SetCursorPosition(x, y + i);
                for (int j = 0; j < spacecraft.GetLength(1); j++)
                {
                    Console.Write(spacecraft[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void Printenemy(char[,] maze,char[,] enemyspacecraft, int ex, int ey)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                Console.SetCursorPosition(ey+j,ex+i);
                    Console.Write(enemyspacecraft[i, j]);
                    maze[ex+i,ey+j]=enemyspacecraft[i,j];
                }
                // Console.WriteLine();
            }
        }

        static void eraseenemy(char[,] maze,char[,] enemyspacecraft,int ex, int ey)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                Console.SetCursorPosition(ey+j, ex + i);
                    Console.Write(' ');
                    maze[ex+i,ey+j]=' ';
                }
                // Console.WriteLine();


            }
        }

    }
}
