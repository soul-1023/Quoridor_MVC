using System.Collections.Generic;

namespace Quoridor_MVC.Models
{
    abstract class AbstractVertex
    {
        public abstract List<Coords> Edges { get; set; }
        public abstract bool IsCharacter { get; set; }
    }
}
