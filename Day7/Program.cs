using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Day7
{
    class Program
    {
        static int[] test = new int[] { 3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26, 27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5 };
        static int[] opCodeTemplate = new int[] { 3, 8, 1001, 8, 10, 8, 105, 1, 0, 0, 21, 46, 63, 76, 97, 118, 199, 280, 361, 442, 99999, 3, 9, 102, 4, 9, 9, 101, 2, 9, 9, 1002, 9, 5, 9, 101, 4, 9, 9, 102, 2, 9, 9, 4, 9, 99, 3, 9, 101, 5, 9, 9, 102, 3, 9, 9, 101, 3, 9, 9, 4, 9, 99, 3, 9, 1001, 9, 2, 9, 102, 3, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 5, 9, 101, 4, 9, 9, 1002, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 5, 9, 101, 3, 9, 9, 1002, 9, 5, 9, 1001, 9, 5, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99 };

        static void Main(string[] args)
        {
            QuestionOne();
            QuestionTwo();
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
                                    Machine machineA = new Machine(opCodes, a);
                                    Machine machineB = new Machine(opCodes, b);
                                    Machine machineC = new Machine(opCodes, c);
                                    Machine machineD = new Machine(opCodes, d);
                                    Machine machineE = new Machine(opCodes, e);

                                    machineA.Run(machineE.Output);
                                    machineB.Run(machineA.Output);
                                    machineC.Run(machineB.Output);
                                    machineD.Run(machineC.Output);
                                    machineE.Run(machineD.Output);

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
            Console.WriteLine("Question One: " + signal);
        }


        static void QuestionTwo()
        {
            int[] opCodes = (int[])opCodeTemplate.Clone();

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
                                    var tempQueue = new Queue<int>();
                                    tempQueue.Enqueue(a);
                                    tempQueue.Enqueue(0);
                                    MachineAsync machineA = new MachineAsync(opCodes, tempQueue, "A");

                                    tempQueue = new Queue<int>();
                                    tempQueue.Enqueue(b);
                                    MachineAsync machineB = new MachineAsync(opCodes, tempQueue, "B");
                                    machineA.outputsTo = machineB;

                                    tempQueue = new Queue<int>();
                                    tempQueue.Enqueue(c);
                                    MachineAsync machineC = new MachineAsync(opCodes, tempQueue, "C");
                                    machineB.outputsTo = machineC;

                                    tempQueue = new Queue<int>();
                                    tempQueue.Enqueue(d);
                                    MachineAsync machineD = new MachineAsync(opCodes, tempQueue, "D");
                                    machineC.outputsTo = machineD;

                                    tempQueue = new Queue<int>();
                                    tempQueue.Enqueue(e);
                                    MachineAsync machineE = new MachineAsync(opCodes, tempQueue, "E");
                                    machineD.outputsTo = machineE;
                                    machineE.outputsTo = machineA;

                                    List<Task> TaskList= new List<Task>{
                                    new Task(machineA.Run),
                                    new Task(machineB.Run),
                                    new Task(machineC.Run),
                                    new Task(machineD.Run),
                                     new Task(machineE.Run)
                                    };

                                    TaskList.ForEach(x => x.Start());

                                    Task.WaitAll(TaskList.ToArray());

                                    phasesetting = e + d * 10 + c * 100 + b * 1000 + a * 10000;

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
            Console.WriteLine("Question Two: " + signal + " Phase Setting: " + phasesetting);
        }
    }
}
