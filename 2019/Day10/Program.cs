using System;
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

            int maxCount = 0;

            foreach(var star in stars)
            {
                var vectorList = new List<Point>();

                foreach(var star2 in stars)
                {
                    //Are they the same?
                    if (star.X == star2.X && star.Y == star2.Y)                    
                        break;

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
                        Console.WriteLine($"Star at {star.X},{star.Y} sees star at {star2.X},{star2.Y}.");
                        vectorList.Add(vector);
                    }
                    
                }

                if (vectorList.Count > maxCount)
                    maxCount = vectorList.Count;
            }

            Console.WriteLine($"Q1: {maxCount}");
        }


        private  static int GCD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            if (b == 0)
                return 0;

            // Pull out remainders.
            while(true)
            {
                int remainder = a % b;
                if (remainder == 0) return b;
                a = b;
                b = remainder;
            };
        }

        static void Q1()
        {            
            Vectors(split(Input.example));            
            //Vectors(split(Input.example2));            
            //Vectors(split(Input.example3));            
            //Vectors(split(Input.real));

            Console.ReadKey();
        }

        static void Q2()
        {
        }
    }
}
