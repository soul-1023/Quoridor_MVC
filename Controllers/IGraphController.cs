using Quoridor_MVC.Models;

namespace Quoridor_MVC.Controllers
{
    interface IGraphController
    {
        void AddLinks(Graph graph, Coords characterPostition, params Coords[] potentialPos);
        void RemoveLinks(Graph graph, params (Coords, Coords)[] coords);
    }
}
