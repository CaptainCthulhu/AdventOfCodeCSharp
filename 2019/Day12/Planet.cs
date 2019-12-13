using System;
using System.Collections.Generic;
using System.Numerics;

namespace Day12
{
    class Planet
    {
        public Vector3 location;
        public Vector3 velocity;

        public Planet(float x, float y, float z)
        {
            velocity = new Vector3();
            location = new Vector3(x, y, z);
        }

        public void Update(List<Planet> planets)
        {
            foreach(Planet p in planets)
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
            return Convert.ToInt32(
                (Math.Abs(location.X) + Math.Abs(location.Y) + Math.Abs(location.Z)) *
                (Math.Abs(velocity.X) + Math.Abs(velocity.Y) + Math.Abs(velocity.Z)));
        }

        public static bool Equivalent(Planet one, Planet two)
        {
            return one.location.X == two.location.X
                && one.location.Y == two.location.Y
                && one.location.Z == two.location.Z;
        }
    }
}
