using System;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{   
    class Interop
    {

        static public Queue<long> Outputs = new Queue<long>();

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



        static public void OpCodeMachine(List<long> opcodes)
        {       

            for (int i = 0; i < 10000000; i++)
            {
                opcodes.Add(0);
            }

            int index = 0;
            int relativeBase = 0;

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
                    opcodes[Index(index, instruction.FirstParameter, 1, opcodes, relativeBase)] = Program.GetInput();

                    index += 2;
                }
                else if (instruction.Operation == 4)
                {
                    long val = opcodes[Index(index, instruction.FirstParameter, 1, opcodes, relativeBase)];
                    Outputs.Enqueue(val);

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
