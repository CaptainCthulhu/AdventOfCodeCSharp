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
        static readonly bool Debug = false;
        static string Input;        

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
            int answer = 0;
            //DoStuff
            Console.WriteLine($"Question One:{answer}");
        }

        static void QuestionTwo() 
        {
            int answer = 0;
           //DoStuff
            Console.WriteLine($"Question Two:{answer}");
        }

        public void Log(string s)
        {
            if (Debug || Testing)
            {
                Console.WriteLine(s);
            }
        }

        static void Parse()
        {           
            
        }

        static void GetDetails()
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

            Input = input.Replace("\r", "").Trim();
        }
    }  
}
