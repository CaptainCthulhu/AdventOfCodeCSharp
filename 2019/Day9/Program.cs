using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day9
{
    class Program
    {
        static long[] opcodesTemplate = new long[] { 1102, 34463338, 34463338, 63, 1007, 63, 34463338, 63, 1005, 63, 53, 1102, 1, 3, 1000, 109, 988, 209, 12, 9, 1000, 209, 6, 209, 3, 203, 0, 1008, 1000, 1, 63, 1005, 63, 65, 1008, 1000, 2, 63, 1005, 63, 904, 1008, 1000, 0, 63, 1005, 63, 58, 4, 25, 104, 0, 99, 4, 0, 104, 0, 99, 4, 17, 104, 0, 99, 0, 0, 1101, 0, 493, 1024, 1102, 1, 38, 1015, 1101, 20, 0, 1011, 1101, 0, 509, 1026, 1101, 0, 32, 1018, 1101, 0, 333, 1022, 1102, 1, 0, 1020, 1101, 326, 0, 1023, 1101, 0, 33, 1010, 1101, 21, 0, 1016, 1101, 25, 0, 1004, 1102, 28, 1, 1008, 1102, 1, 506, 1027, 1102, 488, 1, 1025, 1101, 0, 27, 1013, 1101, 1, 0, 1021, 1101, 0, 34, 1019, 1101, 607, 0, 1028, 1102, 1, 23, 1003, 1102, 26, 1, 1007, 1102, 29, 1, 1009, 1101, 31, 0, 1000, 1102, 37, 1, 1012, 1101, 30, 0, 1005, 1101, 602, 0, 1029, 1101, 36, 0, 1002, 1102, 1, 22, 1001, 1102, 1, 35, 1014, 1102, 24, 1, 1006, 1102, 39, 1, 1017, 109, 4, 21102, 40, 1, 6, 1008, 1010, 40, 63, 1005, 63, 203, 4, 187, 1106, 0, 207, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 13, 1206, 3, 221, 4, 213, 1106, 0, 225, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -5, 1208, -9, 22, 63, 1005, 63, 241, 1106, 0, 247, 4, 231, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -5, 21107, 41, 40, 3, 1005, 1010, 263, 1106, 0, 269, 4, 253, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -1, 1202, 3, 1, 63, 1008, 63, 29, 63, 1005, 63, 295, 4, 275, 1001, 64, 1, 64, 1106, 0, 295, 1002, 64, 2, 64, 109, 16, 21108, 42, 42, -8, 1005, 1014, 313, 4, 301, 1105, 1, 317, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -4, 2105, 1, 5, 1001, 64, 1, 64, 1105, 1, 335, 4, 323, 1002, 64, 2, 64, 109, -5, 1207, -4, 28, 63, 1005, 63, 355, 1001, 64, 1, 64, 1105, 1, 357, 4, 341, 1002, 64, 2, 64, 109, 2, 21102, 43, 1, -1, 1008, 1014, 45, 63, 1005, 63, 377, 1106, 0, 383, 4, 363, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -10, 1208, -3, 36, 63, 1005, 63, 401, 4, 389, 1106, 0, 405, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 6, 21107, 44, 45, 1, 1005, 1012, 423, 4, 411, 1105, 1, 427, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 4, 21101, 45, 0, 3, 1008, 1018, 45, 63, 1005, 63, 453, 4, 433, 1001, 64, 1, 64, 1105, 1, 453, 1002, 64, 2, 64, 109, -23, 2101, 0, 10, 63, 1008, 63, 36, 63, 1005, 63, 475, 4, 459, 1106, 0, 479, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 26, 2105, 1, 6, 4, 485, 1105, 1, 497, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 4, 2106, 0, 5, 1105, 1, 515, 4, 503, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -25, 1201, 10, 0, 63, 1008, 63, 26, 63, 1005, 63, 537, 4, 521, 1105, 1, 541, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 18, 21101, 46, 0, -1, 1008, 1014, 43, 63, 1005, 63, 565, 1001, 64, 1, 64, 1106, 0, 567, 4, 547, 1002, 64, 2, 64, 109, -6, 1201, -4, 0, 63, 1008, 63, 33, 63, 1005, 63, 587, 1105, 1, 593, 4, 573, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 22, 2106, 0, -3, 4, 599, 1105, 1, 611, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -28, 2102, 1, -2, 63, 1008, 63, 22, 63, 1005, 63, 633, 4, 617, 1105, 1, 637, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -1, 21108, 47, 44, 9, 1005, 1011, 653, 1105, 1, 659, 4, 643, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 10, 2107, 24, -8, 63, 1005, 63, 681, 4, 665, 1001, 64, 1, 64, 1105, 1, 681, 1002, 64, 2, 64, 109, -11, 2107, 31, 4, 63, 1005, 63, 697, 1106, 0, 703, 4, 687, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 8, 2101, 0, -8, 63, 1008, 63, 23, 63, 1005, 63, 727, 1001, 64, 1, 64, 1105, 1, 729, 4, 709, 1002, 64, 2, 64, 109, -16, 2108, 21, 10, 63, 1005, 63, 749, 1001, 64, 1, 64, 1106, 0, 751, 4, 735, 1002, 64, 2, 64, 109, 17, 2108, 36, -8, 63, 1005, 63, 769, 4, 757, 1105, 1, 773, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -10, 1207, 1, 23, 63, 1005, 63, 791, 4, 779, 1105, 1, 795, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -3, 2102, 1, 6, 63, 1008, 63, 22, 63, 1005, 63, 815, 1106, 0, 821, 4, 801, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 16, 1205, 7, 837, 1001, 64, 1, 64, 1105, 1, 839, 4, 827, 1002, 64, 2, 64, 109, -5, 1202, 0, 1, 63, 1008, 63, 30, 63, 1005, 63, 863, 1001, 64, 1, 64, 1106, 0, 865, 4, 845, 1002, 64, 2, 64, 109, 4, 1205, 9, 883, 4, 871, 1001, 64, 1, 64, 1106, 0, 883, 1002, 64, 2, 64, 109, 16, 1206, -7, 899, 1001, 64, 1, 64, 1106, 0, 901, 4, 889, 4, 64, 99, 21102, 1, 27, 1, 21101, 915, 0, 0, 1105, 1, 922, 21201, 1, 47633, 1, 204, 1, 99, 109, 3, 1207, -2, 3, 63, 1005, 63, 964, 21201, -2, -1, 1, 21102, 942, 1, 0, 1105, 1, 922, 22102, 1, 1, -1, 21201, -2, -3, 1, 21101, 957, 0, 0, 1106, 0, 922, 22201, 1, -1, -2, 1105, 1, 968, 22101, 0, -2, -2, 109, -3, 2106, 0, 0 };

        static void Main(string[] args)
        {
            List<Task> TaskList = new List<Task>{
                new Task(QuestionOne),
                new Task(QuestionTwo)
                };
            TaskList.ForEach(x => x.Start());
            Task.WaitAll(TaskList.ToArray());
        }

        static void QuestionOne()
        {
            List<long> questionOneOpcodes = opcodesTemplate.Select(x => x).ToList();
            OpCodeMachine(questionOneOpcodes, 1);
        }

        static void QuestionTwo()
        {
            List<long> questionOneOpcodes = opcodesTemplate.Select(x => x).ToList();
            OpCodeMachine(questionOneOpcodes, 2);
        }

        struct Instruction
        {
            public int Operation;
            public int FirstParameter;
            public int SecondParameter;
            public int ThirdParameter;

        }

        static Instruction DetermineOperation(long value)
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

        static int Index(int index, int parameter, int baseOffset, List<long> opcodes, int relativeBase)
        {
            if (parameter == 0)
            {
                return (int)opcodes[index + baseOffset];
            }
            else if (parameter == 1)
            {
                return (int)(index + baseOffset);
            }
            else if (parameter == 2)
            {
                return (int)opcodes[index + baseOffset] + relativeBase;
            }
            else
            {
                Console.WriteLine($"Bad Parameter.");
                return -1;
            }
        }

        static void OpCodeMachine(List<long> opcodes, int input)
        {
            var index = 0;
            var relativeBase = 0;
            for (int i = 0; i < 1000000; ++i)
            {
                opcodes.Add(0);
            }

            while (true)
            {
                index = index % opcodes.Count();
                Instruction instruction = DetermineOperation(opcodes[index]);

                if (instruction.Operation == 1)
                {
                    opcodes[Index(index, instruction.ThirdParameter, 3, opcodes, relativeBase)] =
                        opcodes[Index(index, instruction.FirstParameter, 1, opcodes, relativeBase)] +
                        opcodes[Index(index, instruction.SecondParameter, 2, opcodes, relativeBase)];
                    index += 4;
                }
                else if (instruction.Operation == 2)
                {

                    opcodes[Index(index, instruction.ThirdParameter, 3, opcodes, relativeBase)] =
                    opcodes[Index(index, instruction.FirstParameter, 1, opcodes, relativeBase)] *
                        opcodes[Index(index, instruction.SecondParameter, 2, opcodes, relativeBase)];
                    index += 4;
                }
                else if (instruction.Operation == 3)
                {
                    opcodes[Index(index, instruction.FirstParameter, 1, opcodes, relativeBase)] = input;
                    index += 2;
                }
                else if (instruction.Operation == 4)
                {
                    var val = opcodes[Index(index, instruction.FirstParameter, 1, opcodes, relativeBase)];
                    Console.WriteLine(val);
                    index += 2;
                }
                else if (instruction.Operation == 5)
                {

                    if (opcodes[Index(index, instruction.FirstParameter, 1, opcodes, relativeBase)] != 0)
                        index = (int)opcodes[Index(index, instruction.SecondParameter, 2, opcodes, relativeBase)];
                    else
                        index += 3;
                }
                else if (instruction.Operation == 6)
                {

                    if (opcodes[Index(index, instruction.FirstParameter, 1, opcodes, relativeBase)] == 0)
                        index = (int)opcodes[Index(index, instruction.SecondParameter, 2, opcodes, relativeBase)];
                    else
                        index += 3;
                }
                else if (instruction.Operation == 7)
                {
                    opcodes[Index(index, instruction.ThirdParameter, 3, opcodes, relativeBase)] =
                                       opcodes[Index(index, instruction.FirstParameter, 1, opcodes, relativeBase)] <
                                           opcodes[Index(index, instruction.SecondParameter, 2, opcodes, relativeBase)] ? 1 : 0;
                    index += 4;
                }
                else if (instruction.Operation == 8)
                {
                    opcodes[Index(index, instruction.ThirdParameter, 3, opcodes, relativeBase)] =
                                       opcodes[Index(index, instruction.FirstParameter, 1, opcodes, relativeBase)] ==
                                            opcodes[Index(index, instruction.SecondParameter, 2, opcodes, relativeBase)] ? 1 : 0;
                    index += 4;
                }
                else if (instruction.Operation == 9)
                {
                    relativeBase += (int)opcodes[Index(index, instruction.FirstParameter, 1, opcodes, relativeBase)];
                    index += 2;
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
    }
}
