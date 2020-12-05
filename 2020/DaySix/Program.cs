using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DaySix
{
    class Program
    {
        static void Main(string[] args)
        {
            //now do real work
            var information = Parse(GetDetails());
            QuestionOne(information);
            QuestionTwo(information);            
            Console.WriteLine("Done");
        }       

        static void QuestionOne() 
        {
            int answer = 0;
            //DoStuff
            Console.Writeline($"Question One:{answer}");
        }

        static void QuestionTwo() 
        {
            int answer = 0;
           //DoStuff
            Console.Writeline($"Question Two:{answer}");
        }

        static object Parse(string input)
        {
            return new object();
        }

        static string GetDetails()
        {
            //boilerplate grab info
            var uri = "https://adventofcode.com/2020/day/6/input";
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
