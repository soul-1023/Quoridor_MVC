using System;
using System.Collections.Generic;

namespace Quoridor_MVC.Models
{
    sealed class Vertex : AbstractVertex , ICloneable
    {
        public Vertex()
        {
            IsCharacter = false;
            Edges = new List<Coords>();
        }

        public object Clone()
        {
            Vertex v = new Vertex();
            v.IsCharacter = this.IsCharacter;
            v.Edges =new List<Coords>( this.Edges);
            return v;
        }
    }
}
