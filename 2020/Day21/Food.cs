using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day21
{
    class Food
    {
        public List<String> Names;
        public List<String> Allergens;

        public Food(string names, string allergens)
        {
            Names = names.Split(" ").Select(x => x.Trim()).ToList();
            Allergens = allergens.Split(" ").Select(x => x.Trim()).ToList();
        }
    }
}