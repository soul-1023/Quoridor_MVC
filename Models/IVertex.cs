using System.Collections.Generic;

namespace Quoridor_MVC.Models
{
    interface IVertex
    {
        List<Coords> Edges { get; set; }

        bool IsCharacter { get; set; }
    }
}
