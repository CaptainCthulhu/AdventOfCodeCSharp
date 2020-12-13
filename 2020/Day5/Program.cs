using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DayFive
{
    class Program
    {
        static void Main(string[] args)
        {
            //now do real work
            var information = Parse(GetDetails());       

            QuestionOne(information);
            QuestionTwo(information);            
            Console.WriteLine("Done");
        }       

        static void QuestionOne(List<Seat> seats) 
        {            
            var max = seats.Max(x => x.SeatId);
            Console.WriteLine($"Question One:{max}");
        }

        static void QuestionTwo(List<Seat> seats) 
        {
            var missingSeats = new List<int>();
            int answer = 0;
            foreach (var i in Enumerable.Range(0, 991))
            {
                if (!seats.Any(x => x.SeatId == i))
                {
                    missingSeats.Add(i);
                    if(i != 1 && i != -1 && !missingSeats.Any(x => x == i - 1))
                    {
                        Console.WriteLine($"Question Two:{i}");
                    }
                }
            }
        }

        static List<Seat> Parse(string input)
        {
            var seats = new List<Seat>();
            foreach(var line in input.Split('\n'))
            {
                seats.Add(new Seat(line.Trim()));
            }

            return seats;
        }

        static string GetDetails()
        {
            //boilerplate grab info
            var uri = "https://adventofcode.com/2020/day/5/input";
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

    class Seat
    {
        public int Row;
        public int Column;
        public int SeatId;

        public Seat(string seatLocation)
        {        
            var validColumns = Enumerable.Range(0, 8);
            var validRows = Enumerable.Range(0, 128);
            foreach(char instruction in seatLocation)
            {
                

                if(instruction == 'F')
                {
                    validRows = validRows.Take((int)Math.Ceiling(validRows.Count() / 2m));
                }
                else if (instruction == 'B')
                {
                    validRows = validRows.Skip((int)Math.Ceiling(validRows.Count() / 2m));
                }
                else if (instruction == 'L')
                {
                    validColumns = validColumns.Take((int)Math.Ceiling(validColumns.Count() / 2m));
                }
                else if (instruction == 'R')
                {
                    validColumns = validColumns.Skip((int)Math.Ceiling(validColumns.Count() / 2m));
                }
            }

            Row = validRows.First();
            Column = validColumns.First();
            SeatId = Row * 8 + Column;
        }
    }
}
