using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DayThree
{
    class Program
    {
        static void Main(string[] args)
        {
            //now do real work
            var information = Parse(GetDetails().Trim());
            QuestionOne(information);
            QuestionTwo(information);
            Console.WriteLine("Done");
        }
        static void QuestionOne(char[,] grid)
        {
            Console.WriteLine($"Question One:{Travel(grid, 3, 1)}");
        }
        static void QuestionTwo(char[,] grid)
        {
            long answer = 0;
           //DoStuff
            answer = Travel(grid, 1, 1);
            answer *= Travel(grid, 3, 1);
            answer *= Travel(grid, 5, 1);
            answer *= Travel(grid, 7, 1);
            answer *= Travel(grid, 1, 2);
            Console.WriteLine($"Question Two:{answer}");
        }

        static int Travel(char[,] grid, int xMove, int yMove)
        {
            var yMax = grid.GetLength(1);
            var xMax = grid.GetLength(0);
            int answer = 0;
            int x = 0;
            int y= 0;
            while (y < yMax)
            {

                if (grid[x % xMax, y] == '#')
                    answer++;

                x += xMove;
                y += yMove;
            }

            return answer;            
        }

        static char[,] Parse(string input)
        {
            var lines = input.Split('\n');
            var array = new char[lines[0].Length, lines.Length];
            for(var y = 0; y < lines.Length; ++y)
            {
                var line = lines[y];
                for(var x = 0; x < line.Length; x++)
                {
                    array[x, y] = line[x];
                }
            }
            return array;
        }

        static string GetDetails()
        {
            //boilerplate grab info
            var uri = "https://adventofcode.com/2020/day/3/input";
            var sessionId = "";

            var webRequest = WebRequest.Create(uri) as HttpWebRequest;
            webRequest.CookieContainer = new CookieContainer();
            var cookie = new Cookie("session", sessionId);
            cookie.Domain = "adventofcode.com";

            webRequest.CookieContainer.Add(cookie);
            var responseStream = webRequest.GetResponse().GetResponseStream();
            var reader = new StreamReader(responseStream);
            return reader.ReadToEnd();
        }

        static void DrawGrid(char[,] grid)
        {
            string answer = "";

            for(var y = 0; y < grid.GetLength(1); y++)
            {
                for(var x = 0; x < grid.GetLength(0); x++)
                {
                answer += grid[x,y];
                }
                answer += "\n";
            }

            Console.WriteLine(answer);
        }
    }
}
