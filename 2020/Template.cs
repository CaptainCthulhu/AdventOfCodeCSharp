using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DayTwo
{
    class Program
    {
        static void Main(string[] args)
        {
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
            var uri = "";
            var sessionId = ""

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
