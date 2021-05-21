using System.Collections.Generic;

namespace Quoridor_MVC.Models
{
    sealed class Vertex : AbstractVertex
    {
        public Vertex()
        {
            IsCharacter = false;
            Edges = new List<Coords>();
        }
    }
}
