using Quoridor_MVC.Models;

namespace Quoridor_MVC.Controllers
{
    interface IGraphController
    {
        bool AddLinks(AbstractGraph graph, params Coords[] coords);
        bool RemoveLinks(AbstractGraph graph, params Coords[] coords);
    }
}
