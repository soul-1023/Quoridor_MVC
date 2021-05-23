using Quoridor_MVC.Models;

namespace Quoridor_MVC.Controllers
{
    interface IGraphController
    {
        bool AddLinks(AbstractGraph graph, Coords characterPosition, params Coords[] potentialPositions);

        bool RemoveLinks(AbstractGraph graph, params (Coords, Coords)[] coords);
    }
}
