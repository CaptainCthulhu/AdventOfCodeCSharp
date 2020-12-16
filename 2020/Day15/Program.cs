using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Day15
{
    class Program
    {
        static readonly bool Testing = false;
        static readonly List<int> numbers = new List<int>();

        static void Main(string[] args)
        {
            Parse(GetDetails());
            QuestionOne();
            QuestionTwo();
            Console.WriteLine("Done");
        }

        static void QuestionOne()
        {
            int goal = 2020;
            List<int> workingNumbers = new List<int>();
            workingNumbers.AddRange(numbers);

            while (workingNumbers.Count < goal)
            {
                var lastNumber = workingNumbers.Last();
                var lastIndex = workingNumbers.LastIndexOf(lastNumber);
                var secondLastIndex = workingNumbers.LastIndexOf(lastNumber, lastIndex - 1);

                if (lastIndex == -1 || secondLastIndex == -1)
                    workingNumbers.Add(0);
                else
                    workingNumbers.Add(lastIndex - secondLastIndex);
            }
            //DoStuff
            Console.WriteLine($"Question One:{workingNumbers.Last()}");
        }

        static void QuestionTwo()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            int goal = 30000000;
            Dictionary<int, Number> workingNumbers = new Dictionary<int, Number>();
            //Setup
            for (int i = 0; i < numbers.Count; i++)
            {
                var currentNumber = numbers[i];
                workingNumbers[currentNumber] = new Number(i);
            }
            var previousValue = numbers.Last();
            var count = numbers.Count;

            //Loop
            while (count < goal)
            {
                previousValue = FindValue(previousValue, workingNumbers, count);
                count++;
            }

            Console.WriteLine($"Question Two:{previousValue} {stopWatch.Elapsed}");
        }        

        private static int FindValue(int numberToFind, Dictionary<int, Number> workingNumbers, int count)
        {
            var returnValue = 0;

            if (workingNumbers[numberToFind].LastOccurenceIndex != -1 && workingNumbers[numberToFind].SecondLastOccurenceIndex != -1)            
                returnValue = workingNumbers[numberToFind].LastOccurenceIndex - workingNumbers[numberToFind].SecondLastOccurenceIndex;

            if (!workingNumbers.ContainsKey(returnValue))
            {
                workingNumbers[returnValue] = new Number(count);
            }
            else
            {
                var detail = workingNumbers[returnValue];
                detail.SecondLastOccurenceIndex = workingNumbers[returnValue].LastOccurenceIndex;
                detail.LastOccurenceIndex = count;
                workingNumbers[returnValue] = detail;
            }

            return returnValue;
        }

        static void Parse(string input)
        {
            foreach (var x in input.Trim().Split(','))
            {
                numbers.Add(Convert.ToInt32(x.Trim()));
            }
        }

        static string GetDetails()
        {
            //code with known good solutions.
            if (Testing)
                return @"0,3,6";
            else
                return "20,0,1,11,6,3";
        }

        private struct Number
        {
            public int LastOccurenceIndex { get; set; }
            public int SecondLastOccurenceIndex { get; set; }

            public Number(int lastOccurence)
            {
                LastOccurenceIndex = lastOccurence;
                SecondLastOccurenceIndex = -1;
            }
        }
    }
}
