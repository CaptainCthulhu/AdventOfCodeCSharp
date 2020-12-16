using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DayTwo
{
    class Program
    {
        static readonly bool Testing = true;

        static List<string> answer = new List<String>();

        static void Main(string[] args)
        {
            Parse(GetDetails());
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

        static void Parse(string input)
        {
            foreach(var i in input.Trim().Split('\n'))
            {
                answer.Append(i.Trim());
            }
        }

        static string GetDetails()
        {
            //code with known good solutions.
            if (Testing)
                return @"";


            //boilerplate grab info
            var uri = "";
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
    }  
}
