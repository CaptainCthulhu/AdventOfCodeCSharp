using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Day21
{
    class Program
    {
        static readonly bool Testing = false;
        static readonly bool Debug = false;
        static string Input;
        static List<Food> Foods = new List<Food>();

        static Dictionary<string, List<string>> PossibleNames = new Dictionary<string, List<string>>();

        static void Main()
        {
            GetDetails();
            Parse();
            QuestionOne();
            QuestionTwo();            
            Console.WriteLine("Done");
        }       

        static void QuestionOne() 
        {
            int answer = 0;
            FindNamesWithAllergens();
            var allergenNames = PossibleNames.SelectMany(x => x.Value).ToList();
            foreach (var name in Foods.SelectMany(x => x.Names))
            {
                    if (!allergenNames.Contains(name))
                        answer++;
            }
            //DoStuff
            Console.WriteLine($"Question One:{answer}");
        }

        static void QuestionTwo() 
        {
            var sortedNames = PossibleNames.Keys.ToList();
            sortedNames.Sort();
            var elements = new List<string>();
            foreach (var key in sortedNames)
            {
                elements.Add(PossibleNames[key].First());
            }
            Console.WriteLine($"Question Two:{String.Join(',', elements)}");
        }

        static void FindNamesWithAllergens()
        {
            foreach (var allergen in Foods.SelectMany(x => x.Allergens).Distinct())
            {
                Dictionary<string, int> possibleNamesCounts = new Dictionary<string, int>();
                int foodsWithAllergen = 0;
                foreach (Food f in Foods)
                {
                    if (f.Allergens.Contains(allergen))
                    {
                        foodsWithAllergen++;
                        foreach (string foodName in f.Names)
                        {
                            if (possibleNamesCounts.ContainsKey(foodName))
                                possibleNamesCounts[foodName]++;
                            else
                                possibleNamesCounts[foodName] = 1;
                        }
                    }
                }

                foreach (var key in possibleNamesCounts.Keys)
                {
                    if (possibleNamesCounts[key] != foodsWithAllergen)
                        possibleNamesCounts.Remove(key);
                }

                PossibleNames[allergen] = possibleNamesCounts.Keys.ToList();
            }
            
            while (PossibleNames.Keys.Any(x => PossibleNames[x].Count > 1))
                {
                    foreach (var key in PossibleNames.Keys)
                    {
                        if (PossibleNames[key].Count == 1)
                        {
                            var onlyPossible = PossibleNames[key].First();
                            foreach (var key2 in PossibleNames.Keys)
                            {
                                if (!key2.Equals(key))
                                {
                                    PossibleNames[key2].Remove(onlyPossible);
                                }
                            }
                        }
                    }
                }
        }

        public void Log(string s)
        {
            if (Debug)
            {
                Console.WriteLine(s);
            }
        }

        static void Parse()
        {
            foreach (var line in Input.Split('\n'))
            {
                var elements = line.Trim().Split("(contains ");
                var ingredients = elements[0].Replace(",", "").Trim();
                var allergens = elements[1].Replace(",", "").Replace(")", "").Trim();
                Foods.Add(new Food(ingredients, allergens));
            }
            
        }

        static void GetDetails()
        {

            string input;
            //code with known good solutions.
            if (Testing)
                input = @"mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)";
            else
            {
                if (!File.Exists("input.txt"))
                {
                    //boilerplate grab info
                    var uri = "https://adventofcode.com/2020/day/21/input";
                    var sessionId = "";

                    var webRequest = WebRequest.Create(uri) as HttpWebRequest;
                    webRequest.CookieContainer = new CookieContainer();
                    var cookie = new Cookie("session", sessionId);
                    cookie.Domain = "adventofcode.com";

                    webRequest.CookieContainer.Add(cookie);
                    var responseStream = webRequest.GetResponse().GetResponseStream();

                    using (var fileStream = new FileStream("input.txt", FileMode.Create, FileAccess.Write))
                    {
                        responseStream.CopyTo(fileStream);
                    }
                }

                input = File.ReadAllText("input.txt");
            }

            Input = input.Replace("\r", "").Trim();
        }
    }  
}
