using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day3
{
    class Program
    {
        static int goal = 265149;
        enum Move { Up, Down, Left, Right };

        static Dictionary<Point, int> Grid = new Dictionary<Point, int>();
        static Point Current;
        static Move CurrentDirection;

        static void Reset()
        {
            Grid = new Dictionary<Point, int>();
            Current = new Point(0, 0);
            CurrentDirection = Move.Right;
        }

        static void FindNextLocation()
        {
            switch (CurrentDirection)
            {
                case Move.Up:
                    Current.Y += 1;
                    if (!Grid.ContainsKey(new Point(Current.X - 1, Current.Y)))
                        CurrentDirection = Move.Left;
                    break;
                case Move.Down:
                    Current.Y -= 1;
                    if (!Grid.ContainsKey(new Point(Current.X + 1, Current.Y)))
                        CurrentDirection = Move.Right;
                    break;
                case Move.Left:
                    Current.X -= 1;
                    if (!Grid.ContainsKey(new Point(Current.X, Current.Y - 1)))
                        CurrentDirection = Move.Down;
                    break;
                case Move.Right:
                    Current.X += 1;
                    if (!Grid.ContainsKey(new Point(Current.X, Current.Y + 1)))
                        CurrentDirection = Move.Up;
                    break;
            }
        }

        static void Calculate(int currentValue)
        {
            Grid[Current] = currentValue;
            FindNextLocation();
        }

        static int CalculateValue()
        {
            var runningTally = 0;
            for (int x = Current.X - 1; x <= Current.X + 1; x++)
            {
                for (int y = Current.Y - 1; y <= Current.Y + 1; y++)
                {
                    var val = 0;
                    if (Grid.TryGetValue(new Point(x, y), out val))
                        runningTally += val;
                }
            }
            runningTally = runningTally == 0 ? 1 : runningTally;
            Grid[Current] = runningTally;
            FindNextLocation();
            return runningTally;
        }

        static void PartOne()
        {
            Reset();
            for (int i = 1; i <= goal; i++)
                Calculate(i);

            var key = Grid.FirstOrDefault(x => x.Value == goal).Key;
            Console.WriteLine("PartOne: " + (Math.Abs(key.X) + Math.Abs(key.Y)));
        }

        static void PartTwo()
        {
            Reset();

            var currentValue = 0;
            do
            {
                currentValue = CalculateValue();
            } while (currentValue < goal);

            Console.WriteLine("PartTwo: " + currentValue);
        }

        static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }
    }
}
