using Quoridor_MVC.Models;
using System.Collections.Generic;

namespace Quoridor_MVC.Controllers
{
    interface IActivitiesChecker
    {
        bool CanMove(AbstractGraph graph, Coords characterPosition, Coords chosenPosition);

        bool CanPlaceWall(AbstractGraph graph, ((Coords, Coords), (Coords, Coords)) CoordPairs,
            List<AbstractCharacter> Characters, Dictionary<AbstractCharacter, Coords[]> WinPositions);

        AbstractCharacter DefineWinner(AbstractCharacter character, Coords[] winPoints);
    }
}