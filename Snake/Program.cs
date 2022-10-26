using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Program
    {
        static int foodX;
        static int foodY;
        static int Record = 0;
        static int[] body_x = new int[100];
        static int[] body_y = new int[100];
        static int snakeLen = 10;
        static void SpawnFood()
        {
            Random rand = new Random();
            foodX = rand.Next(4, 116);
            if (foodX % 2 != 0) foodX++;
            foodY = rand.Next(2, 38);
            for (int i = 0; i < snakeLen; i++)
            {
                if(foodX == body_x[i] && foodY == body_y[i])SpawnFood();
            }
        }
        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 40);
            Console.CursorVisible = false;
            bool isGame = true;
            int time = 100;
            int head_x = 20;
            int head_y = 10;
            int dir = 0;

            for (int i = 0; i < snakeLen; i++)
            {
                body_x[i] = head_x - (i * 2);
                body_y[i] = 10;
            }

            SpawnFood();

            while (isGame == true)
            {
                for (int i = 0; i < snakeLen; i++)
                {
                    Console.SetCursorPosition(body_x[i], body_y[i]);
                    Console.Write("  ");
                }

                Console.SetCursorPosition(head_x, head_y);
                Console.Write("  ");

                Console.SetCursorPosition(foodX, foodY);
                Console.Write("  ");

                if (Console.KeyAvailable == true)
                {
                    Console.SetCursorPosition(0, 0);
                    ConsoleKeyInfo key;
                    key = Console.ReadKey();
                    Console.SetCursorPosition(0, 0);
                    Console.Write(" ");


                    if (key.Key == ConsoleKey.D && dir != 2) dir = 0;
                    if (key.Key == ConsoleKey.S && dir != 3) dir = 1;
                    if (key.Key == ConsoleKey.A && dir != 0) dir = 2;
                    if (key.Key == ConsoleKey.W && dir != 1) dir = 3;
                }
                if (dir == 0) head_x += 2;
                if (dir == 1) head_y += 1;
                if (dir == 2) head_x -= 2;
                if (dir == 3) head_y -= 1;

                if(head_x < 4) head_x = 116;
                if(head_x > 116) head_x = 4;
                if(head_y < 2) head_y = 38;
                if(head_y > 38) head_y = 2;

                for (int i = snakeLen; i > 0; i--)
                {
                    body_x[i] = body_x[i - 1];
                    body_y[i] = body_y[i - 1];
                }
                body_x[0] = head_x;
                body_y[0] = head_y;

                for (int i = 1; i < snakeLen; i++)
                {
                    if(body_x[i] == head_x && body_y[i] == head_y)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.Write("Вы собрали " + (snakeLen - 10) + "\t");
                        if((snakeLen-10) > Record) Record = snakeLen-10;
                        Console.WriteLine("Рекорд: " + Record);
                        Console.WriteLine("Вы проиграли! Если хотите начать заново нажмите \"Y\", чтобы выйти нажмите \"N\"");
                        ConsoleKeyInfo key;
                        do
                        {
                            key = Console.ReadKey();
                        }
                        while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N);
                        
                        Console.Clear();
                        if (key.Key == ConsoleKey.Y)
                        {
                            isGame = true;
                            snakeLen = 10;
                            time = 100;
                        }
                        else if (key.Key == ConsoleKey.N)
                        {
                            isGame = false;
                        }
                    }
                }

                if (head_x == foodX && head_y == foodY)
                {
                    SpawnFood();
                    snakeLen++;
                    time--;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                for (int i = 0; i < snakeLen; i++)
                {
                    Console.SetCursorPosition(body_x[i], body_y[i]);
                    Console.Write("██");
                }
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.SetCursorPosition(head_x, head_y);
                Console.Write("██");

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(foodX, foodY);
                Console.Write("██");
                

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(0, 0);
                Console.Write("======================================================== Snake =========================================================");
                Console.WriteLine("Очки: " + (snakeLen - 10));

                System.Threading.Thread.Sleep(time);
            }

        }
    }
}
