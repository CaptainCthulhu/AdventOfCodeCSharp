using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DayNine
{
    class Program
    {
        static List<long> items;
        static long questionOneAnswer = 0;

        static void Main(string[] args)
        {
            //now do real work
            Parse(GetDetails());
            QuestionOne();
            QuestionTwo();
            Console.WriteLine("Done");
        }

        static void QuestionOne()
        {
            for(int i = 25; i < items.Count; i++)
            {
                if (NotSum(i))
                {
                    questionOneAnswer = items[i];
                    break;
                }
            }
            Console.WriteLine($"Question One:{questionOneAnswer}");
        }

        static bool NotSum(int index)
        {
            var goal = items[index];
                for(int j = 0; j < index; j++)
                {
                    for(int k = j + 1; k < index; k++)
                    {
                        if (items[j] + items[k] == goal)
                            return false;
                    }
                }
            return true;
        }

        static void QuestionTwo()
        {
            long answer = 0;
            for (var length = 2; length < items.Count; length++)
            {
                for(var index = 0; index < items.Count - length; index++)
                {
                    if (items.GetRange(index, length).Sum() == questionOneAnswer)
                    {
                        answer = items.GetRange(index, length).Min() + items.GetRange(index, length).Max();
                    }
                }
            }

            Console.WriteLine($"Question Two:{answer}");
        }

        static void Parse(string input)
        {
            items = new List<long>();
            var lines = input.Trim().Split("\n").ToList();
            foreach(var s in lines)
            {
                items.Add(Convert.ToInt64(s.Trim()));
            }
        }

        static string GetDetails()
        {
            //boilerplate grab info
            var uri = "https://adventofcode.com/2020/day/9/input";
            var sessionId = "  ";

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
