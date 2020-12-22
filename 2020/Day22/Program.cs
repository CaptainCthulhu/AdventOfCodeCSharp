using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day22
{
    class Program
    {
        static readonly bool Testing = false;
        static readonly bool Debug = false;

        static readonly bool WriteToFile = false;
        static string Input;

        static Queue<int> Player1Deck;
        static Queue<int> Player2Deck;

        static void Main()
        {
            GetDetails();
            Parse();
            QuestionOne();
            Parse();
            QuestionTwo();
            Console.WriteLine("Done");
        }

        static void QuestionOne()
        {
            int answer = 0;

            while (Player1Deck.Count > 0 && Player2Deck.Count > 0)
            {
                var player1Card = Player1Deck.Dequeue();
                var player2Card = Player2Deck.Dequeue();

                if (player1Card > player2Card)
                {
                    Player1Deck.Enqueue(player1Card);
                    Player1Deck.Enqueue(player2Card);
                }
                else
                {
                    Player2Deck.Enqueue(player2Card);
                    Player2Deck.Enqueue(player1Card);
                }
            }
            var lastDeck = Player1Deck.Count > Player2Deck.Count ? Player1Deck : Player2Deck;

            while (lastDeck.Count > 0)
                answer += lastDeck.Count * lastDeck.Dequeue();

            Console.WriteLine($"Question One: {answer}");
        }

        static void QuestionTwo()
        {
            int answer = 0;
            var returnNumber  = RecursiveCombat(Player1Deck, Player2Deck, 1);

            var lastDeck = returnNumber == 1 ? Player1Deck : Player2Deck;

            Console.WriteLine($"\nPlayer 1's deck: {String.Join(", ", Player1Deck.ToList())}");
            Console.WriteLine($"Player 2's deck: {String.Join(", ", Player2Deck.ToList())}");

            while (lastDeck.Count > 0)
                answer += lastDeck.Count * lastDeck.Dequeue();

            Console.WriteLine($"Question Two: {answer}");
        }

        static int RecursiveCombat(Queue<int> player1Deck, Queue<int> player2Deck, int gameNumber)
        {
            Log("\n=== NEW GAME ===");

            HashSet<List<int>> player1Decks = new HashSet<List<int>>();
            HashSet<List<int>> player2Decks = new HashSet<List<int>>();

            var round = 0;

            while (player1Deck.Count > 0 && player2Deck.Count > 0)
            {
                var returnValue = 0;
                round++;
                Log($"\n-- Round {round} (Game {gameNumber}) --");
                if (SameHands(player1Deck, player1Decks.ToList(), player2Deck, player2Decks.ToList()))
                {
                    Log($"We've done this before...");
                    return 1;
                }

                player1Decks.Add(player1Deck.ToList());
                player2Decks.Add(player1Deck.ToList());

                Log($"Player 1's deck: {String.Join(", ", player1Deck.ToList())}\nPlayer 2's deck: {String.Join(", ", player2Deck.ToList())}");

                var player1Card = player1Deck.Dequeue();
                var player2Card = player2Deck.Dequeue();

                Log($"Player 1 plays: {player1Card}\nPlayer 2 plays: {player2Card}");

                if (player1Card <= player1Deck.Count && player2Card <= player2Deck.Count)
                {
                    Log("Playing a sub-game to determine the winner...");
                    returnValue = RecursiveCombat(Clone(player1Deck, player1Card), Clone(player2Deck, player2Card), gameNumber + 1);
                    Log($"...anyway, back to game {gameNumber}.");
                }


                if (returnValue != 0)
                {
                    if (returnValue == 1)
                    {
                        Log($"Player 1 wins round {round} of game {gameNumber}!");
                        player1Deck.Enqueue(player1Card);
                        player1Deck.Enqueue(player2Card);
                    }
                    else
                    {
                        Log($"Player 2 wins round {round} of game {gameNumber}!");
                        player2Deck.Enqueue(player2Card);
                        player2Deck.Enqueue(player1Card);
                    }
                }
                else if (player1Card > player2Card)
                {
                    Log($"Player 1 wins round {round} of game {gameNumber}!");
                    player1Deck.Enqueue(player1Card);
                    player1Deck.Enqueue(player2Card);
                }
                else
                {
                    Log($"Player 2 wins round {round} of game {gameNumber}!");
                    player2Deck.Enqueue(player2Card);
                    player2Deck.Enqueue(player1Card);
                }
            }

            Log($"The Winner of game {gameNumber} is player {(player1Deck.Count > player2Deck.Count ? 1 : 2)}!");

            return player1Deck.Count > player2Deck.Count ? 1 : 2;
        }
        static Queue<int> Clone(Queue<int> Original, int toClone = 0)
        {
            if (toClone != 0)
                return new Queue<int>(Original.ToArray().Take(toClone));
            else
                return new Queue<int>(Original.ToArray());
        }

        static bool SameHands(Queue<int> deck1, List<List<int>> deck1Decks, Queue<int> deck2, List<List<int>> deck2Decks)
        {
            return deck1Decks.Any(x => x.SequenceEqual(deck1)) || deck2Decks.Any(y => y.SequenceEqual(deck2));
        }


        public static void Log(string s)
        {
            if (Debug || Testing)
            {
                Console.WriteLine(s);
            }
            if (WriteToFile)
            {
                using (StreamWriter sw = File.AppendText("debug.txt"))
                {
                    sw.WriteLine(s);
                }
            }
        }

        static void Parse()
        {
            Player1Deck = new Queue<int>();
            Player2Deck = new Queue<int>();

            var elements = Input.Split("\n\n");
            foreach (var i in elements[0].Split('\n'))
            {
                if (!i.Contains("Player") && !String.IsNullOrWhiteSpace(i))
                    Player1Deck.Enqueue(Convert.ToInt32(i));
            }

            foreach (var i in elements[1].Split('\n'))
            {
                if (!i.Contains("Player") && !String.IsNullOrWhiteSpace(i))
                    Player2Deck.Enqueue(Convert.ToInt32(i));
            }
        }

        static void GetDetails()
        {

            string input;
            //code with known good solutions.
            if (Testing)
                input = @"Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10".Replace("\r", "");
            else
            {
                if (!File.Exists("input.txt"))
                {
                    //boilerplate grab info
                    var uri = "https://adventofcode.com/2020/day/22/input";
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