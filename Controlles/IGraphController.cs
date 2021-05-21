

namespace Quoridor_MVC.Models
{
    interface IGraphController
    {
        bool AddLinks(AbstractGraph graph, params Coords[] coords);
        bool RemoveLinks(AbstractGraph graph, params Coords[] coords);
    }
}
