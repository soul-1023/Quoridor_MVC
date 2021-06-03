
using System;

namespace Quoridor_MVC.Models
{
    sealed public class Coords : ICloneable
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public bool IsTemporary { get; set; } = false;

        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public object Clone()
        {
            return new Coords(this.x, this.y);
        }
    }

}
