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
            var workingPlanets = CopyPlanets(Inputs.planets);

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
            var workingPlanets = CopyPlanets(Inputs.planets);
            var iteration = 0;
            var originalState = CopyPlanets(Inputs.planets);

            do
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
                if (Stopped(workingPlanets))
                    Console.WriteLine($"Stopped {iteration}. Solution? {iteration * 2}");

            } while (!Found(workingPlanets, originalState));

            Console.WriteLine($"Q2: {iteration}");
        }

        static bool Stopped(Planet[] planets)
        {

            for (int i = 0; i < planets.Count(); i++)
            {
                bool answer = planets[i].velocity.X == 0 && planets[i].velocity.Y == 0 && planets[i].velocity.Z == 0;
                if (!answer)
                    return false;
            }

            return true;
        }

        static bool Found(Planet[] planets, Planet[] originalState)
        {

            for (int i = 0; i < planets.Count(); i++)
            {
                var answer = Planet.Equivalent(planets[i], originalState[i]);
                if (!answer)
                    return false;
            }

            return true;
        }

        static Planet[] CopyPlanets(Planet[] planets)
        {
            var returnPlanets = new Planet[planets.Length];

            for (int i = 0; i < planets.Length; i++)
            {
                returnPlanets[i] = Copy(planets[i]);
            }

            return returnPlanets;
        }

        static Planet Copy(Planet planet)
        {
            return new Planet(planet.location.X, planet.location.Y, planet.location.Z);
        }
    }
}
