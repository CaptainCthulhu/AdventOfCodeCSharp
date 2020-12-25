using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day25
{
    class Program
    {
        static readonly bool Testing = false;
        static readonly bool Debug = false;
        static string Input;
        static List<ulong> PublicKeys = new List<ulong>();

        static void Main()
        {
            GetDetails();
            Parse();
            QuestionOne();
            Console.WriteLine("Done");
        }       

        static void QuestionOne() 
        {
            ulong subjectNumber = 7;
            ulong divideBy = 20201227;
            ulong value;

            List<ulong> LoopSizes = new List<ulong>();

            foreach (var i in PublicKeys)
            {                
                ulong result = 0;
                value = 1;
                do
                {
                    result++;
                    value *= subjectNumber;
                    value %= divideBy;
                } while (value != i);

                LoopSizes.Add(result);
            }

            value = 1;
            foreach (var i in Enumerable.Range(0, (int)LoopSizes[1]))
            {
                value *= PublicKeys[0];
                value %= divideBy;
            }

            //DoStuff
            Console.WriteLine($"Question One:{value}");
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
            foreach (var l in Input.Split("\n"))
                PublicKeys.Add(Convert.ToUInt64(l));
        }

        static void GetDetails()
        {

            string input;
            //code with known good solutions.
            if (Testing)
                input = @"5764801
17807724";
            else
            {
                if (!File.Exists("input.txt"))
                {
                    //boilerplate grab info
                    var uri = "https://adventofcode.com/2020/day/25/input";
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

/*
18075941
*/