using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Numerics;

namespace Day12
{
    class Program
    {
        static readonly bool testing = false;

        static readonly char[] directions = new char[] { 'N', 'E', 'S', 'W' };

        static readonly List<String> directionsList = new List<String>();

        static void Main(string[] args)
        {
            //now do real work
            Parse(GetDetails());
            QuestionOne();
            QuestionTwo();            
            Console.WriteLine("Done");
        }

        static void QuestionOne() 
        {
            var facing = 'E';
            var x = 0;
            var y = 0;

            foreach(var direction in directionsList)
            {                
                var newFacing = direction[0];
                var magnitude = Convert.ToInt32(direction.Substring(1));
                if (newFacing == 'F')
                {
                    if (facing == 'N')
                        y -= magnitude;
                    else if (facing == 'E')
                        x += magnitude;
                    else if (facing == 'S')
                        y += magnitude;
                    else if (facing == 'W')
                        x -= magnitude;                    
                }                          
                else if (directions.Contains(newFacing))
                {
                    if (newFacing == 'N')
                        y -= magnitude;
                    else if (newFacing == 'E')
                        x += magnitude;
                    else if (newFacing == 'S')
                        y += magnitude;
                    else if (newFacing == 'W')
                        x -= magnitude;                    
                }
                else
                {
                    var newFacingAdjustment = magnitude / 90;

                    if (newFacing == 'R')
                        facing = directions[(Array.IndexOf(directions, facing) + newFacingAdjustment) % directions.Length];
                    else if (newFacing == 'L')
                    {
                        newFacingAdjustment *= -1;                        
                        while (newFacingAdjustment < 0)   
                        {
                            newFacingAdjustment += directions.Length;                        
                        }                                             
                        facing = directions[(Array.IndexOf(directions, facing) +  newFacingAdjustment) % directions.Length];
                    }
                }
            }

            //DoStuff
            Console.WriteLine($"Question One:{Math.Abs(x) + Math.Abs(y)}");
        }

        static void QuestionTwo() 
        {         
            var x = 0;
            var y = 0;
            var vectorX = 10;
            var vectorY = -1;

            foreach(var directionInstructions in directionsList)
            {                
                var direction = directionInstructions[0];
                var magnitude = Convert.ToInt32(directionInstructions.Substring(1));
                if (direction == 'F')
                {                    
                    x += magnitude * vectorX;
                    y += magnitude * vectorY;
                }                          
                else if (directions.Contains(direction))
                {
                    if (direction == 'N')
                        vectorY -= magnitude;
                    else if (direction == 'E')
                        vectorX += magnitude;
                    else if (direction == 'S')
                        vectorY += magnitude;
                    else if (direction == 'W')
                        vectorX -= magnitude;                    
                }
                else
                {
                    if (direction == 'R')                      
                        magnitude = 360 - magnitude;

                    var movements = magnitude / 90;

                    for(int i = 0; i < movements; i++)
                    {
                        var tempX = vectorX;
                        vectorX = vectorY;
                        vectorY = tempX * -1;                        
                    }
                }                
            }

            //DoStuff
            Console.WriteLine($"Question Two:{Math.Abs(x) + Math.Abs(y)}");
        }

        static void Parse(string input)
        {
            var lines = input.Trim().Split('\n');
            foreach(var line in lines)
            {
                directionsList.Add(line.Trim());
            }
        }

        static string GetDetails()
        {
            if (testing)
            {
                return @"F10
N3
F7
R90
F11";
            }

            //boilerplate grab info
            var uri = "https://adventofcode.com/2020/day/12/input";
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
}
