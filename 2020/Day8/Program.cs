using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DayEight
{
    class Program
    {
        static List<Instruction> items = new List<Instruction>();

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
            Console.WriteLine($"Question One:{RunCode(items, true)}");
        }

        static void QuestionTwo()
        {
            int answer = 0;
            for(var i = 0; i < items.Count; i++)
            {
                bool changed = false;
                var newList = items.Select(x => x).ToList();
                if (newList[i].name == "nop")
                {
                    changed = true;
                    var item = newList[i];
                    item.name = "jmp";
                    newList[i] = item;
                }
                else if (newList[i].name == "jmp")
                {
                    changed = true;
                    var item = newList[i];
                    item.name = "nop";
                    newList[i] = item;
                }
                if (changed)
                {
                    var tempAnswer  = RunCode(newList, false);

                    if (tempAnswer != -1)
                    {
                        answer = tempAnswer;
                        Console.WriteLine($"Question Two:{answer}");
                        break;
                    }
                }

            }
        }

        static void Parse(string input)
        {
            var lines = input.Trim().Split("\n").ToList();
            foreach(var s in lines)
            {
                items.Add(new Instruction(s));
            }
        }

        static int RunCode(List<Instruction> instructions, bool returnOnLoop)
        {
            var accumulator = 0;
            var index = 0;
            List<int> instructionsRun = new List<int>();
            while (!instructionsRun.Contains(index) && index < instructions.Count)
            {
                instructionsRun.Add(index);
                var instruction = instructions[index];
                if (instruction.name == "acc")
                {
                    accumulator += instruction.number;
                    index++;
                }
                else if (instruction.name == "jmp")
                {
                    index += instruction.number;
                }
                else if (instruction.name == "nop")
                {
                    index++;
                }
            }

            if (index < items.Count && !returnOnLoop)
                return -1;
            return accumulator;
        }

        static string GetDetails()
        {
            //boilerplate grab info
            var uri = "https://adventofcode.com/2020/day/8/input";
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

        struct Instruction
        {
            public string name;
            public int number;

            public Instruction(string input)
            {
                var elements = input.Split(" ");
                name = elements[0].Trim();
                number = Convert.ToInt32(elements[1]);
            }
        }

    }
}
