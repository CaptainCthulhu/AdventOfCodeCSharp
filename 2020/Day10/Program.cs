using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace DayTen
{
    class Program
    {
        static List<int> UnorderedItems;
        static List<int> OrderedItems;

        static void Main()
        {
            //now do real work
            Parse(GetDetails());            
            QuestionOne();
            QuestionTwo();
            Console.WriteLine("Done");
        }

        static void QuestionOne()
        {
            var elements = new Dictionary<int, int>();

            elements[1] = 1;
            elements[3] = 1;

            foreach (var i in Enumerable.Range(0, OrderedItems.Count - 1))
            {
                int value = OrderedItems[i + 1] - OrderedItems[i];
                    elements[value] += 1;
            }

                Console.WriteLine($"Question One:{elements[1] * elements[3]}");
        }

        static void QuestionTwo()
        {
            var listConnectionValues = new Dictionary<int, ulong>();

            foreach (var i in Enumerable.Range(0, OrderedItems.Count))
            {
                var value = OrderedItems[i];
                listConnectionValues[value] = 0;
                if (value <= 3)
                    listConnectionValues[value] += 1;
                for (int x = 0; x < i; x++)
                {
                    var xValue = OrderedItems[x];
                    if (value - xValue <=3)
                        listConnectionValues[value] += listConnectionValues[xValue];
                }
            }

            Console.WriteLine($"Question Two:{listConnectionValues[OrderedItems.Max()]}");
        }

        static void Parse(string input)
        {
            UnorderedItems = new List<int>();
            var lines = input.Trim().Split("\n").ToList();
            foreach(var s in lines)
            {
                UnorderedItems.Add(Convert.ToInt32(s.Trim()));
            }

            OrderedItems = UnorderedItems.Select(x => x).OrderBy(x => x).ToList();
        }

        static string GetDetails()
        {
            //boilerplate grab info
            var uri = "https://adventofcode.com/2020/day/10/input";
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
