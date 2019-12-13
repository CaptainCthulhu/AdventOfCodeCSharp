using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Day12
{
    class Program
    {

        static Planet[] planets = new Planet[] {                
            new Planet(3,6,6),
            new Planet(10,7,-9),
            new Planet(-3,7,9),
            new Planet(-8,0,4)
        };   

        static void Main(string[] args)
        {
            Q1();
            Q2();
        }

        static private void Q1()
        {
            int iteration = 0;
            int goal = 1000;
            while (iteration < goal)
            {

                foreach (var x in planets)
                {
                    //hard stuff in here
                }

                iteration++;
            }

        }

        static private void Q2()
        {

        }
    }
}
