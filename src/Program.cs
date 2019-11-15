using System.Threading;
using System;

namespace Conway
{
    class Program
    {
        static string cellColorCode = "\u001b[97m";
        static string backgroundColorCode = "\u001b[30m";
        static string blockCharacter = "\u2588";

        static int FPS = 50;

        static int xMax = (int) Math.Floor((double)Console.WindowWidth / 2);
        static int yMax = Console.WindowHeight - 1;

        static bool[,] map = new bool[yMax, xMax];
        static bool[,] nextGen = new bool[yMax, xMax];

        static void Main(string[] args)
        {
            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    map[y, x] = false;
                }
            }

            int middleX = xMax / 2;
            int middleY = yMax / 2;

            map[middleY - 1, middleX - 1] = true;
            map[middleY - 1, middleX] = true;
            map[middleY, middleX] = true;
            map[middleY, middleX + 1] = true;
            map[middleY + 1, middleX] = true;

            int generation = 0;

            while (true)
            {
                Thread.Sleep(1000 / FPS);
                renderConwaysGrid(map);
                updateConwaysGrid(map);
                Console.Write(cellColorCode + "\nX: " + xMax + " Y: " + yMax + " Generation: " + generation);
                generation++;
            }
        }

        static bool checkCell(int y, int x)
        {
            int xx = x;
            int yy = y;
            if (x < 0)
            {
                xx = xMax - 1;
            }
            if (x >= xMax)
            {
                xx = 0;
            }
            if (y < 0)
            {
                yy = yMax - 1;
            }
            if (y >= yMax)
            {
                yy = 0;
            }
            return map[yy, xx];
        }

        public static void updateConwaysGrid(bool[,] map)
        {
            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    nextGen[y, x] = false;
                }
            }

            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    int neighbours = 0;
                    neighbours += checkCell(y - 1, x - 1) ? 1 : 0;
                    neighbours += checkCell(y - 1, x) ? 1 : 0;
                    neighbours += checkCell(y - 1, x + 1) ? 1 : 0;
                    neighbours += checkCell(y, x - 1) ? 1 : 0;
                    neighbours += checkCell(y, x + 1) ? 1 : 0;
                    neighbours += checkCell(y + 1, x - 1) ? 1 : 0;
                    neighbours += checkCell(y + 1, x) ? 1 : 0;
                    neighbours += checkCell(y + 1, x + 1) ? 1 : 0;

                    if (map[y, x])
                    {
                        if (neighbours < 2 || neighbours > 3)
                        {
                            nextGen[y, x] = false;
                        }
                        else
                        {
                            nextGen[y, x] = true;
                        }
                    }
                    else
                    {
                        if (neighbours == 3)
                        {
                            nextGen[y, x] = true;
                        }
                    }
                }
            }

            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    map[y, x] = nextGen[y, x];
                }
            }
        }

        public static void renderConwaysGrid(bool[,] map)
        {
            Console.SetCursorPosition(0, 0);

            string frame = "";

            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (map[y, x])
                    {
                        frame += cellColorCode;
                    }
                    else
                    {
                        frame += backgroundColorCode;
                    }
                    frame += blockCharacter + blockCharacter;
                }

                if (y < yMax - 1)
                {
                    frame += "\n";
                }
            }

            Console.Write(frame);
        }
    }
}

