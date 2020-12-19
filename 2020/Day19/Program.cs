using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Day19
{
    class Program
    {
        static readonly bool Testing = false;
        public static bool Debug = false;
        static string Input;

        public static int MaxLength;
        public static int MinLength;

        public static Dictionary<int, Rule> Rules = new Dictionary<int, Rule>();
        static List<string> Messages = new List<string>();

        static void Main()
        {
            GetDetails();
            Parse();

            MinLength = Messages.Min(x => x.Length);
            MaxLength = Messages.Max(x => x.Length);

            QuestionOne();
            //QuestionTwo();
            Console.WriteLine("Done");
        }

        static void QuestionOne()
        {
            int answer = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var possible = new List<string>();
            Rules[0].RunRule(possible, new List<int> { 0 });
            Console.WriteLine($"Done Rule Fetching {stopwatch.Elapsed}");
            stopwatch.Restart();

            possible = possible.Where(x => x.Length >= MinLength && x.Length <= MaxLength).ToList();
            Console.WriteLine($"Done Rule Filter {stopwatch.Elapsed}");
            stopwatch.Restart();

            answer = Messages.Count(x => possible.Any(i => i.Equals(x)));            

            Console.WriteLine($"Question One:{answer}. Time {stopwatch.Elapsed}");
        }

        static void QuestionTwo()
        {
            int answer = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //Update the rules
            Rules[8] = new Rule("42 | 42 8");
            Rules[11] = new Rule("42 31 | 42 11 31");

            var possible = new List<string>();

            Rules[0].RunRule(possible, new List<int> { 0 });
            Console.WriteLine($"Done Rule Fetching {stopwatch.Elapsed}");
            stopwatch.Restart();

            possible = possible.Where(x => x.Length >= MinLength && x.Length <= MaxLength).ToList();
            Console.WriteLine($"Done Rule Filter {stopwatch.Elapsed}");
            stopwatch.Restart();

            foreach (var m in Messages)
            {
                Rule.Debug($"\n\nStarting {m}");
                var testString = new String(m.Select(x => x).ToArray());
                if (possible.Contains(m))
                {
                    answer++;
                }
            }

            Console.WriteLine($"Question Two:{answer}. Time {stopwatch.Elapsed}");
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
                input = @"42: 9 14 | 10 1
                            9: 14 27 | 1 26
                            10: 23 14 | 28 1
                            1: a
                            11: 42 31
                            5: 1 14 | 15 1
                            19: 14 1 | 14 14
                            12: 24 14 | 19 1
                            16: 15 1 | 14 14
                            31: 14 17 | 1 13
                            6: 14 14 | 1 14
                            2: 1 24 | 14 4
                            0: 8 11
                            13: 14 3 | 1 12
                            15: 1 | 14
                            17: 14 2 | 1 7
                            23: 25 1 | 22 14
                            28: 16 1
                            4: 1 1
                            20: 14 14 | 1 15
                            3: 5 14 | 16 1
                            27: 1 6 | 14 18
                            14: b
                            21: 14 1 | 1 14
                            25: 1 1 | 1 14
                            22: 14 14
                            8: 42
                            26: 14 22 | 1 20
                            18: 15 15
                            7: 14 5 | 1 21
                            24: 14 1

                            aaaabbaaaabbaaa
                            baabbaaaabbaaaababbaababb";

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

        public void RunRule(List<string> possible, List<int> instructionsRun)
        {
            if (instructionsRun.Where(x => x == 8 || x == 42).Count() > 3)
            {
                Console.WriteLine("Too deep! Abort!");
                return;
            }

            List<string> rulesOptionsOne = possible.Select(x => (string)x.Clone()).ToList();
            List<string> rulesOptionsTwo = possible.Select(x => (string)x.Clone()).ToList();

            if (Character.HasValue)
            {
                if (possible.Count == 0)
                    possible.Add(Character.ToString());
                else
                {
                    foreach (var i in Enumerable.Range(0, possible.Count))
                    {
                        possible[i] = possible[i] + Character.ToString();
                    }
                }
            }

            if (RuleOptionOne != null)
            {
                foreach (var i in RuleOptionOne)
                {
                    instructionsRun.Add(i);
                    Program.Rules[i].RunRule(rulesOptionsOne, instructionsRun);
                    instructionsRun.RemoveAt(instructionsRun.Count - 1);
                }

                possible.AddRange(rulesOptionsOne);
            }

            if (RuleOptionTwo != null)
            {
                foreach (var i in RuleOptionTwo)
                {
                    instructionsRun.Add(i);
                    Program.Rules[i].RunRule(rulesOptionsTwo, instructionsRun);
                    instructionsRun.RemoveAt(instructionsRun.Count - 1);
                }

                possible.AddRange(rulesOptionsTwo);
            }

            rulesOptionsOne.AddRange(rulesOptionsTwo);
            possible = rulesOptionsOne;
        }

        public static void Debug(string s)
        {
            if (Program.Debug)
                Console.WriteLine(s);
        }

    }
}
