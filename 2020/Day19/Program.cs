using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Day19
{
    class Program
    {
        static readonly bool Testing = false;
        public static bool Debug = false;
        static string Input;

        public static Dictionary<int, Rule> Rules = new Dictionary<int, Rule>();
        public static List<string> Messages = new List<string>();

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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int answer = 0;

            var possible = new List<string>();
            var rule = Rules[0].ParseRegex();
            var regex = new Regex(rule);
            answer = Messages.Count(x => regex.Match(x).Success);
            Console.WriteLine($"Question One:{answer}.");
        }

        static void QuestionTwo()
        {           
            //Update the rules
            Rules[8] = new Rule("42 | 42 8");
            Rules[11] = new Rule("42 31 | 42 11 31");

            int answer = 0;

            var possible = new List<string>();
            var rule = Rules[0].ParseRegex();
            var regex = new Regex(rule);
            answer = Messages.Count(x => regex.Match(x).Success); 
            Console.WriteLine($"Question Two:{answer}.");
        }

        static void Parse()
        {
            foreach (var i in Input.Replace("\"", "").Replace("\r", "").Split('\n'))
            {
                if (!String.IsNullOrWhiteSpace(i))
                {
                    if (i.Contains(':'))
                    {
                        var elements = i.Split(':').ToList().Select(x => x.Trim());
                        int key = Convert.ToInt32(elements.First());
                        Rules[key] = new Rule(elements.Last());
                    }
                    else
                    {
                        Messages.Add(i.Trim());
                    }

                }
            }
        }

        static void GetDetails()
        {
            string input;
            //code with known good solutions.
            if (Testing)
                input = @"0: 4 1 5
1: 2 3 | 3 2
2: 4 4 | 5 5
3: 4 5 | 5 4
4: a
5: b

ababbb
bababa
abbbab
aaabbb
aaaabbb";

            else
            {
                if (!File.Exists("input.txt"))
                {
                    //boilerplate grab info
                    var uri = "https://adventofcode.com/2020/day/19/input";
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

            Input = input.Trim();
        }
    }

    public class Rule
    {

        List<int> RuleOptionOne;
        List<int> RuleOptionTwo;
        public char? Character;

        public Rule(string ruleString)
        {
            ruleString = ruleString.Trim();
            if (ruleString.Length == 1)
            {
                Character = Convert.ToChar(ruleString);
            }
            else
            {
                var rules = ruleString.Split('|').ToList().Select(x => x.Trim());
                RuleOptionOne = rules.First().Split(' ').ToList().Select(x => Convert.ToInt32(x.Trim())).ToList();
                if (rules.Count() > 1)
                    RuleOptionTwo = rules.Last().Split(' ').ToList().Select(x => Convert.ToInt32(x.Trim())).ToList();
            }
        }

        public string ParseRegex()
        {
            var parsedRegex = "^";            
            parsedRegex += this.RecursiveParseRegex(new List<int> {0});
            parsedRegex += "$";
            return parsedRegex;
        }

        public string RecursiveParseRegex(List<int> rulesRun)
        {            
            if (rulesRun.Count(x => x == 11 || x == 8) > 5)
                return "";


            var parsedPiece = "(";
            if (Character.HasValue)
            {
                return Character.ToString();
            }
            else
            {
                if (RuleOptionOne != null)
                {
                    foreach (int number in RuleOptionOne)
                    {
                        rulesRun.Add(number);
                        parsedPiece += Program.Rules[number].RecursiveParseRegex(rulesRun);
                        rulesRun.RemoveRange(rulesRun.Count - 1, 1);
                    }
                }
                if (RuleOptionTwo != null)
                {
                    parsedPiece += "|";
                    foreach (int number in RuleOptionTwo)
                    {
                        rulesRun.Add(number);
                        parsedPiece += Program.Rules[number].RecursiveParseRegex(rulesRun);
                        rulesRun.RemoveRange(rulesRun.Count - 1, 1);
                    }
                }
            }
            parsedPiece += ")";
            return parsedPiece;

        }


        public static void Debug(string s)
        {
            if (Program.Debug)
                Console.WriteLine(s);
        }

    }
}
