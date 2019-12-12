using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows;

namespace Day10
{
    public class TargetDetails
    {
        public int Distance;
        public Point Location;
        public Point Vector;
        public decimal Degrees;

        public static TargetDetails CreateTarget(Point shooter, Point target)
        {
            return new TargetDetails(shooter, target);
        }


        public TargetDetails(Point shooter, Point target)
        {
            Location = target;
            DetermineVector(shooter, target);
            DetermineDistance(shooter, target);
            DetermineDegrees();

        }

        public void DetermineDegrees()
        {
            Degrees = Math.Round((decimal)Math.Atan2(Vector.Y, Vector.X) * (decimal)(180 / Math.PI), 1);

            while (Degrees < 0)
                Degrees += 360;

            while (Degrees > 360)
                Degrees -= 360;
        }

        public void DetermineDistance(Point shooter, Point target)
        {
            Distance = Math.Abs(target.X - shooter.X) + Math.Abs(target.Y - shooter.Y);
        }

        public void DetermineVector(Point shooter, Point target)
        {
            var vector = new Point(target.X - shooter.X, target.Y - shooter.Y);
            if (vector.X == 0 || vector.Y == 0)
            {
                vector.X = vector.X == 0 ? 0 : vector.X / Math.Abs(vector.X);
                vector.Y = vector.Y == 0 ? 0 : vector.Y / Math.Abs(vector.Y);
            }
            else
            {
                var gcd = GCD(vector.X, vector.Y);
                vector = new Point(vector.X / gcd, vector.Y / gcd);
            }

            this.Vector = vector;
        }

        private static int GCD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            if (b == 0)
                return 0;

            // Pull out remainders.
            while (true)
            {
                int remainder = a % b;
                if (remainder == 0) return b;
                a = b;
                b = remainder;
            };
        }

    }
}
