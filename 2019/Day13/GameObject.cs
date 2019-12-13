using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Day13
{
    class GameObject
    {
        public enum GameObjectType
        {
            empty = 0,
            wall = 1,
            block = 2,
            horizontalPaddle = 3,
            ball = 4
        }

        public GameObjectType Type;
        public Point Location;
        public int Score;

        public GameObject(int x, int y, int type)
        {
            if (x == -1 && y == 0)
            {
                Score = type;
                Location = new Point(x, y);
            }
            else
            {
                Type = (GameObjectType)type;
                Location = new Point(x, y);
            }
        }

        public bool IsScore()
        {
            return Location.X == -1 && Location.Y == 0;
        }


    }
}
