using System;
using System.Linq;
using System.Collections.Generic;

namespace Day12
{
    class Program
    {      

        static void Main(string[] args)
        {
            Q1();
            Q2();
        }

        static private void Q1()
        {
            var workingPlanets = Inputs.planets;

            int iteration = 0;
            int goal = 1000;
            while (iteration < goal)
            {
                //Update Vector
                foreach (var x in workingPlanets)
                {
                    x.Update(workingPlanets.Where(y => y != x).ToList());
                }

                //Move
                foreach (var x in workingPlanets)
                {
                    x.Move();
                }

                iteration++;
            }
            //4108 wrong
            Console.WriteLine($"Q1: {workingPlanets.Sum(x => x.Energy())}");
        }

        static private void Q2()
        {
            var workingPlanets = Inputs.planets;
            var iteration = 0;
            List<Planet[]> previousState = new List<Planet[]>();

            while (!Found(workingPlanets, previousState))
            {
                //Update Vector
                foreach (var x in workingPlanets)
                {
                    x.Update(workingPlanets.Where(y => y != x).ToList());
                }

                //Move
                foreach (var x in workingPlanets)
                {
                    x.Move();
                }

                iteration++;
            }

            Console.WriteLine($"Q2: {iteration}");
        }

        static bool Found(Planet[] planets, List<Planet[]> previousStates)
        {
            return false;
        }
    }
}
