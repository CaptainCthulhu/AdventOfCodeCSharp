using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;


namespace Day11
{
    public class PaintedSquare
    {
        public long color;
        public Point location;

        public PaintedSquare(long color, Instruction last)
        {
            this.color = color;
            if (last != null)
                this.location = last.location;
            else
                this.location = new Point(0, 0);
        }
    }
}
