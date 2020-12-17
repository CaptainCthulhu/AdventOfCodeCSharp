using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Versioning;
using System.Diagnostics;

namespace Day17
{
    class Program
    {
        static readonly bool Testing = false;    
        static readonly bool Debugging = false;
        static readonly List<Point3> OriginalPoints3D = new List<Point3>();
        static readonly List<Point4> OriginalPoints4D = new List<Point4>();

        static void Main()
        {
            var details = GetDetails();
            Parse3D(details);
            Parse4D(details);
            QuestionOne();
            QuestionTwo();            
            Console.WriteLine("Done");
        }

        static void QuestionOne()
        {
            List<Point3> lastFrame = OriginalPoints3D.Select(x => x).ToList();
            var endGame = 7;
            var turn = 1;

            List<Point3> newFrame;

            if (Debugging)
                Display(lastFrame, 0);

            while (turn < endGame)
            {
                newFrame = new List<Point3>();
                var zMin = lastFrame.Select(i => i.Z).Min();
                var zMax = lastFrame.Select(i => i.Z).Max();
                var yMin = lastFrame.Select(i => i.Y).Min();
                var yMax = lastFrame.Select(i => i.Y).Max();                
                var xMin = lastFrame.Select(i => i.X).Min();
                var xMax = lastFrame.Select(i => i.X).Max();

                for (long z = zMin - 1; z <= zMax + 1; z++)
                {
                    for (long y = yMin - 1; y <= yMax + 1; y++)
                    {
                        for (long x = xMin - 1; x <= xMax + 1; x++)
                        {
                            var hasElement = lastFrame.Any(point => point.X == x && point.Y == y && point.Z == z);
                            var adjacentCount = GetAdjacent3D(x, y, z, lastFrame);
                            if ((hasElement && adjacentCount == 2) || adjacentCount == 3)
                            {
                                newFrame.Add(new Point3(x, y, z));
                            }
                        }
                    }
                }

                lastFrame = newFrame.Select(x => x).ToList();
                if (Debugging)
                    Display(lastFrame, turn);
                turn++;
            }

            Console.WriteLine($"Question One:{lastFrame.Count}");
        }

        static void QuestionTwo() 
        {
            //Clearly this could be optimized to only search AROUND known points, but fast enough.
            List<Point4> lastFrame = OriginalPoints4D.Select(x => x).ToList();
            var endGame = 7;
            var turn = 1;

            List<Point4> newFrame;
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            while (turn < endGame)
            {
                Console.WriteLine($"Turn {turn} Elasped Time: {stopWatch.Elapsed}");
                newFrame = new List<Point4>();
                var wMin = lastFrame.Select(i => i.W).Min(); 
                var wMax = lastFrame.Select(i => i.W).Max(); 
                var zMin = lastFrame.Select(i => i.Z).Min();
                var zMax = lastFrame.Select(i => i.Z).Max();
                var yMin = lastFrame.Select(i => i.Y).Min();
                var yMax = lastFrame.Select(i => i.Y).Max();                
                var xMin = lastFrame.Select(i => i.X).Min();
                var xMax = lastFrame.Select(i => i.X).Max();

                for (long w = zMin - 1; w <= zMax + 1; w++)
                {
                    for (long z = zMin - 1; z <= zMax + 1; z++)
                    {
                        for (long y = yMin - 1; y <= yMax + 1; y++)
                        {
                            for (long x = xMin - 1; x <= xMax + 1; x++)
                            {
                                var hasElement = lastFrame.Any(point => point.X == x && point.Y == y && point.Z == z && point.W == w);
                                var adjacentCount = GetAdjacent4D(x, y, z, w, lastFrame);
                                if ((hasElement && adjacentCount == 2) || adjacentCount == 3)
                                {
                                    newFrame.Add(new Point4(x, y, z, w));
                                }
                            }
                        }
                    }
                }

                lastFrame = newFrame.Select(x => x).ToList();
                turn++;
            }
            Console.WriteLine($"Question Two:{lastFrame.Count}");
        }

        static int GetAdjacent3D(long X, long Y, long Z, List<Point3> Points)
        {
            int count = 0;

            for (long z = Z - 1; z <= Z + 1; z++)
            {
                for (long y = Y - 1; y <= Y + 1; y++)
                {
                    for (long x = X - 1; x <= X + 1; x++)
                    {
                        if ((x != X || y != Y || z != Z) && Points.Any(point => point.Equals(x,y,z)))
                        {
                            count++;
                        }
                        
                    }
                }
            }

            return count;
        }

        static int GetAdjacent4D(long X, long Y, long Z, long W, List<Point4> Points)
        {
            int count = 0;
            for (long w = W - 1; w <= W + 1; w++)
            {
                for (long z = Z - 1; z <= Z + 1; z++)
                {
                    for (long y = Y - 1; y <= Y + 1; y++)
                    {
                        for (long x = X - 1; x <= X + 1; x++)
                        {
                            if ((x != X || y != Y || z != Z || w != W) && Points.Any(point => point.Equals(x, y, z, w)))
                            {
                                count++;
                            }

                        }
                    }
                }
            }

            return count;
        }


        static void Display(List<Point3> Points, int Turn)
        {
            for (long z = Points.Select(z => z.Z).Min() - 1; z < Points.Select(z => z.Z).Max() + 2; z++)
            {
                Console.WriteLine($"Turn {Turn} z-Level: {z} zMin = {Points.Select(z => z.Z).Min()} zMax = {Points.Select(z => z.Z).Max()}" +
                $" yMin = {Points.Select(y => y.Y).Min()} yMax = {Points.Select(y => y.Y).Max()}" +
                $" xMin = {Points.Select(x => x.X).Min()} xMax = {Points.Select(x => x.X).Max()}");

                string output = "";
                for (long y = Points.Select(y => y.Y).Min() - 1; y < Points.Select(y => y.Y).Max() + 2; y++)
                {
                    for (long x = Points.Select(x => x.X).Min() - 1; x < Points.Select(x => x.X).Max() + 2; x++)
                    {
                        if (Points.Any(point => point.Equals(x,y,z)))
                            output += "#";
                        else
                            output += ".";
                    }
                    output += '\n';
                }
                Console.WriteLine(output);
            }

            Console.WriteLine("\n\n");
        }

        static void Parse3D(string input)
        {
            var lines = input.Trim().Split('\n');
            int z = 1;
            foreach(int y in Enumerable.Range(0, lines.Length))
            {
                foreach(int x in Enumerable.Range(0, lines[y].Length))
                {
                    if (lines[y][x] == '#')
                        OriginalPoints3D.Add(new Point3(x,y,z));
                }
            }
            
        }

        static void Parse4D(string input)
        {
            var lines = input.Trim().Split('\n');
            int z = 1;
            int w = 1;
            foreach(int y in Enumerable.Range(0, lines.Length))
            {
                foreach(int x in Enumerable.Range(0, lines[y].Length))
                {
                    if (lines[y][x] == '#')
                        OriginalPoints4D.Add(new Point4(x,y,z,w));
                }
            }
            
        }

        static string GetDetails()
        {

            string input;
            //code with known good solutions.
            if (Testing)
                input = @".#.
..#
###";
            else
            {
                if (!File.Exists("input.txt"))
                {
                    //boilerplate grab info
                    var uri = "https://adventofcode.com/2020/day/17/input";
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

            return input;
        }
    }

    public struct Point3
    {
        public long X;
        public long Y;
        public long Z;


        public Point3(int x, int y, int z)
        {
            X = (long)x;
            Y = (long)y;
            Z = (long)z;            
        }

        public Point3(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public bool Equals(Point3 point)        
        {            
            return point.X == X && point.Y == Y && point.Z == Z;
        }       
        public bool Equals(long x, long y, long z)        
        {            
            return X == x && Y == y && Z == z;
        }    
    }

    public struct Point4
    {
        public long X;
        public long Y;
        public long Z;
        public long W;

        public Point4(long x, long y, long z, long w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }        

        public bool Equals(Point4 point)        
        {            
            return point.X == X && point.Y == Y && point.Z == Z && point.W == W;
        }       
        public bool Equals(long x, long y, long z, long w)        
        {            
            return X == x && Y == y && Z == z && W == w;
        }    
    }
}