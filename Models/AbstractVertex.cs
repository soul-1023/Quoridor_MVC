using System.Collections.Generic;

namespace Quoridor_MVC.Models
{
    abstract class AbstractVertex
    {
        public List<Coords> Edges { get; set; }

        public bool IsCharacter { get; protected set; }

        public void ToggleIsCharacter()
        {
            IsCharacter = !IsCharacter;
        }
    }
}
