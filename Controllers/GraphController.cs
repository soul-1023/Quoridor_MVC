using Quoridor_MVC.Models;
using System.Linq;
using System.Collections.Generic;
namespace Quoridor_MVC.Controllers
{
    class GraphController : IGraphController
    {
        public void AddLinks(AbstractGraph graph, Coords characterPostition, params Coords[] potentialPos)
        {
            foreach (Coords coords in potentialPos)
            {
                graph[characterPostition.y, characterPostition.x].Edges.Add(coords);
                coords.IsTemporary = true;
                graph[coords.y, coords.x].Edges.Add(characterPostition);
                characterPostition.IsTemporary = true;
            }
        }

        public void RemoveLinks(AbstractGraph graph, params (Coords, Coords)[] linkedCoords)
        {
            foreach ((Coords, Coords) coordsPair in linkedCoords)
            {
                 
                graph[coordsPair.Item1.y, coordsPair.Item1.x].Edges.Remove(graph[coordsPair.Item1.y, coordsPair.Item1.x].Edges.
                       Find(e => e.x == coordsPair.Item2.x && e.y == coordsPair.Item2.y));
                graph[coordsPair.Item2.y, coordsPair.Item2.x].Edges.Remove(graph[coordsPair.Item2.y, coordsPair.Item2.x].Edges.
                       Find(e => e.x == coordsPair.Item1.x && e.y == coordsPair.Item1.y));
            }
        }

        public void ResertVertexEdges(AbstractGraph graph, Coords vertexCoords, bool isTemporaryNeeded)
        {
            List<Coords> withTemporary = new List<Coords>();
            foreach(Coords e in graph[vertexCoords.y, vertexCoords.x].Edges)
            {
                withTemporary.Add(e);
            }

            //List<Coords> filter = new List<Coords>();
            //foreach (Coords e in graph[vertexCoords.y, vertexCoords.x].Edges)
            //{
            //    if (e.IsTemporary == false)
            //        filter.Add(e);
            //}
            //graph[vertexCoords.y, vertexCoords.x].Edges = filter;
            graph[vertexCoords.y, vertexCoords.x].Edges = graph[vertexCoords.y, vertexCoords.x]
                .Edges
                .Where(e => !e.IsTemporary)
                .ToList();
            if (isTemporaryNeeded == false)
            {
                foreach (Coords e in withTemporary)
                {
                    e.IsTemporary = false;
                }
            }
        }
    }
}