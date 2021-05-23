using Quoridor_MVC.Models;

namespace Quoridor_MVC.Controllers
{
    interface IActivitiesChecker
    {
        bool CanMove(AbstractGraph graph, Coords characterPosition, Coords chosenPosition);

        bool CanPlaceWall(AbstractGraph graph, ((Coords, Coords), (Coords, Coords)) CoordPairs);

        AbstractCharacter DefineWinner(AbstractCharacter character, Coords[] winPoints);
    }
}