using Quoridor_MVC.Models;

namespace Quoridor_MVC.Controllers
{
    interface IActivitiesChecker
    {
        bool CanMove(AbstractGraph graph, params Coords[] coords);

        bool CanPlaceWall(AbstractGraph graph, params Coords[] coords);

        // определиться с аргументами
        AbstractCharacter DefineWinner();
    }
}