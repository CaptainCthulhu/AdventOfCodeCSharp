using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day18
{
    class Program
    {
        static readonly bool Testing = false;
        static string Input;
        static List<string> Equations;

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
            List<ulong> answers = new List<ulong>();
            var equations = Equations.Select(x => (string)x.Clone()).ToList();
            foreach(var equation in equations)
            {
                var input = equation.Trim().Replace("(", "( ").Replace(")", " )").Split(" ").ToList();
                answers.Add(SolveQ1(input));
            }

            Console.WriteLine($"Question One:{answers.Sum(x => (decimal)x)}");
        }

        static void QuestionTwo() 
        {
            List<ulong> answers = new List<ulong>();
            var equations = Equations.Select(x => (string)x.Clone()).ToList();
            foreach(var equation in equations)
            {
                var input = equation.Trim().Replace("(", "( ").Replace(")", " )").Split(" ").ToList();
                answers.Add(SolveQ2(input));
            }

            Console.WriteLine($"Question Two:{answers.Sum(x => (decimal)x)}");
        }

        static void Parse()
        {
            Equations = new List<String>();

            foreach (var i in Input.Trim().Split('\n'))
                Equations.Add(i);
        }

        static ulong SolveQ1(List<string> s)
        {
            //brackets first
            for (int i = 0; i < s.Count; i++)
            {
                if (s[i] == "(")
                {                    
                    var startBracketCount = 1;
                    var endBracketCount = 0;
                    var currentSubIndex = 0;
                    

                    while (startBracketCount != endBracketCount)
                    {
                        currentSubIndex++;
                        if (s[i + currentSubIndex] == "(")
                            startBracketCount++;
                        else if (s[i + currentSubIndex] == ")")
                            endBracketCount++;
                    }

                    var subAnswer = SolveQ1(s.GetRange(i+1,currentSubIndex-1));
                    s.RemoveRange(i, currentSubIndex+1);
                    s.Insert(i, subAnswer.ToString());
                }                
            }

            while(s.Count > 1)
            {
                
                if (s[1] == "+" || s[1] == "*")
                {
                    ulong mathAnswer = 0;
                    if (s[1] == "+")
                    {
                        mathAnswer = Convert.ToUInt64(s[0]) + Convert.ToUInt64(s[2]);
                    }
                    else if (s[1] == "*")
                    {
                        mathAnswer = Convert.ToUInt64(s[0]) * Convert.ToUInt64(s[2]);
                    }

                    s.RemoveRange(0, 3);

                    s.Insert(0, mathAnswer.ToString());
                }
            }

            return (ulong)s.Sum(s => Convert.ToDecimal(s));
        }        

        static ulong SolveQ2(List<string> s)
        {
            //brackets first
            for (int i = 0; i < s.Count; i++)
            {
                if (s[i] == "(")
                {
                    var startBracketCount = 1;
                    var endBracketCount = 0;
                    var currentSubIndex = 0;


                    while (startBracketCount != endBracketCount)
                    {
                        currentSubIndex++;
                        if (s[i + currentSubIndex] == "(")
                            startBracketCount++;
                        else if (s[i + currentSubIndex] == ")")
                            endBracketCount++;
                    }

                    var subAnswer = SolveQ2(s.GetRange(i + 1, currentSubIndex - 1));
                    s.RemoveRange(i, currentSubIndex + 1);
                    s.Insert(i, subAnswer.ToString());
                }
            }

            for (int i = 0; i < s.Count; i++)
            {
                if (s[i] == "+")
                {                    
                    ulong mathAnswer = Convert.ToUInt64(s[i-1]) + Convert.ToUInt64(s[i+1]);
                    i--;
                    s.RemoveRange(i, 3);
                    s.Insert(i, mathAnswer.ToString());
                }
            }

            while (s.Count > 1)
            {
                if (s[1] == "*")
                {
                    ulong  mathAnswer = Convert.ToUInt64(s[0]) * Convert.ToUInt64(s[2]);                    
                    s.RemoveRange(0, 3);
                    s.Insert(0, mathAnswer.ToString());
                }
            }

            return (ulong)s.Sum(s => Convert.ToDecimal(s));
        }

        static void GetDetails()
        {

            string input;
            //code with known good solutions.
            if (Testing)
                input = @"5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))";
            else
            {
                if (!File.Exists("input.txt"))
                {
                    //boilerplate grab info
                    var uri = "https://adventofcode.com/2020/day/18/input";
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

            Input = input;
        }
    }  
}
