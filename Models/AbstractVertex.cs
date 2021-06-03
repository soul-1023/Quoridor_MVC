using System;
using System.Collections.Generic;

namespace Quoridor_MVC.Models
{
    abstract class AbstractVertex: ICloneable
    {
        public List<Coords> Edges { get; set; }

        public bool IsCharacter { get; protected set; }

        public object Clone()
        {
            Vertex v = new Vertex();
            v.IsCharacter = this.IsCharacter;
            v.Edges = new List<Coords>(this.Edges);
            return v;
        }

        public void ToggleIsCharacter()
        {
            IsCharacter = !IsCharacter;
        }
    }
}
