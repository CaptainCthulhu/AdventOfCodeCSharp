using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Data;
using Microsoft.VisualBasic;
using System.Linq.Expressions;

namespace Day23
{
    class Program
    {
        static readonly bool Testing = false;
        static readonly bool Debug = false;
        static string Input;
        static LinkedList<int> Cups;
        static LinkedListNode<int> FirstCup;
        static Dictionary<int, LinkedListNode<int>> Indexes;

        static int MaxValue;

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
            Go(100);
            var current = Cups.Find(1);
            Console.WriteLine($"Question One:");
            WriteLinkedList(current);
            
        }

        static void QuestionTwo()
        {
            Parse();
            foreach (int i in Enumerable.Range(MaxValue + 1, 999_991))
            {
                var newItem = new LinkedListNode<int>(i);
                Cups.AddLast(newItem);
                Indexes[i] = newItem;
            }

            Go(10_000_000);
            var current = Cups.Find(1);
            Console.WriteLine($"Question Two: {(ulong)current.Next.Value * (ulong)current.Next.Next.Value}");
            
        }

        static public void Go(int moves)
        {           

            LinkedListNode<int> current = FirstCup;

            foreach (var turn in Enumerable.Range(0, moves))
            {
                List<LinkedListNode<int>> links = new List<LinkedListNode<int>>();

                foreach (var i in Enumerable.Range(0, 3))
                {                    
                    links.Add(current.NextOrFirst());
                    Cups.Remove(current.NextOrFirst());
                }
                
                links.Reverse();

                LinkedListNode<int> oneToGoTo = null;
                var currentValue = current.Value;

                while (oneToGoTo == null)
                {
                    currentValue--;
                    if (currentValue < 1)
                        currentValue = Cups.Count + 3;
                    if (!links.Select(x => x.Value).Contains(currentValue))                    
                        oneToGoTo = Indexes[currentValue];
                }                

                foreach (var element in links)
                {
                    Cups.AddAfter(oneToGoTo, element);
                }

                current = current.NextOrFirst();
            }
        }

        static public void Log(string s)
        {
            if (Debug || Testing)
            {
                Console.WriteLine(s);
            }
        }

        static public void WriteLinkedList(LinkedListNode<int> node)
        {
            var nextNode = node;
            string answer = "";
            do
            {
                answer += nextNode.Value;
                nextNode = nextNode.NextOrFirst();
            }
            while (nextNode != node);

            Console.WriteLine(answer);
        }
        


        static void Parse()
        {
            Indexes = new Dictionary<int, LinkedListNode<int>>();
            FirstCup = null;
            MaxValue = Int32.MinValue;
            Cups = new LinkedList<int>();
            LinkedListNode<int> newCup = null;
            foreach (var i in Input.Trim())
            {
                var value = Convert.ToInt32(i.ToString());

                if (value > MaxValue)
                {
                    MaxValue = value;
                }
                newCup= new LinkedListNode<int>(value);                
                if (FirstCup == null)
                {
                    FirstCup = newCup;
                    Cups.AddFirst(newCup);
                }
                else
                    Cups.AddLast(newCup);

                Indexes[value] = newCup;
            }
        }

        static void GetDetails()
        {

            string input;
            //code with known good solutions.
            if (Testing)
            {
                Input = @"389125467";
                return;
            }
            else
            {
                Input = @"872495136";
                return;
                if (!File.Exists("input.txt"))
                {
                    //boilerplate grab info
                    var uri = "https://adventofcode.com/2020/day/23/input";
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

            Input = input.Replace("\r", "").Trim();
        }
    }
}
