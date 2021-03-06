﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            Q1();
            Q2();
        }

        static List<Point> split(string s)
        {
            List<Point> stars = new List<Point>();

            var rows = s.Split("\r\n").ToList();
            rows.ForEach(x => x.Trim());

            for (int y = 0; y < rows.Count; y++)
            {
                for (int x = 0; x < rows[0].Length; x++)
                {
                    if (rows[y][x] == '#')
                        stars.Add(new Point(x, y));
                }
            }

            return stars;
        }


        private static void Vectors(List<Point> stars)
        {
            Point bestPoint = new Point();
            int maxCount = 0;

            foreach (var star in stars)
            {
                var vectorList = new List<Point>();

                foreach (var star2 in stars)
                {
                    //Are they the same?
                    if (star.X == star2.X && star.Y == star2.Y)
                        continue;

                    var vector = new Point(star2.X - star.X, star2.Y - star.Y);
                    if (vector.X == 0 || vector.Y == 0)
                    {
                        vector.X = vector.X == 0 ? 0 : vector.X / Math.Abs(vector.X);
                        vector.Y = vector.Y == 0 ? 0 : vector.Y / Math.Abs(vector.Y);
                    }
                    else
                    {
                        var gcd = GCD(vector.X, vector.Y);
                        vector = new Point(vector.X / gcd, vector.Y / gcd);
                    }
                    if (!(vectorList.Any(v => v.X == vector.X && v.Y == vector.Y)))
                    {
                        vectorList.Add(vector);
                    }

                }

                if (vectorList.Count > maxCount)
                {
                    bestPoint = star;
                    maxCount = vectorList.Count;
                }
            }

            Console.WriteLine($"Q1: {maxCount}. Best Point: {bestPoint}");
        }


        private static int GCD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            if (b == 0)
                return 0;

            // Pull out remainders.
            while (true)
            {
                int remainder = a % b;
                if (remainder == 0) return b;
                a = b;
                b = remainder;
            };
        }

        private static void Eliminate(List<Point> stars, Point shooter)
        {
            stars.Remove(shooter);

            List<TargetDetails> targetDetails = new List<TargetDetails>();
            stars.ForEach(x => targetDetails.Add(TargetDetails.CreateTarget(shooter, x)));

            var degrees = targetDetails.Select(x => x.Degrees);
            var degress2 = degrees.Distinct();
            var degrees3 = degress2.ToList();
            degrees3.Sort();
            Console.WriteLine($"Sanity check: {degrees3.Count} count.");

            Dictionary<decimal, List<TargetDetails>> groupDetails = new Dictionary<decimal, List<TargetDetails>>();

            foreach (var degree in degrees3)
            {
                List<TargetDetails> degreeList = targetDetails.Where(x => x.Degrees == degree).ToList();
                degreeList.Sort((emp1, emp2) => emp1.Distance.CompareTo(emp2.Distance));
                groupDetails[degree] = degreeList;
            }

            int eliminated = 0;
            Point target = new Point();
            int goal = 200;
            List<TargetDetails> eliminatedTargets = new List<TargetDetails>();

            bool start = true;

            while (eliminated < goal)
            {
                foreach (var angle in degrees3)
                {
                    if (start && angle < 270)
                    {
                    }
                    else
                    {
                        start = false;

                        var targets = groupDetails[angle];
                        eliminatedTargets.Add(targets[0]);
                        target = targets[0].Location;
                        Console.WriteLine($"Target #{eliminated + 1} eliminated at {target.X},{target.Y}, angle {targets[0].Degrees}, distance {targets[0].Distance}. Value: {target.X * 100 + target.Y}. Left at angle {targets[0].Degrees}? {targets.Count()}");
                        


                        targets.RemoveAt(0);
                        if (groupDetails[angle].Count == 0)
                            groupDetails.Remove(angle);
                        eliminated++;

                        if (eliminated == 200)
                            break;
                    }
                }

            }
            //1532 too high 631???  2????? Nope
            Console.WriteLine($"Q2: {target.X * 100 + target.Y}");

        }


        static void Q1()
        {
            Vectors(split(Input.real));
        }

        static void Q2()
        {

            Eliminate(split(Input.example3), new Point(11, 13));
            //Best Point X= 17,Y = 22
            Eliminate(split(Input.real), new Point(17, 22));
        }
    }
}