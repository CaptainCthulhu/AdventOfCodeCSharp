using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    class Program
    {
        static readonly bool Testing = true;

        static List<string> Answer = new List<String>();

        static void Main(string[] args)
        {
            Parse(GetDetails());
            QuestionOne();
            QuestionTwo();
            Console.WriteLine("Done");
        }

        static void QuestionOne()
        {
            var currentMask = "";
            Dictionary<long, long> lines = new Dictionary<long, long>();

            foreach (var line in Answer)
            {
                if (line.Contains("mask"))
                {
                    currentMask = ProcessMask(line);
                }
                else
                {
                    var result = ProcessLineQ1(line, currentMask);
                    lines[result.Key] = result.Value;
                }
            }

            //DoStuff
            Console.WriteLine($"Question One:{lines.Values.Sum()}");
        }

        static void QuestionTwo()
        {
            var currentMask = "";
            Dictionary<long, long> lines = new Dictionary<long, long>();

            foreach (var line in Answer)
            {
                if (line.Contains("mask"))
                {
                    currentMask = ProcessMask(line);
                }
                else
                {
                    var results = ProcessLineQ2(line, currentMask);
                    foreach (var i in results)                    
                        lines[i.Key] = i.Value;                    
                }
            }

            //DoStuff
            Console.WriteLine($"Question Two:{lines.Values.Sum()}");
        }

        static string ProcessMask(string line)
        {
            return line.Split("=")[1].Trim().ToUpper();
        }

        static KeyValuePair<long, long> ProcessLineQ1(string line, string mask)
        {
            var elements = line.Split("=");
            var register = Convert.ToInt64(elements[0].Replace("mem[", "").Replace("]", ""));
            var value = Convert.ToString(Convert.ToInt64(elements[1]), 2).PadLeft(36, '0');

            var workingString = "";
            for (int i = 0; i < mask.Length; i++)
            {
                workingString += mask[i] != 'X' ? mask[i] : value[i];
            }
            var answer = Convert.ToInt64(workingString, 2);


            return new KeyValuePair<long, long>(register, answer);
        }

        static List<KeyValuePair<long, long>> ProcessLineQ2(string line, string mask)
        {
            //NukeFromOrbit, rebuild
            var elements = line.Split("=");
            var register = Convert.ToInt64(elements[0].Replace("mem[", "").Replace("]", ""));
            var registerStr = Convert.ToString(Convert.ToInt64(register), 2).PadLeft(36, '0');
            var maskedAddress = "";

            for (int i = 0; i < mask.Length; i++)
            {
                maskedAddress += mask[i] == '0' ? registerStr[i] : mask[i];
            }

            List<string> maskList = new List<string>() { "" };

            for (int i = 0; i < maskedAddress.Length; i++)
            {
                if (maskedAddress[i] == 'X')
                {
                    var newList = maskList.Select(item => (string)item.Clone()).ToList();
                    for (int x = 0; x < maskList.Count; x++)                    
                        maskList[x] += "1";                    
                    for (int x = 0; x < newList.Count; x++)                    
                        newList[x] += "0";                    
                    maskList.AddRange(newList);
                }
                else
                {
                    for (int x = 0; x < maskList.Count; x++)                    
                        maskList[x] += maskedAddress[i];
                    
                }

            }            

            List<KeyValuePair<long, long>> returnElements = new List<KeyValuePair<long, long>>();
            var registerValue = Convert.ToInt64(elements[1].Trim());

            foreach (var i in maskList)            
                returnElements.Add(new KeyValuePair<long, long>(Convert.ToInt64(i, 2), registerValue));
            

            return returnElements;
        }


        static void Parse(string input)
        {
            foreach(var i in input.Trim().Split('\n'))            
                Answer.Add(i.Trim());
            
        }

        static string GetDetails()
        {
            //code with known good solutions.
            if (Testing)
                return @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";


            //boilerplate grab info
            var uri = "https://adventofcode.com/2020/day/14/input";
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
