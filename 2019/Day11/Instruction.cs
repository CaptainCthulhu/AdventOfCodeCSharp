using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Day11
{
    public class Instruction
    {
        public Point location;
        public string facing;
        public int instruction;

        public Instruction(long color, long direction, List<PaintedSquare> paintedSquares, List<Instruction> instructions)
        {
            Instruction instruction = null;
            Point oldLocation = new Point(0, 0);
            if (instructions.Count > 0)
            {
                instruction = instructions.Last();
                oldLocation = instruction.location;
            }

            paintedSquares.Add(new PaintedSquare(color, instruction));
            facing = DetermineFacing(direction, instruction);
            location = DetermineLocation(oldLocation, facing);            
        }


        static Point DetermineLocation(Point location, string facing)
        {

            if (facing == "Up")            
                return new Point(location.X, location.Y + 1);            
            else if (facing == "Down")            
                return new Point(location.X, location.Y - 1);
            else if (facing == "Left")            
                return new Point(location.X - 1, location.Y);            
            else if (facing == "Right")            
                return new Point(location.X + 1, location.Y);
            

            return new Point(0, 0);

        }

        string DetermineFacing(long direction, Instruction previousLocation)
        {

            if (direction == 0)
            {
                if (previousLocation == null || previousLocation.facing == "Up")
                    return "Left";
                else if (previousLocation.facing == "Left")
                    return "Down";
                else if (previousLocation.facing == "Down")
                    return "Right";
                else if (previousLocation.facing == "Right")
                    return "Up";
            }
            else if (direction == 1)
            {
                if (previousLocation == null || previousLocation.facing == "Up")
                    return "Right";
                else if (previousLocation.facing == "Left")
                    return "Up";
                else if (previousLocation.facing == "Down")
                    return "Left";
                else if (previousLocation.facing == "Right")
                    return "Down";
            }

            return "Up";
        }
    }
}

