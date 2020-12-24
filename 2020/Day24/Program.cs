using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace Day24
{
    class Program
    {
        static readonly bool Testing = false;
        static readonly bool Debug = false;
        static string Input;
        static List<List<string>> InstructionsLists;
        static HashSet<(int, int)> Tiles = new HashSet<(int, int)>();

        static Dictionary<string, (int, int)> Directions = new Dictionary<string, (int, int)>
        {
            {"e",(1, 0)},
            {"ne",(1,-1)},
            {"nw",(0, -1)},
            {"w",(-1, 0)},
            {"sw",(-1, 1)},
            {"se",(0, 1)}
        };

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
            Walk();
            Console.WriteLine($"Question One: {Tiles.Count()}");
        }
        static void QuestionTwo()
        {
            foreach (var i in Enumerable.Range(0, 100))
            {
                Tiles = HexGameOfLife();
            }
            Console.WriteLine($"Question Two: {Tiles.Count()}");
        }

        static void Walk()
        {
            foreach (var instructions in InstructionsLists)
            {
                var currentPoint = (0, 0);
                foreach (var instruction in instructions)                
                    currentPoint = NewLocation(currentPoint, Directions[instruction]);                
                
                if (Tiles.Contains(currentPoint))
                    Tiles.Remove(currentPoint);                
                else
                    Tiles.Add(currentPoint);
            }
        }

        static HashSet<(int,int)> HexGameOfLife()
        {
            HashSet<(int, int)> newTiles = new HashSet<(int, int)>();

            foreach (var element in Tiles)
            {
                //Search the core
                SearchCoordinate(element, (0, 0), newTiles);
                //Search the rest
                foreach (var value in Directions.Values)
                {
                    SearchCoordinate(element, value, newTiles);
                }                
                
            }

            return newTiles;
        }

        static void SearchCoordinate((int, int) originalCoordinates, (int, int) movementDirections,  HashSet<(int, int)> newTiles)
        {
            var newLocation = NewLocation(originalCoordinates, movementDirections);

            if (Tiles.Contains(newLocation))
            {
                var blackTileCount = SearchAround(newLocation);
                if (blackTileCount != 0 && blackTileCount < 3)
                    newTiles.Add(newLocation);
            }
            else if (!newTiles.Contains(newLocation) && SearchAround(newLocation) == 2)
                newTiles.Add(newLocation);
        }

        static int SearchAround((int, int) coordinates)
        {
            var blackTileCount = 0;
            foreach (var value in Directions.Values)
            {
                blackTileCount += HasElement(coordinates, value);
            }

            return blackTileCount;
        }

        static int HasElement((int,int) originalCoordinates, (int,int)movementDirection)
        {
            var newLocation = NewLocation(originalCoordinates, movementDirection);
            return Tiles.Contains(newLocation) ? 1 : 0;

        }

        static (int, int) NewLocation((int, int) originalCoordinates, (int, int) movementDirection)
        {
            return (originalCoordinates.Item1 + movementDirection.Item1, originalCoordinates.Item2 + movementDirection.Item2);
        }

        public static void Log(string s)
        {
            if (Debug || Testing)
            {
                Console.WriteLine(s);
            }
        }

        static void Parse()
        {
            InstructionsLists = new List<List<string>>();
            var lines = Input.Split('\n');
            foreach (var e in lines)
            {
                var tempList = new List<string>();
                Queue<char> input = new Queue<char>(e);
                while (input.Count > 0)
                {
                    string answer = input.Dequeue().ToString();
                    if (answer == "s" || answer == "n")
                        answer += input.Dequeue().ToString();
                    tempList.Add(answer);
                }
                InstructionsLists.Add(tempList);
            }
        }

        static void GetDetails()
        {

            string input;
            //code with known good solutions.
            if (Testing)
                input = @"sesenwnenenewseeswwswswwnenewsewsw
neeenesenwnwwswnenewnwwsewnenwseswesw
seswneswswsenwwnwse
nwnwneseeswswnenewneswwnewseswneseene
swweswneswnenwsewnwneneseenw
eesenwseswswnenwswnwnwsewwnwsene
sewnenenenesenwsewnenwwwse
wenwwweseeeweswwwnwwe
wsweesenenewnwwnwsenewsenwwsesesenwne
neeswseenwwswnwswswnw
nenwswwsewswnenenewsenwsenwnesesenew
enewnwewneswsewnwswenweswnenwsenwsw
sweneswneswneneenwnewenewwneswswnese
swwesenesewenwneswnwwneseswwne
enesenwswwswneneswsenwnewswseenwsese
wnwnesenesenenwwnenwsewesewsesesew
nenewswnwewswnenesenwnesewesw
eneswnwswnwsenenwnwnwwseeswneewsenese
neswnwewnwnwseenwseesewsenwsweewe
wseweeenwnesenwwwswnew";
            else
            {
                if (!File.Exists("input.txt"))
                {
                    //boilerplate grab info
                    var uri = "https://adventofcode.com/2020/day/24/input";
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


