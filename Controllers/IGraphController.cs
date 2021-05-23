using Quoridor_MVC.Models;

namespace Quoridor_MVC.Controllers
{
    interface IGraphController
    {
        void AddLinks(AbstractGraph graph, Coords characterPostition, params Coords[] potentialPos);
        void RemoveLinks(AbstractGraph graph, params (Coords, Coords)[] coords);
    }
}
