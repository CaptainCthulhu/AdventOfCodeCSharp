using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Day13
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
            Interop.OpCodeMachine(Input.CloneInputs());
            List<GameObject> objects = new List<GameObject>();

            while (Interop.Outputs.Count > 3)
            {
                var gameObject = new GameObject((int)Interop.Outputs.Dequeue(), (int)Interop.Outputs.Dequeue(), (int)Interop.Outputs.Dequeue());
                objects.Add(gameObject);
            }

            Console.WriteLine($"Q1: {objects.Count(x => x.Type == GameObject.GameObjectType.block)}");

        }

        static readonly Interop q2Interop = new Interop();
        static List<GameObject> Q2Objects = new List<GameObject>();

        static private void Q2()
        {
            List<long> input = Input.CloneInputs();
            input[0] = 2;

            Interop.OpCodeMachine(input);
            ParseInputs();

            Console.WriteLine($"Q2: {Q2Objects.Where(x => x.IsScore).LastOrDefault().Score}");

        }

        static public void ParseInputs()
        {
            while (Interop.Outputs.Count >= 3)
            {
                var gameObject = new GameObject((int)Interop.Outputs.Dequeue(), (int)Interop.Outputs.Dequeue(), (int)Interop.Outputs.Dequeue());
                Q2Objects.Add(gameObject);
            }
        }

        static public int GetInput()
        {
            ParseInputs();
            GameObject paddle = Q2Objects.Where(x => x.Type == GameObject.GameObjectType.horizontalPaddle).LastOrDefault();
            GameObject ball = Q2Objects.Where(x => x.Type == GameObject.GameObjectType.ball).LastOrDefault();

            if (ball.Location.X < paddle.Location.X)
                return -1;
            else if (ball.Location.X > paddle.Location.X)
                return 1;
            else
                return 0;
        }

    }
}
