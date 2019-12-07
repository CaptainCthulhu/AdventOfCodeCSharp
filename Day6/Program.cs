using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    class Program
    {
        static string[] orbits = Values.real;


        class Planet
        {
            public string name;
            public List<Planet> orbits;
            public List<Planet> orbitsBy;
        }

        static void Main(string[] args)
        {
            QuestionOne();
            QuestionTwo();
        }

        static void PopulateSolarSystem(List<Planet> planets)
        {
            foreach (var orbit in orbits)
            {
                string planetName1 = orbit.Split(")")[0];
                string planetName2 = orbit.Split(")")[1];

                var planet1 = planets.Where(x => x.name == planetName1).FirstOrDefault();
                var planet2 = planets.Where(x => x.name == planetName2).FirstOrDefault();

                if (planet1 == null)
                {
                    planet1 = new Planet();
                    planet1.name = planetName1;
                    planet1.orbits = new List<Planet>();
                    planet1.orbitsBy = new List<Planet>();
                    planets.Add(planet1);
                }
                if (planet2 == null)
                {
                    planet2 = new Planet();
                    planet2.name = planetName2;
                    planet2.orbits = new List<Planet>();
                    planet2.orbitsBy = new List<Planet>();
                    planets.Add(planet2);
                }

                planet1.orbitsBy.Add(planet2);
                planet2.orbits.Add(planet1);

            }
        }

        static int CountOrbits(List<Planet> planets)
        {
            int count = 0;

            var rootPlanet = planets.Where(x => x.orbits.Count == 0).FirstOrDefault();

            foreach (var planet in planets)
            {
                int planetCount = 0;
                if (planet != rootPlanet)
                {
                    var currentPlanet = planet;
                    do
                    {
                        currentPlanet = currentPlanet.orbits.FirstOrDefault();
                        planetCount++;
                    } while (currentPlanet != rootPlanet);
                    count += planetCount;
                }
            }

            return count;
        }

        static int CountTransfers(List<Planet> planets)
        {
            var transfers = 0;
            var startPlanet = planets.Where(x => x.name == "YOU").FirstOrDefault().orbits.FirstOrDefault();
            var endPlanet = planets.Where(x => x.name == "SAN").FirstOrDefault().orbits.FirstOrDefault();
            var rootPlanet = planets.Where(x => x.orbits.Count == 0).FirstOrDefault();

            var Path1 = new List<Planet>();
            var Path2 = new List<Planet>();

            var currentPlanet = startPlanet;
            do
            {
                currentPlanet = currentPlanet.orbits.FirstOrDefault();
                Path1.Add(currentPlanet);
            } while (currentPlanet != rootPlanet);

            currentPlanet = endPlanet;
            do
            {
                currentPlanet = currentPlanet.orbits.FirstOrDefault();
                Path2.Add(currentPlanet);
            } while (currentPlanet != rootPlanet && !Path1.Contains(currentPlanet));

            var overLapPoint = Path1.FindIndex(x => x.name == Path2.Last().name);

            transfers = overLapPoint +  Path2.Count + 1; 

            return transfers;
        }

        static void QuestionOne()
        {
            List<Planet> planets = new List<Planet>();
            PopulateSolarSystem(planets);
            Console.WriteLine("Question One: " + CountOrbits(planets));
        }

        static void QuestionTwo()
        {
            List<Planet> planets = new List<Planet>();
            PopulateSolarSystem(planets);
            CountTransfers(planets);
            Console.WriteLine("Question Two: " + CountTransfers(planets));
        }
    }
}
