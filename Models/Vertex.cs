using System.Collections.Generic;

namespace Quoridor_MVC.Models
{
    class Vertex : AbstractVertex
    {
        public override List<Coords> Edges { get; set; }
        public override bool IsCharacter { get; set; }

        public Vertex()
        {
            IsCharacter = false;
            Edges = new List<Coords>();
        }
    }
}
