using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Day16
{
    class Program
    {
        static readonly bool Testing = false;

        static Dictionary<string, List<int>> Rules;
        static List<int> MyTicket;

        static List<List<int>> OtherTickets;

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
            List<int> invalidNumbers = new List<int>();

            var validValues = Rules.Values.SelectMany(x => x.Select(y => y));
            invalidNumbers.AddRange(MyTicket.Where(x => !validValues.Contains(x)));
            invalidNumbers.AddRange(OtherTickets.SelectMany(y => y.Where(x => !validValues.Contains(x))));            

            Console.WriteLine($"Question One:{invalidNumbers.Sum()}");
        }        

        static void QuestionTwo() 
        {
            var validValues = Rules.Values.SelectMany(x => x.Select(y => y));
            var validTickets = OtherTickets.Where(y => y.All(x => validValues.Contains(x))).ToList();
            validTickets.Add(MyTicket);

            var PossibilityMatrix = new List<List<string>>();

            foreach(var i in Enumerable.Range(0, MyTicket.Count))
            {
                PossibilityMatrix.Add(Rules.Keys.Select(x => (string)x.Clone()).ToList());
            }


            while (PossibilityMatrix.Any(x => x.Count > 1))
            {
                foreach (var index in Enumerable.Range(0, PossibilityMatrix.Count))
                {
                    foreach (var row in validTickets)
                    {
                        foreach (var rule in Rules)
                        {
                            if (!rule.Value.Contains(row[index]))
                            {
                                PossibilityMatrix[index].Remove(rule.Key);
                            }

                        }
                    }

                    if (PossibilityMatrix[index].Count == 1)
                    {
                        var value = PossibilityMatrix[index].First();
                        foreach (var i in Enumerable.Range(0, PossibilityMatrix.Count))
                        {
                            if (i != index)
                            {
                                PossibilityMatrix[i] = PossibilityMatrix[i].Where(x => x != value).ToList();
                            }
                        }
                    }
                }
            }

            long answer = 1;

            foreach (var index in Enumerable.Range(0, PossibilityMatrix.Count))
            {
                if (PossibilityMatrix[index].First().ToLower().Contains("departure"))
                    answer *= MyTicket[index];
            }

            Console.WriteLine($"Question Two:{answer}");
        }

        static void Parse(string input)
        {
            Rules = new Dictionary<string, List<int>>();
            MyTicket = new List<int>();
            OtherTickets = new List<List<int>>();

            var sections = input.Trim().Split("\n\n");

            //Parse the Rules
            foreach (var i in sections[0].Trim().Split('\n'))
            {                
                if (!String.IsNullOrEmpty(i))
                {
                    var elements = i.Trim().Split(':');
                    var label = elements[0];
                    var ranges = elements[1].Trim().Split(" or ");
                    var possibleRanges = new List<int>();

                    foreach (var range in ranges)
                    {
                        var highLow = range.Split("-");
                        var newRange = Enumerable.Range(Convert.ToInt32(highLow[0]), Convert.ToInt32(highLow[1]) + 1 - Convert.ToInt32(highLow[0]));
                        possibleRanges.AddRange(newRange);
                    }

                    Rules[label] = possibleRanges;
                }
            }

            //Get My List
            foreach (var i in sections[1].Split('\n'))
            {
                if (i.Trim() != "your ticket:")
                {
                    MyTicket = i.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                }
            }

            //Get Everyone else's
            foreach (var i in sections[2].Split('\n'))
            {
                if (i.Trim() != "nearby tickets:")
                {
                    OtherTickets.Add(i.Split(',').Select(x => Convert.ToInt32(x)).ToList());
                }
            }
        }

        static string GetDetails()
        {
            //code with known good solutions.
            if (Testing)
                return @"class: 1-3 or 5-7
                    row: 6-11 or 33-44
                    seat: 13-40 or 45-50

                    your ticket:
                    7,1,14

                    nearby tickets:
                    7,3,47
                    40,4,50
                    55,2,20
                    38,6,12".Replace("\r", "");

            //boilerplate grab info
            var uri = "https://adventofcode.com/2020/day/16/input";
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
