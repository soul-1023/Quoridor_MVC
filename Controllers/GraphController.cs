using Quoridor_MVC.Models;
using System.Linq;

namespace Quoridor_MVC.Controllers
{
    class GraphController : IGraphController
    {
        public void AddLinks(Graph graph, Coords characterPostition, params Coords[] potentialPos)
        {
            foreach (Coords coords in potentialPos)
            {
                graph[characterPostition.x, characterPostition.y].Edges.Add(coords);
                graph[coords.x, coords.y].Edges.Add(characterPostition);
            }
        }

        public void RemoveLinks(Graph graph, params (Coords, Coords)[] linkedCoords)
        {
            foreach ((Coords, Coords) coordsPair in linkedCoords)
            {
                graph[coordsPair.Item1.x, coordsPair.Item1.y].Edges.Remove(coordsPair.Item2);
                graph[coordsPair.Item2.x, coordsPair.Item2.y].Edges.Remove(coordsPair.Item1);
            }
        }
    }
}