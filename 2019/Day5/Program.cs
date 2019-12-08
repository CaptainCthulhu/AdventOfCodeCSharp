using System;
using System.Collections.Generic;
using System.Linq;

namespace Day5
{
    class Program
    {
        static int[] opcodesTemplate = new int[] { 3, 225, 1, 225, 6, 6, 1100, 1, 238, 225, 104, 0, 1102, 31, 68, 225, 1001, 13, 87, 224, 1001, 224, -118, 224, 4, 224, 102, 8, 223, 223, 1001, 224, 7, 224, 1, 223, 224, 223, 1, 174, 110, 224, 1001, 224, -46, 224, 4, 224, 102, 8, 223, 223, 101, 2, 224, 224, 1, 223, 224, 223, 1101, 13, 60, 224, 101, -73, 224, 224, 4, 224, 102, 8, 223, 223, 101, 6, 224, 224, 1, 224, 223, 223, 1101, 87, 72, 225, 101, 47, 84, 224, 101, -119, 224, 224, 4, 224, 1002, 223, 8, 223, 1001, 224, 6, 224, 1, 223, 224, 223, 1101, 76, 31, 225, 1102, 60, 43, 225, 1102, 45, 31, 225, 1102, 63, 9, 225, 2, 170, 122, 224, 1001, 224, -486, 224, 4, 224, 102, 8, 223, 223, 101, 2, 224, 224, 1, 223, 224, 223, 1102, 29, 17, 224, 101, -493, 224, 224, 4, 224, 102, 8, 223, 223, 101, 1, 224, 224, 1, 223, 224, 223, 1102, 52, 54, 225, 1102, 27, 15, 225, 102, 26, 113, 224, 1001, 224, -1560, 224, 4, 224, 102, 8, 223, 223, 101, 7, 224, 224, 1, 223, 224, 223, 1002, 117, 81, 224, 101, -3645, 224, 224, 4, 224, 1002, 223, 8, 223, 101, 6, 224, 224, 1, 223, 224, 223, 4, 223, 99, 0, 0, 0, 677, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1105, 0, 99999, 1105, 227, 247, 1105, 1, 99999, 1005, 227, 99999, 1005, 0, 256, 1105, 1, 99999, 1106, 227, 99999, 1106, 0, 265, 1105, 1, 99999, 1006, 0, 99999, 1006, 227, 274, 1105, 1, 99999, 1105, 1, 280, 1105, 1, 99999, 1, 225, 225, 225, 1101, 294, 0, 0, 105, 1, 0, 1105, 1, 99999, 1106, 0, 300, 1105, 1, 99999, 1, 225, 225, 225, 1101, 314, 0, 0, 106, 0, 0, 1105, 1, 99999, 8, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 329, 1001, 223, 1, 223, 1108, 677, 226, 224, 102, 2, 223, 223, 1006, 224, 344, 101, 1, 223, 223, 108, 677, 226, 224, 102, 2, 223, 223, 1006, 224, 359, 101, 1, 223, 223, 7, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 374, 101, 1, 223, 223, 1007, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 389, 101, 1, 223, 223, 8, 677, 677, 224, 102, 2, 223, 223, 1006, 224, 404, 1001, 223, 1, 223, 1007, 677, 677, 224, 1002, 223, 2, 223, 1006, 224, 419, 101, 1, 223, 223, 1108, 677, 677, 224, 1002, 223, 2, 223, 1005, 224, 434, 1001, 223, 1, 223, 1107, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 449, 101, 1, 223, 223, 107, 226, 226, 224, 102, 2, 223, 223, 1006, 224, 464, 101, 1, 223, 223, 1108, 226, 677, 224, 1002, 223, 2, 223, 1005, 224, 479, 1001, 223, 1, 223, 7, 677, 677, 224, 102, 2, 223, 223, 1006, 224, 494, 1001, 223, 1, 223, 1107, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 509, 101, 1, 223, 223, 107, 677, 677, 224, 1002, 223, 2, 223, 1006, 224, 524, 101, 1, 223, 223, 1008, 677, 677, 224, 1002, 223, 2, 223, 1006, 224, 539, 101, 1, 223, 223, 7, 226, 677, 224, 1002, 223, 2, 223, 1005, 224, 554, 101, 1, 223, 223, 108, 226, 226, 224, 1002, 223, 2, 223, 1006, 224, 569, 101, 1, 223, 223, 1008, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 584, 101, 1, 223, 223, 8, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 599, 101, 1, 223, 223, 1007, 226, 226, 224, 1002, 223, 2, 223, 1005, 224, 614, 101, 1, 223, 223, 1107, 226, 226, 224, 1002, 223, 2, 223, 1006, 224, 629, 101, 1, 223, 223, 107, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 644, 1001, 223, 1, 223, 1008, 226, 226, 224, 1002, 223, 2, 223, 1006, 224, 659, 101, 1, 223, 223, 108, 677, 677, 224, 1002, 223, 2, 223, 1005, 224, 674, 1001, 223, 1, 223, 4, 223, 99, 226 };

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

        static void OpCodeMachine1(int[] opcodes)
        {
            var index = 0;
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

                    opcodes[instruction.ThirdParameter == 0 ? opcodes[index + 3] : index + 3]  = 
                    (instruction.FirstParameter == 0 ? opcodes[opcodes[index + 1]] : opcodes[index + 1]) *
                        (instruction.SecondParameter == 0 ? opcodes[opcodes[index + 2]] : opcodes[index + 2]);
                    index += 4;
                }
                else if (instruction.Operation == 3)
                {
                    opcodes[instruction.FirstParameter == 0 ? opcodes[index + 1] : index + 1] = 1;                       
                    index += 2;
                }
                else if (instruction.Operation == 4)
                {
                    var val = instruction.FirstParameter == 0 ? opcodes[opcodes[index + 1]] : opcodes[index + 1];
                    if (val != 0)
                        Console.WriteLine(val);
                    index += 2;
                }
                else if (instruction.Operation == 99)
                {
                    break;
                }
            }
        }

        static void OpCodeMachine2(int[] opcodes)
        {
            var index = 0;

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
                    opcodes[instruction.FirstParameter == 0 ? opcodes[index + 1] : index + 1] = 5;
                    index += 2;
                }
                else if (instruction.Operation == 4)
                {
                    var val = instruction.FirstParameter == 0 ? opcodes[opcodes[index + 1]] : opcodes[index + 1];
                    if (val != 0)
                        Console.WriteLine(val);
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
                    break;
                }
                else
                {
                    Console.WriteLine($"Bad Opcode: {instruction.Operation}. Point Location: {index}.");
                    break;
                }
            }
        }




        static void QuestionOne()
        {
            Console.Write("Question One: ");
            OpCodeMachine1((int[])opcodesTemplate.Clone());
        }

        static void QuestionTwo()
        {

            Console.Write("Question Two: ");           
            OpCodeMachine2((int[])opcodesTemplate.Clone());
        }

    }
}
