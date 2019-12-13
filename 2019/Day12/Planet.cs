using System;
using System.Collections.Generic;
namespace Day12
{
    class Planet
    {
        public struct Vector
        {
            public int Y;
            public int X;
            public int Z;

            public Vector(int x, int y, int z)
            {
                Y = y;
                X = x;
                Z = z;
            }
        }

        public Vector velocity;
        public Vector location;

        public Planet(int x, int y, int z)
        {
            velocity = new Vector();
            location = new Vector(x, y, z);
        }

        public void Update(List<Planet> planets)
        {
            foreach (Planet p in planets)
            {
                velocity.X += p.location.X > location.X ? 1 : p.location.X < location.X ? -1 : 0;
                velocity.Y += p.location.Y > location.Y ? 1 : p.location.Y < location.Y ? -1 : 0;
                velocity.Z += p.location.Z > location.Z ? 1 : p.location.Z < location.Z ? -1 : 0;
            }
        }
        public void Move()
        {
            location.X += velocity.X;
            location.Y += velocity.Y;
            location.Z += velocity.Z;
        }

        public int Energy()
        {
            return
                (Math.Abs(location.X) + Math.Abs(location.Y) + Math.Abs(location.Z)) *
                (Math.Abs(velocity.X) + Math.Abs(velocity.Y) + Math.Abs(velocity.Z));
        }

        public static bool Equivalent(Planet one, Planet two)
        {
            return one.location.X == two.location.X
                && one.location.Y == two.location.Y
                && one.location.Z == two.location.Z;
        }

        public static bool checkAxis(int positionOne, int positionTwo, int velocityOne, int velocityTwo)
        {
            return positionOne == positionTwo && velocityOne == velocityTwo;
        }
    }
}
