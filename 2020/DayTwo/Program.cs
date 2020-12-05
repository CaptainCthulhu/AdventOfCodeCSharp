using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DayTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            //now do real work
            var items = GetDetails().Trim().Split("\n");
            List<PasswordDetail> passwordDetails = new List<PasswordDetail>();
            foreach (var item in items)
            {
                if (!String.IsNullOrWhiteSpace(item))
                {
                    passwordDetails.Add(new PasswordDetail(item));
                }
            }

            QuestionOne(passwordDetails);
            QuestionTwo(passwordDetails);

            
            Console.WriteLine("Done");
        }       

        static void QuestionOne(List<PasswordDetail> passwordDetails) 
        {
            var total = 0;
            foreach(var passwordDetail in passwordDetails)
            {
                var count = passwordDetail.Password.Count(x => x == passwordDetail.Character);

                if (count <= passwordDetail.HighRange && count >= passwordDetail.LowRange)
                    total++;
            }

            Console.WriteLine($"Question One: {total}");
        }

        static void QuestionTwo(List<PasswordDetail> passwordDetails) 
        {
            var total = 0;
            foreach(var passwordDetail in passwordDetails)
            {
                var firstChar = passwordDetail.Password[passwordDetail.LowRange - 1];
                var secondChar = passwordDetail.Password[passwordDetail.HighRange - 1];

                if (firstChar != secondChar && (firstChar == passwordDetail.Character || secondChar == passwordDetail.Character))                
                    total++;
            }

            Console.WriteLine($"Question Two: {total}");
        }

        static string GetDetails()
        {
            //boilerplate grab info
            var uri = "https://adventofcode.com/2020/day/2/input";
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

    public class PasswordDetail
    {
        public int HighRange;
        public int LowRange;
        public char Character;
        public string Password;

        public PasswordDetail(string input)
        {
            var splitString = input.Split(':');
            this.Password = splitString[1].Trim();
            splitString = splitString[0].Split(' ');
            this.Character = Convert.ToChar(splitString[1].Trim());
            splitString = splitString[0].Split('-');
            this.LowRange = Convert.ToInt32(splitString[0].Trim());
            this.HighRange = Convert.ToInt32(splitString[1].Trim());
        }
    };
}
