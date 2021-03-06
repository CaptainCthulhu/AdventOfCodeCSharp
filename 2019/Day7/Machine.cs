﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    class Machine
    {
        int[] OpCodes;
        public int Index = 0;
        public int Input = 0;
        public int Output = 0;
        public int Code = 0;

        public Machine(int[] opCodes, int phaseSetting)
        {
            this.OpCodes = (int[])opCodes.Clone();
            this.Input = phaseSetting;
        }

        public void Run(int currentInput)
        {
            while (true)
            {
                Index = Index % OpCodes.Length;
                Instruction instruction = DetermineOperation(OpCodes[Index]);

                if (instruction.Operation == 1)
                {
                    OpCodes[instruction.ThirdParameter == 0 ? OpCodes[Index + 3] : Index + 3] =
                        (instruction.FirstParameter == 0 ? OpCodes[OpCodes[Index + 1]] : OpCodes[Index + 1]) +
                        (instruction.SecondParameter == 0 ? OpCodes[OpCodes[Index + 2]] : OpCodes[Index + 2]);
                    Index += 4;
                }
                else if (instruction.Operation == 2)
                {

                    OpCodes[instruction.ThirdParameter == 0 ? OpCodes[Index + 3] : Index + 3] =
                    (instruction.FirstParameter == 0 ? OpCodes[OpCodes[Index + 1]] : OpCodes[Index + 1]) *
                        (instruction.SecondParameter == 0 ? OpCodes[OpCodes[Index + 2]] : OpCodes[Index + 2]);
                    Index += 4;
                }
                else if (instruction.Operation == 3)
                {
                    OpCodes[instruction.FirstParameter == 0 ? OpCodes[Index + 1] : Index + 1] = this.Input;
                    this.Input = currentInput;
                    Index += 2;
                }
                else if (instruction.Operation == 4)
                {
                    Output = instruction.FirstParameter == 0 ? OpCodes[OpCodes[Index + 1]] : OpCodes[Index + 1];
                    Index += 2;               
                    return;
                }
                else if (instruction.Operation == 5)
                {
                    var firstParamterPointer = instruction.FirstParameter == 0 ? OpCodes[Index + 1] : (Index + 1);
                    if (OpCodes[firstParamterPointer] != 0)
                        Index = (instruction.SecondParameter == 0 ? OpCodes[OpCodes[Index + 2]] : OpCodes[Index + 2]);
                    else
                        Index += 3;
                }
                else if (instruction.Operation == 6)
                {
                    var firstParameterPointer = instruction.FirstParameter == 0 ? OpCodes[Index + 1] : (Index + 1);
                    if (OpCodes[firstParameterPointer] == 0)
                        Index = (instruction.SecondParameter == 0 ? OpCodes[OpCodes[Index + 2]] : OpCodes[Index + 2]);
                    else
                        Index += 3;
                }
                else if (instruction.Operation == 7)
                {
                    OpCodes[instruction.ThirdParameter == 0 ? OpCodes[Index + 3] : Index + 3] =
                                       ((instruction.FirstParameter == 0 ? OpCodes[OpCodes[Index + 1]] : OpCodes[Index + 1]) <
                                           (instruction.SecondParameter == 0 ? OpCodes[OpCodes[Index + 2]] : OpCodes[Index + 2])) ? 1 : 0;
                    Index += 4;
                }
                else if (instruction.Operation == 8)
                {
                    OpCodes[instruction.ThirdParameter == 0 ? OpCodes[Index + 3] : Index + 3] =
                                       ((instruction.FirstParameter == 0 ? OpCodes[OpCodes[Index + 1]] : OpCodes[Index + 1]) ==
                                           (instruction.SecondParameter == 0 ? OpCodes[OpCodes[Index + 2]] : OpCodes[Index + 2])) ? 1 : 0;
                    Index += 4;
                }
                else if (instruction.Operation == 99)
                {
                    Code = 99;
                    return;
                }
                else
                {
                    Console.WriteLine($"Bad Opcode: {instruction.Operation}. Point Location: {Index}.");
                    break;
                }
            }
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
    }
}