using System;
using System.Collections.Generic;
using System.Threading;

namespace MyMonsters
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Console.Write("Введите высоту карты (61 MAX): ");
            int mapSizeY = Convert.ToInt32(Console.ReadLine());
            Console.WindowHeight = mapSizeY + 5;
            Console.Clear();
            Console.Write("Введите ширину карты (187 MAX): ");
            int mapSizeX = Convert.ToInt32(Console.ReadLine());
            Console.WindowWidth = mapSizeX + 5;
            Console.Clear();
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;

            List<Zones> zones = new List<Zones>
            {
               new Zones("O", ConsoleColor.DarkGray),
               new Zones("Ø", ConsoleColor.Green),
               new Zones("#", ConsoleColor.White),
               new Zones("$", ConsoleColor.DarkCyan),
               new Zones("@", ConsoleColor.Red),
               new Zones("+", ConsoleColor.White)
            };

            Random random = new Random();

            string[,] map = new string[mapSizeX, mapSizeY];

            GenMap(mapSizeX, mapSizeY, zones, map);

            GetMap(mapSizeX, mapSizeY, zones, map);

            //Console.ReadKey();

            Evo1(mapSizeY, mapSizeX, zones, map);

            //Console.ReadKey();

            while (true)
            {
                GetMap(mapSizeX, mapSizeY, zones, map);

                GetNewLive(mapSizeX, mapSizeY, zones, random, map);

                Evo1(mapSizeY, mapSizeX, zones, map);                
                Evo2(mapSizeY, mapSizeX, zones, map);
                Evo3(mapSizeY, mapSizeX, zones, map);
                Evo4(mapSizeY, mapSizeX, zones, map);

                //Console.ReadKey();
            }
        }

        private static void Evo4(int mapSizeY, int mapSizeX, List<Zones> zones, string[,] map)
        {
            for (int y = 0; y < mapSizeY; y++) // 3
            {
                for (int x = 0; x < mapSizeX; x++)
                {
                    Evo(mapSizeX, mapSizeY, zones[3].Symbol, zones[4].Symbol, zones[5].Symbol, map, y, x, zones[0].Symbol);

                    GetMap(mapSizeX, mapSizeY, zones, map);
                }
            }
        }

        private static void Evo3(int mapSizeY, int mapSizeX, List<Zones> zones, string[,] map)
        {
            for (int y = 0; y < mapSizeY; y++) // 3
            {
                for (int x = 0; x < mapSizeX; x++)
                {
                    Evo(mapSizeX, mapSizeY, zones[2].Symbol, zones[3].Symbol, zones[4].Symbol, map, y, x, zones[0].Symbol);

                    GetMap(mapSizeX, mapSizeY, zones, map);
                }
            }
        }

        private static void Evo2(int mapSizeY, int mapSizeX, List<Zones> zones, string[,] map)
        {
            for (int y = 0; y < mapSizeY; y++) // 2
            {
                for (int x = 0; x < mapSizeX; x++)
                {
                    Evo(mapSizeX, mapSizeY, zones[1].Symbol, zones[2].Symbol, zones[3].Symbol, map, y, x, zones[0].Symbol);

                    GetMap(mapSizeX, mapSizeY, zones, map);
                }
            }
        }

        private static void Evo1(int mapSizeY, int mapSizeX, List<Zones> zones, string[,] map)
        {
            for (int y = 0; y < mapSizeY; y++) // 1
            {
                for (int x = 0; x < mapSizeX; x++)
                {
                    Evo(mapSizeX, mapSizeY, zones[0].Symbol, zones[1].Symbol, zones[2].Symbol, map, y, x, zones[0].Symbol);

                    GetMap(mapSizeX, mapSizeY, zones, map);
                }
            }
        }

        private static void GetNewLive(int mapSizeX, int mapSizeY, List<Zones> zones, Random random, string[,] map)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                for (int x = 0; x < mapSizeX; x++)
                {
                    if (map[x, y] == zones[0].Symbol)
                    {
                        int randNum = random.Next(1, 10);

                        if (randNum >= 7)
                        {
                            map[x, y] = zones[1].Symbol;
                        }

                        GetMap(mapSizeX, mapSizeY, zones, map);
                    }
                }
            }
        }

        private static void GetMap(int mapSizeX, int mapSizeY, List<Zones> zones, string[,] map)
        {
            for (int i = 0; i < mapSizeY; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - mapSizeX / 2, Console.WindowHeight / 2 - mapSizeY / 2 + i);
                for (int j = 0; j < mapSizeX; j++)
                {
                    if (map[j, i] == zones[0].Symbol)
                    {
                        Console.ForegroundColor = zones[0].Color;
                    }
                    else if (map[j, i] == zones[1].Symbol)
                    {
                        Console.ForegroundColor = zones[1].Color;
                    }
                    else if (map[j, i] == zones[2].Symbol)
                    {
                        Console.ForegroundColor = zones[2].Color;
                    }
                    else if (map[j, i] == zones[3].Symbol)
                    {
                        Console.ForegroundColor = zones[3].Color;
                    }
                    else if (map[j, i] == zones[4].Symbol)
                    {
                        Console.ForegroundColor = zones[4].Color;
                    }

                    Console.Write(map[j, i]);
                }
            }
        }

        private static void Evo(int mapSizeX, int mapSizeY, string lowerZone, string middleZone, string highZone, string[,] map, int y, int x, string emptyZone)
        {
            int countLiveZone;
            XY[] deleteZones = new XY[8];
            if (map[x, y] == emptyZone)
            {
                countLiveZone = 0;
                if (x == 0 & y == 0)
                {
                    if (map[x + 1, y] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[0] = new XY(x + 1, y);
                    }
                    if (map[x, y + 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[1] = new XY(x, y + 1);
                    }
                    if (map[x + 1, y + 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[2] = new XY(x + 1, y + 1);
                    }
                }
                else if (x == mapSizeX - 1 & y == 0)
                {
                    if (map[x - 1, y] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[0] = new XY(x - 1, y);
                    }
                    if (map[x - 1, y + 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[1] = new XY(x - 1, y + 1);
                    }
                    if (map[x, y + 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[2] = new XY(x, y + 1);
                    }
                }
                else if (y == 0)
                {
                    if (map[x + 1, y] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[0] = new XY(x + 1, y);
                    }
                    if (map[x, y + 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[1] = new XY(x, y + 1);
                    }
                    if (map[x + 1, y + 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[2] = new XY(x + 1, y + 1);
                    }
                    if (map[x - 1, y] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[3] = new XY(x - 1, y);
                    }
                    if (map[x - 1, y + 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[4] = new XY(x - 1, y + 1);
                    }
                }
                else if (x == 0 & y == mapSizeY - 1)
                {
                    if (map[x + 1, y] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[0] = new XY(x + 1, y);
                    }
                    if (map[x + 1, y - 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[1] = new XY(x + 1, y - 1);
                    }
                    if (map[x, y - 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[2] = new XY(x, y - 1);
                    }
                }
                else if (x == 0)
                {
                    if (map[x + 1, y] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[0] = new XY(x + 1, y);
                    }
                    if (map[x, y + 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[1] = new XY(x, y + 1);
                    }
                    if (map[x + 1, y + 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[2] = new XY(x + 1, y + 1);
                    }
                    if (map[x, y - 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[3] = new XY(x, y - 1);
                    }
                    if (map[x + 1, y - 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[4] = new XY(x + 1, y - 1);
                    }
                }
                else if (x == mapSizeX - 1 & y == mapSizeY - 1)
                {
                    if (map[x - 1, y] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[0] = new XY(x - 1, y);
                    }
                    if (map[x - 1, y - 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[1] = new XY(x - 1, y - 1);
                    }
                    if (map[x, y - 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[2] = new XY(x, y - 1);
                    }
                }
                else if (x == mapSizeX - 1)
                {
                    if (map[x - 1, y] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[0] = new XY(x - 1, y);
                    }
                    if (map[x - 1, y - 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[1] = new XY(x - 1, y - 1);
                    }
                    if (map[x, y + 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[2] = new XY(x, y + 1);
                    }
                    if (map[x, y - 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[3] = new XY(x, y - 1);
                    }
                    if (map[x - 1, y + 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[4] = new XY(x - 1, y + 1);
                    }
                }
                else if (y == mapSizeY - 1)
                {
                    if (map[x - 1, y] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[0] = new XY(x - 1, y);
                    }
                    if (map[x - 1, y - 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[1] = new XY(x - 1, y - 1);
                    }
                    if (map[x, y - 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[2] = new XY(x, y - 1);
                    }
                    if (map[x + 1, y - 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[3] = new XY(x + 1, y - 1);
                    }
                    if (map[x + 1, y] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[4] = new XY(x + 1, y);
                    }
                }
                else
                {
                    if (map[x - 1, y - 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[0] = new XY(x - 1, y - 1);
                    }
                    if (map[x, y - 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[1] = new XY(x, y - 1);
                    }
                    if (map[x + 1, y - 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[2] = new XY(x + 1, y - 1);
                    }
                    if (map[x + 1, y] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[3] = new XY(x + 1, y);
                    }
                    if (map[x + 1, y + 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[4] = new XY(x + 1, y + 1);
                    }
                    if (map[x, y + 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[5] = new XY(x, y + 1);
                    }
                    if (map[x - 1, y + 1] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[6] = new XY(x - 1, y + 1);
                    }
                    if (map[x - 1, y] == middleZone)
                    {
                        countLiveZone++;
                        deleteZones[7] = new XY(x - 1, y);
                    }
                }

                if (countLiveZone >= 3)
                {
                    map[x, y] = highZone;
                    int j = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        if (deleteZones[i] != null && j < 3)
                        {                       
                            map[deleteZones[i].X, deleteZones[i].Y] = emptyZone; 
                            j++;
                        }
                    }
                }

                //if (countLiveZone >= 3)
                //{
                //    map[x, y] = highZone;
                //
                //    if (x == 0 & y == 0)
                //    {
                //        map[x + 1, y] = lowerZone;
                //        map[x, y + 1] = lowerZone;
                //        map[x + 1, y + 1] = lowerZone;
                //    }
                //    else if (x == mapSizeX - 1 & y == 0)
                //    {
                //        map[x - 1, y] = lowerZone;
                //        map[x - 1, y + 1] = lowerZone;
                //        map[x, y + 1] = lowerZone;
                //    }
                //    else if (y == 0)
                //    {
                //        map[x + 1, y] = lowerZone;
                //        map[x, y + 1] = lowerZone;
                //        map[x + 1, y + 1] = lowerZone;
                //        map[x - 1, y] = lowerZone;
                //        map[x - 1, y + 1] = lowerZone;
                //    }
                //    else if (x == 0 & y == mapSizeY - 1)
                //    {
                //        map[x + 1, y] = lowerZone;
                //        map[x + 1, y - 1] = lowerZone;
                //        map[x, y - 1] = lowerZone;
                //    }
                //    else if (x == 0)
                //    {
                //        map[x + 1, y] = lowerZone;
                //        map[x, y + 1] = lowerZone;
                //        map[x + 1, y + 1] = lowerZone;
                //        map[x, y - 1] = lowerZone;
                //        map[x + 1, y - 1] = lowerZone;
                //    }
                //    else if (x == mapSizeX - 1 & y == mapSizeY - 1)
                //    {
                //        map[x - 1, y] = lowerZone;
                //        map[x - 1, y - 1] = lowerZone;
                //        map[x, y - 1] = lowerZone;
                //    }
                //    else if (x == mapSizeX - 1)
                //    {
                //        map[x - 1, y] = lowerZone;
                //        map[x - 1, y - 1] = lowerZone;
                //        map[x, y + 1] = lowerZone;
                //        map[x, y - 1] = lowerZone;
                //        map[x - 1, y + 1] = lowerZone;
                //    }
                //    else if (y == mapSizeY - 1)
                //    {
                //        map[x - 1, y] = lowerZone;
                //        map[x - 1, y - 1] = lowerZone;
                //        map[x, y - 1] = lowerZone;
                //        map[x + 1, y - 1] = lowerZone;
                //        map[x + 1, y] = lowerZone;
                //    }
                //    else
                //    {
                //        map[x - 1, y - 1] = lowerZone;
                //        map[x, y - 1] = lowerZone;
                //        map[x + 1, y - 1] = lowerZone;
                //        map[x + 1, y] = lowerZone;
                //        map[x + 1, y + 1] = lowerZone;
                //        map[x, y + 1] = lowerZone;
                //        map[x - 1, y + 1] = lowerZone;
                //        map[x - 1, y] = lowerZone;
                //    }             
                //}
            }
        }

        private static void GenMap(int mapSizeX, int mapSizeY, List<Zones> zones, string[,] map)
        {
            Random random = new Random();
            for (int i = 0; i < mapSizeY; i++)
            {
                for (int j = 0; j < mapSizeX; j++)
                {
                    int randNum = random.Next(1, 10);

                    if (randNum <= 5)
                        map[j, i] = zones[0].Symbol;
                    if (randNum > 5)
                        map[j, i] = zones[1].Symbol;
                }
            }
        }
    }
}
