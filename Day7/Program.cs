using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    class Program
    {
        static int[] test = new int[] { 3, 26, 1001, 26, -4, 26, 3, 27, 1002, 27, 2, 27, 1, 27, 26, 27, 4, 27, 1001, 28, -1, 28, 1005, 28, 6, 99, 0, 0, 5 };
        static int[] opCodeTemplate = new int[] { 3, 8, 1001, 8, 10, 8, 105, 1, 0, 0, 21, 46, 63, 76, 97, 118, 199, 280, 361, 442, 99999, 3, 9, 102, 4, 9, 9, 101, 2, 9, 9, 1002, 9, 5, 9, 101, 4, 9, 9, 102, 2, 9, 9, 4, 9, 99, 3, 9, 101, 5, 9, 9, 102, 3, 9, 9, 101, 3, 9, 9, 4, 9, 99, 3, 9, 1001, 9, 2, 9, 102, 3, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 5, 9, 101, 4, 9, 9, 1002, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 5, 9, 101, 3, 9, 9, 1002, 9, 5, 9, 1001, 9, 5, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99 };

        static void Main(string[] args)
        {
            QuestionOne();
            QuestionTwo();
        }

        struct Instruction
        {
            public int Operation;
            public int FirstParameter;
            public int SecondParameter;
            public int ThirdParameter;
        }

        static Instruction DetermineOperation(int value)
        {
            List<int> numerals = value.ToString("D5").ToCharArray().ToList().Select(s => int.Parse(s.ToString())).Reverse().ToList();

            return new Instruction
            {
                Operation = numerals[0] + numerals[1] * 10,
                FirstParameter = numerals[2],
                SecondParameter = numerals[3],
                ThirdParameter = numerals[4]
            };
        }

        static int MachineOne(int[] opcodes, int firstNumber, int secondNumber)
        {
            var index = 0;
            var answer = 0;
            var input = firstNumber;

            while (true)
            {
                index = index % opcodes.Length;
                Instruction instruction = DetermineOperation(opcodes[index]);

                if (instruction.Operation == 1)
                {
                    opcodes[instruction.ThirdParameter == 0 ? opcodes[index + 3] : index + 3] =
                        (instruction.FirstParameter == 0 ? opcodes[opcodes[index + 1]] : opcodes[index + 1]) +
                        (instruction.SecondParameter == 0 ? opcodes[opcodes[index + 2]] : opcodes[index + 2]);
                    index += 4;
                }
                else if (instruction.Operation == 2)
                {

                    opcodes[instruction.ThirdParameter == 0 ? opcodes[index + 3] : index + 3] =
                    (instruction.FirstParameter == 0 ? opcodes[opcodes[index + 1]] : opcodes[index + 1]) *
                        (instruction.SecondParameter == 0 ? opcodes[opcodes[index + 2]] : opcodes[index + 2]);
                    index += 4;
                }
                else if (instruction.Operation == 3)
                {
                    opcodes[instruction.FirstParameter == 0 ? opcodes[index + 1] : index + 1] = input;
                    input = secondNumber;
                    index += 2;
                }
                else if (instruction.Operation == 4)
                {
                    var val = instruction.FirstParameter == 0 ? opcodes[opcodes[index + 1]] : opcodes[index + 1];
                    if (val != 0)
                        answer = val;
                    index += 2;
                }
                else if (instruction.Operation == 5)
                {
                    var firstParamterPointer = instruction.FirstParameter == 0 ? opcodes[index + 1] : (index + 1);
                    if (opcodes[firstParamterPointer] != 0)
                        index = (instruction.SecondParameter == 0 ? opcodes[opcodes[index + 2]] : opcodes[index + 2]);
                    else
                        index += 3;
                }
                else if (instruction.Operation == 6)
                {
                    var firstParameterPointer = instruction.FirstParameter == 0 ? opcodes[index + 1] : (index + 1);
                    if (opcodes[firstParameterPointer] == 0)
                        index = (instruction.SecondParameter == 0 ? opcodes[opcodes[index + 2]] : opcodes[index + 2]);
                    else
                        index += 3;
                }
                else if (instruction.Operation == 7)
                {
                    opcodes[instruction.ThirdParameter == 0 ? opcodes[index + 3] : index + 3] =
                                       ((instruction.FirstParameter == 0 ? opcodes[opcodes[index + 1]] : opcodes[index + 1]) <
                                           (instruction.SecondParameter == 0 ? opcodes[opcodes[index + 2]] : opcodes[index + 2])) ? 1 : 0;
                    index += 4;
                }
                else if (instruction.Operation == 8)
                {
                    opcodes[instruction.ThirdParameter == 0 ? opcodes[index + 3] : index + 3] =
                                       ((instruction.FirstParameter == 0 ? opcodes[opcodes[index + 1]] : opcodes[index + 1]) ==
                                           (instruction.SecondParameter == 0 ? opcodes[opcodes[index + 2]] : opcodes[index + 2])) ? 1 : 0;
                    index += 4;
                }
                else if (instruction.Operation == 99)
                {
                    return answer;
                }
                else
                {
                    Console.WriteLine($"Bad Opcode: {instruction.Operation}. Point Location: {index}.");
                    break;
                }
            }
            return 0;
        }        

        static void QuestionOne()
        {
            int[] opCodes = (int[])opCodeTemplate.Clone();

            int signal = 0;
            int phasesetting = 0;
            for (int a = 0; a < 5; a++)
            {
                for (int b = 0; b < 5; b++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        for (int d = 0; d < 5; d++)
                        {
                            for (int e = 0; e < 5; e++)
                            {
                                if (a != b & a != c && a != d && a != e
                                    && b != c && b != d && b != e
                                    && c != d && c != e
                                    && d != e)
                                {
                                    var answer = MachineOne((int[])opCodes.Clone(), a, 0);
                                    answer = MachineOne((int[])opCodes.Clone(), b, answer);
                                    answer = MachineOne((int[])opCodes.Clone(), c, answer);
                                    answer = MachineOne((int[])opCodes.Clone(), d, answer);
                                    answer = MachineOne((int[])opCodes.Clone(), e, answer);

                                    if (answer > signal)
                                    {
                                        phasesetting = e + d * 10 + c * 100 + b * 1000 + a * 10000;
                                        signal = answer;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Question One: " + signal);
        }

        static void QuestionTwo()
        {
            int[] opCodes = (int[])test.Clone();
            //need to keep the index too....

            int signal = 0;
            int phasesetting = 0;

            for (int a = 5; a < 10; a++)
            {
                for (int b = 5; b < 10; b++)
                {
                    for (int c = 5; c < 10; c++)
                    {
                        for (int d = 5; d < 10; d++)
                        {
                            for (int e = 5; e < 10; e++)
                            {
                                if (a != b & a != c && a != d && a != e
                                    && b != c && b != d && b != e
                                    && c != d && c != e
                                    && d != e)
                                {
                                    Machine machineA = new Machine(opCodes, a);
                                    Machine machineB = new Machine(opCodes, b);
                                    Machine machineC = new Machine(opCodes, c);
                                    Machine machineD = new Machine(opCodes, d);
                                    Machine machineE = new Machine(opCodes, e);

                                    do
                                    {
                                        machineA.Run(machineE.Output);
                                        machineB.Run(machineA.Output);
                                        machineC.Run(machineB.Output);
                                        machineD.Run(machineC.Output);
                                        machineE.Run(machineD.Output);
                                    } while (machineE.Code != 99);

                                    if (machineE.Output > signal)
                                    {
                                        phasesetting = e + d * 10 + c * 100 + b * 1000 + a * 10000;
                                        signal = machineE.Output;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Question Two: " + signal);
        }
    }
}
