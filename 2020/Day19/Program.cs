using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Day19
{
    class Program
    {
        static readonly bool Testing = false;
        public static bool Debug = false;
        static string Input;

        public static Dictionary<int, Rule> Rules = new Dictionary<int, Rule>();
        static List<string> Messages = new List<string>();

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
            int answer = 0;
            foreach (var m in Messages)
            {
                var testString = new String(m.Select(x => x).ToArray());
                testString = Rules[0].RunRule(testString, 0, new List<int>());
                if (testString.Length == 0)
                    answer++;
            }

            Console.WriteLine($"Question One:{answer}");
        }

        static void QuestionTwo()
        {
            int answer = 0;
            Debug = false;

            //Update the rules
            Rules[8] = new Rule("42 | 42 8");
            Rules[11] = new Rule("42 31 | 42 11 31");

            foreach (var m in Messages)
            {
                Rule.Debug($"\n\nStarting {m}");
                var testString = new String(m.Select(x => x).ToArray());

                testString = Rules[0].RunRule(testString, 0, new List<int>());                

                if (testString.Length == 0)
                {
                    answer++;
                }
            }

            Console.WriteLine($"Question Two:{answer}");
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

        public string RunRule(string input, int ruleNum, List<int> ruleList)
        {
            ruleList.Add(ruleNum);
            Debug(String.Join(", ", ruleList));

            if (Character.HasValue)
            {
                var result = input;
                if (input.First() == Character)
                {
                    result = input.Remove(0, 1);
                    Debug($"Removing '{input.First()}' from '{input}' result '{result}'");
                }
                return result;
            }
            else
            {
                var ruleOptionOneString = new String(input.Select(x => x).ToArray());
                var ruleOptionTwoString = new String(input.Select(x => x).ToArray());

                if (RuleOptionOne != null)
                    ruleOptionOneString = ProcessRule(RuleOptionOne, ruleOptionOneString, ruleNum, ruleList, "One");

                if (RuleOptionTwo != null)
                    ruleOptionTwoString = ProcessRule(RuleOptionTwo, ruleOptionTwoString, ruleNum, ruleList, "Two");


                Debug($"Rule {ruleNum} Options '{ruleOptionOneString}' or '{ruleOptionTwoString}'");

                if (ruleOptionOneString.Length < ruleOptionTwoString.Length)
                {
                    Debug($"Choosing option one '{ruleOptionOneString}'");
                    return ruleOptionOneString;
                }
                else
                {
                    Debug($"Choosing option two '{ruleOptionTwoString}'");
                    return ruleOptionTwoString;
                }

            }
        }

        private static string ProcessRule(List<int> rulesToProcess, string inputString, int ruleNumberCalled, List<int> rulesRun, string ruleOptionOneOrTwo)
        {
            var modifiedString = new String(inputString.Select(x => x).ToArray());

            foreach (int i in rulesToProcess)
            {
                Debug($"Rule {ruleNumberCalled.ToString().PadLeft(4)} Option {ruleOptionOneOrTwo}. Run Rule {i.ToString().PadLeft(4)} {modifiedString.PadLeft(25)}");
                modifiedString = Program.Rules[i].RunRule(modifiedString, i, rulesRun);
                rulesRun.RemoveAt(rulesRun.Count - 1);
                if (modifiedString.Length == inputString.Length)
                {
                    Debug($"Rule {i.ToString().PadLeft(4)} did nothing. Breaking.");
                    return modifiedString;
                }
            }

            return modifiedString;
        }

        public static void Debug(string s)
        {
            if (Program.Debug)
                Console.WriteLine(s);
        }

    }
}
