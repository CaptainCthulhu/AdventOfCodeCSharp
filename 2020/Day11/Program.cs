using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace DayEleven
{
    class Program
    {
        static char[,] originalOrder;

        static void Main()
        {
            //now do real work
            Parse(GetDetails());
            QuestionOne();
            QuestionTwo();
            Console.WriteLine("Done");
        }


        static void QuestionOne()
        {
            var lastFrameArray = Clone(originalOrder);
            var newFrameArray = Clone(lastFrameArray);

            do
            {
                lastFrameArray = Clone(newFrameArray);
                for (int y = 0; y < newFrameArray.GetLength(1); y++)
                {
                    for (int x = 0; x < newFrameArray.GetLength(0); x++)
                    {
                        UpdateSquareQuestionOne(lastFrameArray, newFrameArray, x, y);
                    }
                }
            } while (!AreTheSame(lastFrameArray, newFrameArray));



            Console.WriteLine($"Question One {CountCharacters(lastFrameArray, '#')}");
        }

        static void QuestionTwo()
        {
            var lastFrameArray = Clone(originalOrder);
            var newFrameArray = Clone(lastFrameArray);

            do
            {
                lastFrameArray = Clone(newFrameArray);
                for (int y = 0; y < newFrameArray.GetLength(1); y++)
                {
                    for (int x = 0; x < newFrameArray.GetLength(0); x++)
                    {
                        UpdateSquareQuestionTwo(lastFrameArray, newFrameArray, x, y);

                    }
                }
                Display(newFrameArray);
            } while (!AreTheSame(lastFrameArray, newFrameArray));

            Console.WriteLine($"Question Two {CountCharacters(lastFrameArray, '#')}");
        }

        static void UpdateSquareQuestionOne(char[,] originalArray, char[,] updateArray, int x, int y)
        {
            var maxX = updateArray.GetLength(0);
            var maxY = updateArray.GetLength(1);

            var character = originalArray[x, y];
            var occupied = 0;

            if (character == '#' || character == 'L')
            {
                for (int y1 = -1; y1 <= 1; y1++)
                {
                    for (int x1 = -1; x1 <= 1; x1++)
                    {
                        if (x + x1 >= 0 && x + x1 < maxX && y + y1 >= 0 && y + y1 < maxY)
                        {
                            if (x1 != 0 || y1 != 0)
                            {
                                if (originalArray[x + x1, y + y1] == '#')
                                    occupied++;
                            }
                        }
                    }
                }

                if (character == '#' && occupied >= 4)
                {
                    updateArray[x, y] = 'L';
                }
                else if (character == 'L' && occupied == 0)
                {
                    updateArray[x, y] = '#';
                }
            }
        }

        static void UpdateSquareQuestionTwo(char[,] originalArray, char[,] updateArray, int x, int y)
        {
            var maxX = updateArray.GetLength(0);
            var maxY = updateArray.GetLength(1);

            var character = originalArray[x, y];
            var occupied = 0;

            if (character == '#' || character == 'L')
            {

                //up
                var newY = (y - 1);
                while (newY >=0)
                {
                    if (originalArray[x, newY] == 'L')
                        break;
                    if (originalArray[x, newY] == '#')
                    {
                        occupied++;
                        break;
                    }
                    newY--;
                }
                //down
                newY = (y + 1);
                while (newY < maxY)
                {
                    if (originalArray[x, newY] == 'L')
                        break;
                    if (originalArray[x, newY] == '#')
                    {
                        occupied++;
                        break;
                    }
                    newY++;
                }
                //left
                var newX = (x - 1);
                while (newX >= 0)
                {
                    if (originalArray[newX, y] == 'L')
                        break;
                    if (originalArray[newX, y] == '#')
                    {
                        occupied++;
                        break;
                    }
                    newX--;
                }
                //right
                newX = (x + 1);
                while (newX < maxX)
                {
                    if (originalArray[newX, y] == 'L')
                        break;
                    if (originalArray[newX, y] == '#')
                    {
                        occupied++;
                        break;
                    }
                    newX++;
                }
                //upleft
                newY = (y - 1);
                newX = (x - 1);
                while (newX >= 0 && newY >= 0)
                {
                    if (originalArray[newX, newY] == 'L')
                        break;
                    if (originalArray[newX, newY] == '#')
                    {
                        occupied++;
                        break;
                    }
                    newX--;
                    newY--;
                }
                //upright
                newY = (y - 1);
                newX = (x + 1);
                while (newX < maxX && newY >= 0)
                {
                    if (originalArray[newX, newY] == 'L')
                        break;
                    if (originalArray[newX, newY] == '#')
                    {
                        occupied++;
                        break;
                    }
                    newX++;
                    newY--;
                }

                //downright
                newY = (y + 1);
                newX = (x + 1);
                while (newX < maxX && newY < maxY)
                {
                    if (originalArray[newX, newY] == 'L')
                        break;
                    if (originalArray[newX, newY] == '#')
                    {
                        occupied++;
                        break;
                    }
                    newX++;
                    newY++;
                }
                //downleft
                newY = (y + 1);
                newX = (x - 1);
                while (newX >= 0 && newY < maxY)
                {
                    if (originalArray[newX, newY] == 'L')
                        break;
                    if (originalArray[newX, newY] == '#')
                    {
                        occupied++;
                        break;
                    }
                    newX--;
                    newY++;
                }

                if (character == '#' && occupied >= 5)
                {
                    updateArray[x, y] = 'L';
                }
                else if (character == 'L' && occupied == 0)
                {
                    updateArray[x, y] = '#';
                }
            }
        }

        static void Display(char[,] inputArray)
        {            
            var answer = "";
            for (int y = 0; y < inputArray.GetLength(1); y++)
            {
                for (int x = 0; x < inputArray.GetLength(0); x++)
                {
                    answer += (inputArray[x, y]);
                }
                answer += "\n";
            }
            Console.Clear();
            Console.Write(answer);
            Thread.Sleep(100);
        }

        static int CountCharacters(char[,] inputArray, char find)
        {
            var answer = 0;

            for (int y = 0; y < inputArray.GetLength(1); y++)
            {
                for (int x = 0; x < inputArray.GetLength(0); x++)
                {
                    if (inputArray[x, y] == find)
                        answer++;
                }
            }

            return answer;

        }

        static char[,] Clone(char[,] inputArray)
        {
            var newArray = new char[inputArray.GetLength(0), inputArray.GetLength(1)];

            for (int y = 0; y < inputArray.GetLength(1); y++)
            {
                for (int x = 0; x < inputArray.GetLength(0); x++)
                {
                    newArray[x, y] = inputArray[x, y];
                }
            }

            return newArray;
        }

        static void Parse(string input)
        {
            var lines = input.Trim().Split('\n');
            originalOrder = new char[lines[0].Trim().Length, lines.Length];
            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y].Trim().ToCharArray();
                for (int x = 0; x < line.Length; x++)
                {
                    originalOrder[x, y] = line[x];
                }
            }
        }

        static bool AreTheSame(char[,] arrayOne, char[,] arrayTwo)
        {
            for (int y = 0; y < arrayOne.GetLength(1); y++)
            {
                for (int x = 0; x < arrayOne.GetLength(0); x++)
                {
                    if (arrayOne[x, y] != arrayTwo[x, y])
                        return false;
                }
            }
            return true;
        }

        static string GetDetails()
        {
            //boilerplate grab info
            var uri = "https://adventofcode.com/2020/day/11/input";
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
