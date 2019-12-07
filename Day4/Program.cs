using System;
using System.Linq;
using System.Collections.Generic;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            QuestionOne();
            QuestionTwo();
        }


        static bool Happy(int i)
        {
            bool result = true;
            bool doubleFound = false;
            List<int> numerals = i.ToString().ToCharArray().ToList().Select(s => int.Parse(s.ToString())).ToList();

            var first = numerals[0];
            var second = 0;
            for(int x = 1; x < numerals.Count; x++)
            {
                second = numerals[x];
                if (first == second)
                    doubleFound = true;
                else if (first > second)
                    return false;
                first = second;
            }


            return doubleFound;
        }

        static bool HappyQ2(int i)
        {
            bool result = true;
            bool doubleFound = false;
            List<int> numerals = i.ToString().ToCharArray().ToList().Select(s => int.Parse(s.ToString())).ToList();

            var first = numerals[0];
            var second = 0;
            for (int x = 1; x < numerals.Count; x++)
            {
                second = numerals[x];
                if (first == second)
                {
                    if ((x - 2 < 0 || numerals[x - 2] != second) && (x + 1 >= numerals.Count || numerals[x + 1] != second))
                    {
                        doubleFound = true;
                    }
                }
                else if (first > second)
                    return false;
                first = second;
            }


            return doubleFound;
        }

        static void QuestionOne()
        {
            List<int> possible = new List<int>();

            for(int i = 254032; i < 789860;  i++)
            {
                if (Happy(i))
                    possible.Add(i);
            }
            Console.WriteLine("Question One: " + possible.Count);
        }

        static void QuestionTwo()
        {
            List<int> possible = new List<int>();

            for (int i = 254032; i < 789860; i++)
            {
                if (HappyQ2(i))
                    possible.Add(i);
            }
            Console.WriteLine("Question Two: " + possible.Count);
        }
    }
}
