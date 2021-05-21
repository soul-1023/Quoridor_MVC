using System.Collections.Generic;

namespace Quoridor_MVC.Models
{
    sealed class Vertex : AbstractVertex
    {
        public new List<Coords> Edges { get; set; }

        public new bool IsCharacter { get; set; }

        public Vertex()
        {
            IsCharacter = false;
            Edges = new List<Coords>();
        }
    }
}
