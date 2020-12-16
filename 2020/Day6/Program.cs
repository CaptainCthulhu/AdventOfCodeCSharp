using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

//not 3388

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

        static void QuestionOne(List<Group> groups)
        {
            int answer = 0;

            foreach(Group g in groups)
            {
                answer += g.Questions.Keys.Count();
            }

            Console.WriteLine($"Question One:{answer}");
        }

        static void QuestionTwo(List<Group> groups)
        {
            int answer = 0;

            foreach(Group g in groups)
            {
                answer += g.Questions.Where(x => x.Value == g.Passengers.Count()).Select(x => x.Key).Count();
            }
            Console.WriteLine($"Question Two:{answer}");
        }

        static List<Group> Parse(string input)
        {
            var groups = new List<Group>();
            var groupLines = input.Split("\n\n");
            foreach(var group in groupLines)
            {
                groups.Add(new Group(group));
            }

            return groups;
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

    class Group{
        public List<string> Passengers;
        public Dictionary<char, int> Questions;

        public Group(string input){
            Passengers = new List<string>();
            Passengers = input.Trim().Split('\n').ToList();
            Passengers = Passengers.Select(x => new String(x.Trim().OrderBy(x => x).ToArray())).ToList();

            Questions = new Dictionary<char, int>();
            foreach(var p in Passengers)
            {
                foreach(char q in p)
                {
                    if (q != '\n')
                    {
                        if (Questions.ContainsKey(q))
                        {
                            Questions[q]++;
                        }
                        else
                        {
                            Questions[q] = 1;
                        }
                    }
                }
            }
        }
    }
}
