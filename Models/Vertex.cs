using System.Collections.Generic;

namespace Quoridor_MVC.Models
{
    sealed class Vertex : IVertex
    {
        public List<Coords> Edges { get; set; }

        public bool IsCharacter { get; set; }

        public Vertex()
        {
            IsCharacter = false;
            Edges = new List<Coords>();
        }
    }
}
