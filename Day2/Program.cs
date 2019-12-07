using System;

namespace Day2
{
    class Program
    {

        static int[] opcodes = new int[] { 1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 1, 6, 19, 1, 9, 19, 23, 2, 23, 10, 27, 1, 27, 5, 31, 1, 31, 6, 35, 1, 6, 35, 39, 2, 39, 13, 43, 1, 9, 43, 47, 2, 9, 47, 51, 1, 51, 6, 55, 2, 55, 10, 59, 1, 59, 5, 63, 2, 10, 63, 67, 2, 9, 67, 71, 1, 71, 5, 75, 2, 10, 75, 79, 1, 79, 6, 83, 2, 10, 83, 87, 1, 5, 87, 91, 2, 9, 91, 95, 1, 95, 5, 99, 1, 99, 2, 103, 1, 103, 13, 0, 99, 2, 14, 0, 0 };

        static int[] opcodesQ1 = new int[] { 1, 12, 2, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 1, 6, 19, 1, 9, 19, 23, 2, 23, 10, 27, 1, 27, 5, 31, 1, 31, 6, 35, 1, 6, 35, 39, 2, 39, 13, 43, 1, 9, 43, 47, 2, 9, 47, 51, 1, 51, 6, 55, 2, 55, 10, 59, 1, 59, 5, 63, 2, 10, 63, 67, 2, 9, 67, 71, 1, 71, 5, 75, 2, 10, 75, 79, 1, 79, 6, 83, 2, 10, 83, 87, 1, 5, 87, 91, 2, 9, 91, 95, 1, 95, 5, 99, 1, 99, 2, 103, 1, 103, 13, 0, 99, 2, 14, 0, 0 };
        static int[] opcodesQ2 = new int[] { 1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 1, 6, 19, 1, 9, 19, 23, 2, 23, 10, 27, 1, 27, 5, 31, 1, 31, 6, 35, 1, 6, 35, 39, 2, 39, 13, 43, 1, 9, 43, 47, 2, 9, 47, 51, 1, 51, 6, 55, 2, 55, 10, 59, 1, 59, 5, 63, 2, 10, 63, 67, 2, 9, 67, 71, 1, 71, 5, 75, 2, 10, 75, 79, 1, 79, 6, 83, 2, 10, 83, 87, 1, 5, 87, 91, 2, 9, 91, 95, 1, 95, 5, 99, 1, 99, 2, 103, 1, 103, 13, 0, 99, 2, 14, 0, 0 };
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            QuestionOne();
            QuestionTwo();
        }

        static void QuestionOne()
        {
            var i = 0;
            while (true)
            {
                int workingVal = i % opcodesQ1.Length;
                if (opcodesQ1[workingVal] == 1)
                {
                    opcodesQ1[opcodesQ1[workingVal + 3]] = opcodesQ1[opcodesQ1[workingVal + 1]] + opcodesQ1[opcodesQ1[workingVal + 2]];
                }
                else if (opcodesQ1[workingVal] == 2)
                {
                    opcodesQ1[opcodesQ1[workingVal + 3]] = opcodesQ1[opcodesQ1[workingVal + 1]] * opcodesQ1[opcodesQ1[workingVal + 2]];
                }
                else if (opcodesQ1[workingVal] == 99)
                    break;

                i += 4;
            }

            Console.WriteLine("Question One: " + opcodesQ1[0]);
        }


        static int Run(int noun, int verb)
        {
            var i = 0;
            var workingOpcodes = (int[])opcodes.Clone();
            workingOpcodes[1] = noun;
            workingOpcodes[2] = verb;

            while (true)
            {
                int workingVal = i % opcodesQ1.Length;
                if (workingOpcodes[workingVal] == 1)
                {
                    workingOpcodes[workingOpcodes[workingVal + 3]] = workingOpcodes[workingOpcodes[workingVal + 1]] + workingOpcodes[workingOpcodes[workingVal + 2]];
                }
                else if (opcodesQ1[workingVal] == 2)
                {
                    workingOpcodes[workingOpcodes[workingVal + 3]] = workingOpcodes[workingOpcodes[workingVal + 1]] * workingOpcodes[workingOpcodes[workingVal + 2]];
                }
                else if (workingOpcodes[workingVal] == 99)
                    return workingOpcodes[0];

                i += 4;

            }
        }

        static void QuestionTwo()
        {
            int goal = 19690720;


            var i = 0;
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    var result = Run(noun, verb);
                    if (result == goal)
                    {
                        Console.WriteLine("Question One: " + ((100 * noun) + verb));
                        return;
                    }

                }
            }



        }
    }
}



