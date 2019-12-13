using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Day12
{
    class Planet
    {
        public Vector3 location;
        public Vector3 velocity;

        public Planet(int x, int y, int z)
        {
            velocity = new Vector3();
            location = new Vector3(x, y, z);
        }
    }
}
