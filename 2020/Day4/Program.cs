using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace DayFour
{
    class Program
    {
        static void Main(string[] args)
        {
             //now do real work
            var information = Parse(GetDetails().Trim());
            QuestionOne(information);
            QuestionTwo(information);
            Console.WriteLine("Done");
        }
        static void QuestionOne(List<Passport> passports)
        {
            var answer = 0;

            foreach(var passport in passports)
            {
                if (DetailsPresent(passport))
                    answer++;
            }

            Console.WriteLine($"Question One:{answer}");
        }
        static void QuestionTwo(List<Passport> passports)
        {
            int answer = 0;
            var validPassports = passports.Where(x => DetailsPresent(x)).ToList();
            foreach(var passport in validPassports)
            {
                var valid = true;
                passport.byr = Convert.ToInt32(passport.properties["byr"]);
                if (passport.byr < 1920 || passport.byr > 2002)
                    valid = false;

                passport.iyr = Convert.ToInt32(passport.properties["iyr"]);
                if (passport.iyr < 2010 || passport.iyr > 2020)
                    valid = false;

                passport.eyr = Convert.ToInt32(passport.properties["eyr"]);
                if (passport.eyr < 2020 || passport.eyr > 2030)
                    valid = false;

                passport.hgt = passport.properties["hgt"];
                if (passport.hgt.Contains("cm") || passport.hgt.Contains("in"))
                {
                    if (passport.hgt.Contains("cm"))
                    {
                        var hgtcm = passport.hgt.Replace("cm", "");
                        var hgt = Convert.ToInt32(hgtcm);
                        if (hgt < 150 || hgt > 193)
                            valid = false;
                    }
                    else
                    {
                        var hgtin = passport.hgt.Replace("in", "");
                        var hgt = Convert.ToInt32(hgtin);
                        if (hgt < 59 || hgt > 76)
                            valid = false;
                    }
                }
                else
                {
                    valid = false;
                }

                passport.hcl = passport.properties["hcl"];
                Regex rx = new Regex(@"^#(?:[0-9a-f]{6})$", RegexOptions.Compiled);
                if (rx.Matches(passport.hcl).Count() == 0)
                    valid = false;

                passport.ecl = passport.properties["ecl"];
                List<string> options = new List<String>{"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
                if(!options.Contains(passport.ecl))
                    valid = false;

                passport.pid = passport.properties["pid"];
                rx = new Regex(@"^[0-9]{9}$", RegexOptions.Compiled);
                if (rx.Matches(passport.pid).Count() == 0)
                    valid = false;


                if (valid)
                    answer++;

            }

            Console.WriteLine($"Question Two:{answer}");
        }

        static bool DetailsPresent(Passport passport)
        {
                bool allFound = true;
                foreach(var prop in Passport.requiredItems)
                {
                    if (!passport.properties.ContainsKey(prop))
                        allFound = false;
                }

                return allFound;
        }

        static List<Passport> Parse(string input)
        {
            var rawPassports = input.Split("\n\n");

            List<Passport> passports = new List<Passport>();

            foreach(var rawPassport in rawPassports)
            {
                passports.Add(new Passport(rawPassport));
            }

            return passports;
        }

        static string GetDetails()
        {
            //boilerplate grab info
            var uri = "https://adventofcode.com/2020/day/4/input";
            var sessionId = "";

            var webRequest = WebRequest.Create(uri) as HttpWebRequest;
            webRequest.CookieContainer = new CookieContainer();
            var cookie = new Cookie("session", sessionId);
            cookie.Domain = "adventofcode.com";

            webRequest.CookieContainer.Add(cookie);
            var responseStream = webRequest.GetResponse().GetResponseStream();
            var reader = new StreamReader(responseStream);
            return reader.ReadToEnd();
        }
    }

    public class Passport
    {

        public static string[] items = new string[]{"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid"};
        public static string[] requiredItems = new string[]{"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};
        public Dictionary<string, string> properties = new Dictionary<string, string>();
        public int byr;
        public int iyr;
        public int eyr;
        public string hgt;
        public string hcl;
        public string ecl;
        public string pid;
        public string cid;

        public Passport(string input)
        {
            var props = input.Split('\n').SelectMany(x => x.Split(' '));
            foreach(var prop in props)
            {
                var keyValue = prop.Trim().Split(':');
                properties[keyValue[0]] = keyValue[1];
            }

        }
    }
}
