using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor_MVC
{

    sealed class Coords
    {
        public int x { get; private set; }
        public int y { get; private set; }

        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

}
