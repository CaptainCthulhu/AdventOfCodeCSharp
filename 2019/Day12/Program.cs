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
            Console.ReadKey();
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
            long iteration = 0;
            var originalState = CopyPlanets(workingPlanets);

            long xCycles = 0;
            long yCycles = 0;
            long zCycles = 0;

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

                //check x
                bool good = true;
                if (xCycles == 0)
                {
                    for (int i = 0; i < workingPlanets.Count(); i++)
                    {
                        good = Planet.checkAxis(workingPlanets[i].location.X, originalState[i].location.X, workingPlanets[i].velocity.X,  originalState[i].velocity.X);
                        if (good == false)
                            break;
                    }
                    if (good)
                        xCycles = iteration;
                }


                if (yCycles == 0)
                {
                    good = true;
                    for (int i = 0; i < workingPlanets.Count(); i++)
                    {
                        good = Planet.checkAxis(workingPlanets[i].location.Y, originalState[i].location.Y, workingPlanets[i].velocity.Y,  originalState[i].velocity.Y);
                        if (good == false)
                            break;
                    }
                    if (good)
                        yCycles = iteration;
                }

                if (zCycles == 0)
                {
                    good = true;
                    for (int i = 0; i < workingPlanets.Count(); i++)
                    {
                        good = Planet.checkAxis(workingPlanets[i].location.Z, originalState[i].location.Z, workingPlanets[i].velocity.Z,  originalState[i].velocity.Z);
                        if (good == false)
                            break;
                    }
                    if (good)
                        zCycles = iteration;
                }

            } while (xCycles == 0 || yCycles == 0 || zCycles == 0);
	    //Cheated a bit, used Wolfram Alpha to find the LCM
            Console.WriteLine($"Q2: {xCycles} {yCycles} {zCycles}");
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
            if (Stopped(planets))
            {
                for (int i = 0; i < planets.Count(); i++)
                {
                    var answer = Planet.Equivalent(planets[i], originalState[i]);
                    if (!answer)
                        return false;
                }
            }
            else
                return false;

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

