using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using System.Reflection.Emit;
using System.Data;
using System.Threading.Tasks.Dataflow;
using System.Buffers;
using System.ComponentModel.Design;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Net.Sockets;

namespace Day20
{
    class Program
    {
        static readonly bool Testing = false;
        static readonly bool Debug = false;
        static string Input;
        static Dictionary<int, char[,]> Tiles = new Dictionary<int, char[,]>();
        static Dictionary<int, int> PieceEdges = new Dictionary<int, int>();
        static Dictionary<int, List<int>> ConnectsTo = new Dictionary<int, List<int>>();

        static char[,] MegaMap;
        static char[,] SeaMonster;

        static int SeaMonsterHashes; 

        static void Main()
        {
            GetDetails();
            Parse();
            QuestionOne();
            QuestionTwo();
            Console.WriteLine("Done");
        }

        static void QuestionOne()
        {
            Dictionary<int, List<string>> Edges = new Dictionary<int, List<string>>();

            foreach (var e in Tiles)
            {
                var edges = GetEdges(e.Value);
                Edges[e.Key] = edges;
            }

            foreach (var i in Edges)
            {
                ConnectsTo[i.Key] = new List<int>();
                foreach (var k in Edges)
                {
                    if (i.Key != k.Key && k.Value.Any(a => i.Value.Contains(a)))
                    {
                        ConnectsTo[i.Key].Add(k.Key);
                    }

                }
            }


            var keys = ConnectsTo.Where(x => x.Value.Count == 2);

            ulong answer = 1;

            foreach (var k in keys)
            {
                answer *= (ulong)k.Key;
            }

            //DoStuff
            Console.WriteLine($"Question One:{answer}");
        }

        static void QuestionTwo()
        {
            int answer = 0;
            ParseSeaMonster();
            var gridSize = (int)Math.Sqrt(Tiles.Count);

            int[,] tilePlacement = new int[gridSize, gridSize];

            PopulateGrid(tilePlacement, gridSize);

            SetPositions(0, tilePlacement);

            ResizeTiles();

            StitchThemTogether(tilePlacement);

            //DoStuff
            Console.WriteLine($"Question Two:{CountHashes() - (SearchTheMegaMap() * SeaMonsterHashes)}");
        }

        static int SearchTheMegaMap()
        {

            var maximumCount = 0;
            for (var i = 0; i < 4; i++)
            {
                if (i == 1)
                    MegaMap = Helper.FlipHorizontal(MegaMap);
                else if (i == 2)
                    MegaMap = Helper.FlipVertical(MegaMap);
                else if (i == 3)
                    MegaMap = Helper.FlipHorizontal(MegaMap);

                for (int rotateTop = 0; rotateTop < 4; rotateTop++)
                {
                    var result = FindSeaMonster();
                    if (result > maximumCount)
                        maximumCount = result;
                    MegaMap = Helper.RotateTile(MegaMap);
                }
            }

            return maximumCount;
        }

        static public int FindSeaMonster()
        {
            var count = 0;
            var seaMonsterXLength = SeaMonster.GetLength(0);
            var seaMonsterYLength = SeaMonster.GetLength(1);
            for (int y = 0; y < MegaMap.GetLength(0) - seaMonsterYLength; y++)
            {
                for (int x = 0; x < MegaMap.GetLength(0) - seaMonsterXLength; x++)
                {
                    var found = true;
                    for (int ySM = 0; ySM < SeaMonster.GetLength(1); ySM++)
                    {
                        for (int xSM = 0; xSM < SeaMonster.GetLength(0); xSM++)
                        {
                            if (SeaMonster[xSM, ySM] == '#')
                            {
                                if (MegaMap[x + xSM, y + ySM] != SeaMonster[xSM, ySM])
                                {
                                    found = false;
                                }
                            }
                        }
                    }
                    if (found)
                        count++;
                }
            }

            return count;
        }

        static int CountHashes()
        {
            var count = 0;
            foreach (var y in Enumerable.Range(0, MegaMap.GetLength(0)))
            {
                foreach (var x in Enumerable.Range(0, MegaMap.GetLength(0)))
                {
                    if (MegaMap[x, y] == '#')
                        count++;
                }
            }
            return count;
        }

        static public void StitchThemTogether(int[,] tilePlacement)
        {
            var xLength = Tiles.First().Value.GetLength(0) * tilePlacement.GetLength(0);
            MegaMap = new char[xLength, xLength];

            for (int y = 0; y < tilePlacement.GetLength(0); y++)
            {
                for (int x = 0; x < tilePlacement.GetLength(0); x++)
                {
                    var tile = Tiles[tilePlacement[x, y]];

                    for (int y1 = 0; y1 < tile.GetLength(0); y1++)
                    {
                        for (int x1 = 0; x1 < tile.GetLength(0); x1++)
                        {
                            var megaMapXCoord = x * Tiles.First().Value.GetLength(0) + x1;
                            var megaMapYCoord = y * Tiles.First().Value.GetLength(0) + y1;
                            MegaMap[megaMapXCoord, megaMapYCoord] = tile[x1, y1];
                        }
                    }
                }
            }
        }

        static public void ResizeTiles()
        {
            foreach (var tile in Tiles)
            {
                var newLength = tile.Value.GetLength(0) - 2;
                var newCharArray = new char[newLength, newLength];

                for (int y = 1; y < newLength + 1; y++)
                {
                    for (int x = 1; x < newLength + 1; x++)
                    {
                        newCharArray[x - 1, y - 1] = tile.Value[x, y];
                    }               
                }               

                Tiles[tile.Key] = newCharArray;
            }
        }

        static public void PopulateGrid(int[,] tilePlacement, int gridSize)
        {
            var corner = ConnectsTo.Where(x => x.Value.Count == 2).First();
            tilePlacement[0, 0] = corner.Key;
                        //Place the tiles
            while (Helper.ContainsNumber(tilePlacement, 0))
            {
                foreach (var y in Enumerable.Range(0, gridSize))
                {
                    foreach (var x in Enumerable.Range(0, gridSize))
                    {
                        if (tilePlacement[x, y] == 0)
                        {
                            var adjacentTiles = GetAdjacentTiles(x, y, tilePlacement);

                            var possibleIdList = adjacentTiles.First().Value.Where(a => adjacentTiles.Last().Value.Contains(a));

                            var possibleItems = ConnectsTo.Where(a => possibleIdList.Contains(a.Key) && !Helper.ContainsNumber(tilePlacement, a.Key));

                            tilePlacement[x, y] = possibleItems.Where(x => x.Value.Count == possibleItems.Select(x => x.Value.Count).Min()).First().Key;
                        }

                    }
                }
            }
        }

        static public List<KeyValuePair<int, List<int>>> GetAdjacentTiles(int x, int y, int[,] tilePlacement)
        {
            var adjacentTiles = new List<KeyValuePair<int, List<int>>>();

            if (x > 0)                            
                adjacentTiles.Add(new KeyValuePair<int, List<int>>(tilePlacement[x - 1, y], ConnectsTo[tilePlacement[x - 1, y]]));                            
            if (y > 0)
                adjacentTiles.Add(new KeyValuePair<int, List<int>>(tilePlacement[x, y - 1], ConnectsTo[tilePlacement[x, y - 1]]));

            return adjacentTiles;
        }

        static public bool SetPositions(int number, int[,] tilePlacement)
        {
            if (number >= Tiles.Count)
                return true;

            var xPosition = number % tilePlacement.GetLength(0);
            var yPosition = number / tilePlacement.GetLength(1);

            var tileNumber = tilePlacement[xPosition, yPosition];
            var tile = Tiles[tileNumber];

            var adjacentTiles = GetAdjacentTiles(xPosition, yPosition, tilePlacement);
            string directionToCheck = "";
            string toCheckAgainst = "";
            Tuple<int, int> coordinates;

            if (adjacentTiles.Count > 0)
            {
                var firstItem = adjacentTiles.First();
                coordinates = Helper.CoordinatesOf(tilePlacement, firstItem.Key);
                if (coordinates.Item1 < xPosition)
                {
                    directionToCheck = "left";
                    toCheckAgainst = Helper.GetRightEdge(Tiles[firstItem.Key]);
                }
                else if (coordinates.Item2 < yPosition)
                {
                    directionToCheck = "up";
                    toCheckAgainst = Helper.GetBottomEdge(Tiles[firstItem.Key]);
                }
            }

            for (var flipCounter = 0; flipCounter < 4; flipCounter++)
            {
                if (flipCounter == 1)
                    tile = Helper.FlipHorizontal(tile);
                else if (flipCounter == 2)
                    tile = Helper.FlipVertical(tile);
                else if (flipCounter == 3)
                    tile = Helper.FlipHorizontal(tile);

                for (int rotationCount = 0; rotationCount < 4; rotationCount++)
                {
                    Tiles[tileNumber] = tile;                    
                    bool correct = true;
                    if (directionToCheck == "left")
                    {
                        var leftEdge = Helper.GetLeftEdge(tile);
                        correct = leftEdge.Equals(toCheckAgainst);
                    }
                    else if (directionToCheck == "up")
                    {                        
                        var topEdge = Helper.GetTopEdge(tile);
                        correct = topEdge.Equals(toCheckAgainst);
                    }
                    if (correct)
                    {
                        var result = SetPositions(number + 1, tilePlacement);
                        if (result)
                            return true;
                    }
                    Tiles[tileNumber] = Helper.RotateTile(tile);
                }
            }

            return false;
        }


        public void Log(string s)
        {
            if (Debug)
            {
                Console.WriteLine(s);
            }
        }

        private static List<string> GetEdges(char[,] picture)
        {
            int xMax = picture.GetLength(0);
            int yMax = picture.GetLength(1);

            var xMinString = "";
            var xMaxString = "";
            foreach (var x in Enumerable.Range(0, xMax))
            {
                xMinString += picture[x, 0];
                xMaxString += picture[x, yMax - 1];
            }

            var yMinString = "";
            var yMaxString = "";
            foreach (var y in Enumerable.Range(0, yMax))
            {
                yMinString += picture[0, y];
                yMaxString += picture[xMax - 1, y];
            }

            var edges = new List<string> { xMinString, xMaxString, yMinString, yMaxString };
            var flippedEdges = new List<string>();

            foreach (var edge in edges)
            {
                string s = "";
                foreach (var i in Enumerable.Range(0, edge.Length).Reverse().ToList())
                {
                    s += edge[i];
                }
                flippedEdges.Add(s);
            }

            edges.AddRange(flippedEdges);

            return edges;
        }

        static void ParseSeaMonster()
        {
                    var seaMonster = @"                  #
#    ##    ##    ###
 #  #  #  #  #  #   ".Replace("\r", "").Replace(" ", ".");
            SeaMonsterHashes = seaMonster.Count(x => x == '#');

            var seaLines = seaMonster.Split('\n');

            SeaMonster = new char[seaLines[0].Length, seaLines.Length];

            foreach (var y in Enumerable.Range(0, seaLines.Length))
            {
                var seaLine = seaLines[y];
                foreach (var x in Enumerable.Range(0, seaLines[0].Length))
                {
                    SeaMonster[x, y] = seaLine[x];
                }
            }
        }

        static void Parse()
        {
            foreach (var tileFrame in Input.Split("\n\n"))
            {
                var tileLines = tileFrame.Trim().Split("\n");
                var tileImg = new char[tileLines.First().Length, tileLines.Length - 1];
                int title = 0;

                foreach (var y in Enumerable.Range(0, tileLines.Length))
                {

                    var workingLine = tileLines[y].Trim();
                    if (workingLine.Contains("Tile"))
                    {
                        title = Convert.ToInt32(workingLine.Split(" ")[1].Replace(":",""));
                    }
                    else
                    {
                        foreach (var x in Enumerable.Range(0, workingLine.Length))
                        {
                            tileImg[x, y - 1] = workingLine[x];
                        }
                    }
                }
                Tiles[title] = tileImg;
            }
        }

        static void Display(int i, char[,] picture)
        {
            var pictureString = $"Tile: {i}\n";
            foreach (var y in Enumerable.Range(0, picture.GetLength(1)))
            {
                foreach (var x in Enumerable.Range(0, picture.GetLength(0)))
                {
                    pictureString += (picture[x, y]);
                }
                pictureString += "\n";
            }
            pictureString += "\n";
            Console.WriteLine(pictureString);
        }

        static void GetDetails()
        {

            string input;
            //code with known good solutions.
            if (Testing)
                input = @"Tile 2311:
                            ..##.#..#.
                            ##..#.....
                            #...##..#.
                            ####.#...#
                            ##.##.###.
                            ##...#.###
                            .#.#.#..##
                            ..#....#..
                            ###...#.#.
                            ..###..###

                            Tile 1951:
                            #.##...##.
                            #.####...#
                            .....#..##
                            #...######
                            .##.#....#
                            .###.#####
                            ###.##.##.
                            .###....#.
                            ..#.#..#.#
                            #...##.#..

                            Tile 1171:
                            ####...##.
                            #..##.#..#
                            ##.#..#.#.
                            .###.####.
                            ..###.####
                            .##....##.
                            .#...####.
                            #.##.####.
                            ####..#...
                            .....##...

                            Tile 1427:
                            ###.##.#..
                            .#..#.##..
                            .#.##.#..#
                            #.#.#.##.#
                            ....#...##
                            ...##..##.
                            ...#.#####
                            .#.####.#.
                            ..#..###.#
                            ..##.#..#.

                            Tile 1489:
                            ##.#.#....
                            ..##...#..
                            .##..##...
                            ..#...#...
                            #####...#.
                            #..#.#.#.#
                            ...#.#.#..
                            ##.#...##.
                            ..##.##.##
                            ###.##.#..

                            Tile 2473:
                            #....####.
                            #..#.##...
                            #.##..#...
                            ######.#.#
                            .#...#.#.#
                            .#########
                            .###.#..#.
                            ########.#
                            ##...##.#.
                            ..###.#.#.

                            Tile 2971:
                            ..#.#....#
                            #...###...
                            #.#.###...
                            ##.##..#..
                            .#####..##
                            .#..####.#
                            #..#.#..#.
                            ..####.###
                            ..#.#.###.
                            ...#.#.#.#

                            Tile 2729:
                            ...#.#.#.#
                            ####.#....
                            ..#.#.....
                            ....#..#.#
                            .##..##.#.
                            .#.####...
                            ####.#.#..
                            ##.####...
                            ##..#.##..
                            #.##...##.

                            Tile 3079:
                            #.#.#####.
                            .#..######
                            ..#.......
                            ######....
                            ####.#..#.
                            .#...#.##.
                            #.#####.##
                            ..#.###...
                            ..#.......
                            ..#.###...";
            else
            {
                if (!File.Exists("input.txt"))
                {
                    //boilerplate grab info
                    var uri = "https://adventofcode.com/2020/day/20/input";
                    var sessionId = "";

                    var webRequest = WebRequest.Create(uri) as HttpWebRequest;
                    webRequest.CookieContainer = new CookieContainer();
                    var cookie = new Cookie("session", sessionId);
                    cookie.Domain = "adventofcode.com";

                    webRequest.CookieContainer.Add(cookie);
                    var responseStream = webRequest.GetResponse().GetResponseStream();

                    using (var fileStream = new FileStream("input.txt", FileMode.Create, FileAccess.Write))
                    {
                        responseStream.CopyTo(fileStream);
                    }
                }

                input = File.ReadAllText("input.txt");
            }

            Input = input.Replace("\r", "").Trim();
        }


    }
}
