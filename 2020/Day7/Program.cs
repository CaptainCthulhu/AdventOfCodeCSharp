using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DaySeven
{
    class Program
    {
        static Dictionary<String, List<String>> Bags = new Dictionary<string, List<string>>();

        static void Main(string[] args)
        {
            Parse(GetDetails());
            QuestionOne();
            QuestionTwo();
            Console.WriteLine("Done");
        }

        static void QuestionOne()
        {
            var start = "shiny gold bag";
            var foundTypes = new List<String>();
            FindContainer(start, foundTypes);
            Console.WriteLine($"Question One:{foundTypes.Count()}");
        }

        static void QuestionTwo()
        {            
            var start = "shiny gold bag";
            var foundTypes = new List<String>();
            DigDown(start, foundTypes);
            Console.WriteLine($"Question Two:{foundTypes.Count()}");
        }

        static void FindContainer(string input, List<String> FoundTypes)
        {
            foreach(var bagName in Bags.Keys)
            {
                if (bagName != input && Bags[bagName].Any(x => x.Contains(input)) && !FoundTypes.Contains(bagName))
                {
                    Console.WriteLine($"{bagName} will hold {input}");   
                    FoundTypes.Add(bagName);                  
                    FindContainer(bagName, FoundTypes);
                }
            }
        }

        static void DigDown(string input, List<String> Found)
        {
            if (Bags.ContainsKey(input))
            {
                foreach(var bagName in Bags[input])
                {
                    Found.Add(bagName);                  
                    DigDown(bagName, Found);
                
                }
            }   
        }

        static void Parse(string input)
        {
            var lines = input.Trim().Split('\n');
            foreach(var line in lines)
            {
                var elements = line.Split("contain");
                var name = elements[0].Trim();
                name = name.EndsWith("s") ? name.Remove(name.Length - 1 ) : name;
                var holdingnames = new List<string>();
                var containedItems = elements[1].Trim().Split(",");
                foreach(var item in containedItems)
                {
                    var trimmedItem = item.Trim();
                    if (trimmedItem != "no other bags.")
                    {
                        var number = Convert.ToInt32(trimmedItem[0].ToString());
                        var description = trimmedItem.Remove(0,1).Trim().Replace(".", "");
                        description = description.EndsWith("s") ? description.Remove(description.Length - 1 ) : description;
                        for(int i = 0; i < number; i++)
                        {                            
                            holdingnames.Add(description);
                        }
                    }
                }

                if (Bags.ContainsKey(name))
                    Bags[name].AddRange(holdingnames);
                else
                    Bags[name] = holdingnames;
            }
        }

        static string GetDetails()
        {   
               //boilerplate grab info
            var uri = "https://adventofcode.com/2020/day/7/input";
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
