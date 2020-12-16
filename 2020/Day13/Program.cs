using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace Day13
{
    class Program
    {
        static string Input;
        static readonly bool Testing = false;
        static int MyArrivalTime = 0;
        static Dictionary<string, List<int>> BusTimes = new Dictionary<string, List<int>>();

        static List<ulong> q2Answers = new List<ulong>();

        private static readonly object ConsoleWriterLock = new object();

        static void Main(string[] args)
        {
            //now do real work
            Input = GetDetails();
            QuestionOne();
            QuestionTwo();
            Console.WriteLine("Done");
        }

        static void QuestionOne()
        {            
            Parse(Input);
            foreach(var key in BusTimes.Keys)
            {
                if (key != "x")
                {
                    var incrementer = Convert.ToInt32(key);
                    var count = 1;
                    var currentTime = 0;
                    while (currentTime < MyArrivalTime)
                    {
                        currentTime = incrementer * count;
                        BusTimes[key].Add(currentTime);
                        count++;
                    }
                }
            }

            var id = 0;
            var minutes = Int32.MaxValue;
            foreach (var key in BusTimes.Keys)
            {
                if (key != "x")
                {
                    var LastValue = BusTimes[key].Last();
                    if (LastValue < minutes)
                    {
                        id = Convert.ToInt32(key);
                        minutes = LastValue;
                    }
                }
            }

            Console.WriteLine($"Question One:{id * (minutes - MyArrivalTime)}");
        }

        static void QuestionTwo()
        {

            ulong prevNumber = 1;
            var taskList = new List<Task>();
            for (ulong count = 129_366_106_081; count < 2_069_857_697_283; count += 129_366_106_081)
            {
                var newclass = new Program();                
                Task newTask = Task.Factory.StartNew(() =>
                { 
                    newclass.FindValue(prevNumber, count);
                });

                Thread.Sleep(1000);
                prevNumber = count;

                taskList.Add(newTask);
        }

            while (taskList.Any(t => !t.IsCompleted)) { }

            Console.WriteLine($"Question Two: {q2Answers.Min()}");
        }
        public void FindValue(ulong min, ulong max) 
        {
            lock(ConsoleWriterLock)
            {
                Console.WriteLine($"Starting a thread {min}, {max}.");
            }

            var elements = ParseQ2(Input);

            var maxKey = elements.Max(x => x.Key);
            var maxKeyLong = Convert.ToUInt64(maxKey);

            var maxKeyOffset = Convert.ToUInt64(elements[maxKey]);
            var firstKeyLong = Convert.ToUInt64(elements.First().Key);


            for (ulong count = min; count < max; count++)
                {
                    var number = maxKeyLong * count;
                    var minValue = number - maxKeyOffset;
                    if (minValue % firstKeyLong == 0)
                    {

                        if (elements.All(x => (minValue + (ulong)x.Value) % (ulong)x.Key == 0))
                        {
                            lock(q2Answers)
                            {
                            Console.WriteLine($"Added {minValue}");
                            q2Answers.Add(minValue);
                            break;
                            }                            
                        }
                    }
                }
    }

        static void Parse(string input)
        {
            var items = input.Trim().Split('\n');
            MyArrivalTime = Convert.ToInt32(items[0].Trim());
            foreach(var i in items[1].Split(','))
            {
                var timer = i.Trim();
                BusTimes[timer] = new List<int>();
            }
        }

        static Dictionary<int, int> ParseQ2(string input)
        {

            var items = input.Trim().Split('\n');
            Dictionary<int, int> elements = new Dictionary<int, int>();
            var offset = 0;
            foreach (var i in items[1].Split(','))
            {
                if (i.Trim() == "x")
                    offset++;
                else
                {
                    elements[Convert.ToInt32(i.Trim())] = offset + elements.Keys.Count;                    
                }
            }

            return elements;
        }

        static string GetDetails()
        {
            if (Testing)
                return @"939
7,13,x,x,59,x,31,19";
            //boilerplate grab info
            var uri = "https://adventofcode.com/2020/day/13/input";
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
