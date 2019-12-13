﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Day11
{
    class Program
    {
        static List<long> input = new List<long> { 3, 8, 1005, 8, 338, 1106, 0, 11, 0, 0, 0, 104, 1, 104, 0, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 1008, 8, 1, 10, 4, 10, 1002, 8, 1, 29, 2, 105, 19, 10, 1006, 0, 52, 1, 1009, 7, 10, 1006, 0, 6, 3, 8, 102, -1, 8, 10, 101, 1, 10, 10, 4, 10, 108, 1, 8, 10, 4, 10, 1001, 8, 0, 64, 2, 1002, 19, 10, 1, 8, 13, 10, 1, 1108, 16, 10, 2, 1003, 1, 10, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 1008, 8, 1, 10, 4, 10, 1002, 8, 1, 103, 1006, 0, 10, 2, 109, 16, 10, 1, 102, 11, 10, 2, 6, 13, 10, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 1008, 8, 0, 10, 4, 10, 1002, 8, 1, 140, 2, 102, 8, 10, 2, 4, 14, 10, 1, 8, 19, 10, 1006, 0, 24, 3, 8, 1002, 8, -1, 10, 101, 1, 10, 10, 4, 10, 1008, 8, 0, 10, 4, 10, 1001, 8, 0, 177, 1006, 0, 16, 1, 1007, 17, 10, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 108, 1, 8, 10, 4, 10, 101, 0, 8, 205, 3, 8, 1002, 8, -1, 10, 1001, 10, 1, 10, 4, 10, 1008, 8, 0, 10, 4, 10, 102, 1, 8, 228, 1, 1005, 1, 10, 1, 9, 1, 10, 3, 8, 102, -1, 8, 10, 101, 1, 10, 10, 4, 10, 1008, 8, 1, 10, 4, 10, 1002, 8, 1, 258, 3, 8, 1002, 8, -1, 10, 1001, 10, 1, 10, 4, 10, 108, 0, 8, 10, 4, 10, 102, 1, 8, 279, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 108, 0, 8, 10, 4, 10, 102, 1, 8, 301, 1, 3, 17, 10, 2, 7, 14, 10, 2, 6, 18, 10, 1, 1001, 17, 10, 101, 1, 9, 9, 1007, 9, 1088, 10, 1005, 10, 15, 99, 109, 660, 104, 0, 104, 1, 21102, 1, 48092525312, 1, 21101, 355, 0, 0, 1106, 0, 459, 21102, 665750184716, 1, 1, 21102, 366, 1, 0, 1106, 0, 459, 3, 10, 104, 0, 104, 1, 3, 10, 104, 0, 104, 0, 3, 10, 104, 0, 104, 1, 3, 10, 104, 0, 104, 1, 3, 10, 104, 0, 104, 0, 3, 10, 104, 0, 104, 1, 21102, 1, 235324768296, 1, 21101, 0, 413, 0, 1105, 1, 459, 21101, 3263212736, 0, 1, 21102, 424, 1, 0, 1106, 0, 459, 3, 10, 104, 0, 104, 0, 3, 10, 104, 0, 104, 0, 21102, 1, 709496824676, 1, 21101, 447, 0, 0, 1105, 1, 459, 21102, 988220904204, 1, 1, 21102, 1, 458, 0, 1106, 0, 459, 99, 109, 2, 21201, -1, 0, 1, 21102, 40, 1, 2, 21102, 490, 1, 3, 21102, 1, 480, 0, 1105, 1, 523, 109, -2, 2106, 0, 0, 0, 1, 0, 0, 1, 109, 2, 3, 10, 204, -1, 1001, 485, 486, 501, 4, 0, 1001, 485, 1, 485, 108, 4, 485, 10, 1006, 10, 517, 1101, 0, 0, 485, 109, -2, 2105, 1, 0, 0, 109, 4, 2101, 0, -1, 522, 1207, -3, 0, 10, 1006, 10, 540, 21102, 0, 1, -3, 22101, 0, -3, 1, 22102, 1, -2, 2, 21102, 1, 1, 3, 21101, 559, 0, 0, 1106, 0, 564, 109, -4, 2105, 1, 0, 109, 5, 1207, -3, 1, 10, 1006, 10, 587, 2207, -4, -2, 10, 1006, 10, 587, 22102, 1, -4, -4, 1105, 1, 655, 22101, 0, -4, 1, 21201, -3, -1, 2, 21202, -2, 2, 3, 21102, 606, 1, 0, 1105, 1, 564, 21202, 1, 1, -4, 21101, 0, 1, -1, 2207, -4, -2, 10, 1006, 10, 625, 21102, 0, 1, -1, 22202, -2, -1, -2, 2107, 0, -3, 10, 1006, 10, 647, 22101, 0, -1, 1, 21101, 647, 0, 0, 105, 1, 522, 21202, -2, -1, -2, 22201, -4, -2, -4, 109, -5, 2106, 0, 0 };
        static List<Instruction> instructions = new List<Instruction>();
        static List<PaintedSquare> paintedSquares = new List<PaintedSquare>();
        static bool q2 = false;

        static void Main(string[] args)
        {
            QuestionOne();
            QuestionTwo();
        }

        static void QuestionOne()
        {
            Interop.OpCodeMachine(Clone());
            var locations = paintedSquares.Select(x => x.location);
            Console.WriteLine($"Question 1: {locations.Distinct().Count()}");
        }

        static List<long> Clone()
        {
            List<long> returnList = new List<long>();
            foreach (var i in input)
                returnList.Add(i);

            return returnList;
        }

        static void QuestionTwo()
        {
            q2 = true;
            Interop.OpCodeMachine(Clone());
            var locations = paintedSquares.Where(x => x.color == 1).Select(x => x.location);
            string result = "";
            for (int y = locations.Max(i => i.Y); y >= locations.Min(i => i.Y); y--)
            {
                for (int x = locations.Min(i => i.X); x <= locations.Max(i => i.X); x++)
                {
                    if (locations.Any(i => i.X == x && i.Y == y))
                    {
                        result += "█";
                    }
                    else
                    {
                        result += " ";
                    }
                }
                result += "\n";
            }

            Console.WriteLine($"Question 2: \n{result}");
        }




        public static int Instructions(Queue<long> outputs)
        {
            while (outputs.Count >= 2)
            {
                var color = outputs.Dequeue();
                var direction = outputs.Dequeue();

                instructions.Add(new Instruction(color, direction, paintedSquares, instructions));
            }

            if (instructions.Count > 0)
            {
                var currentLocation = instructions.Last().location;
                if (paintedSquares.Count > 0)
                {
                    var squares = paintedSquares.Where(x => x.location == currentLocation);
                    if (squares.Count() > 0)
                    {
                        var square = squares.Last();
                        if (square != null)
                            return (int)square.color;
                    }
                }
            }
            if (q2 == true || instructions.Count == 0)
                return 1;
            return 0;
        }    
    }
}
