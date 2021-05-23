using System;
using System.Linq;

namespace Quoridor_MVC.Models
{
    sealed class Graph : AbstractGraph
    {
        public Graph(int size)
        {
            Vertexes = new Vertex[size, size];

            FillGraph(size);
        }

        protected override void FillGraph(int size)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    this[y, x] = new Vertex();
                    SetVertexEdges(y, x, size);
                }
            }
        }

        private void SetVertexEdges(int y, int x, int size)
        {
            Coords TopElem = x != 0 ? new Coords(x - 1, y) : null;
            Coords DownElem = x != size - 1 ? new Coords(x + 1, y) : null;
            Coords LeftElem = y != 0 ? new Coords(x, y - 1) : null;
            Coords RightElem = y != size - 1 ? new Coords(x, y + 1) : null;
            Coords[] AroundElems = { TopElem, DownElem, LeftElem, RightElem };

            AroundElems.Where(e => e != null).ToList().ForEach(e => this[y, x].Edges.Add(e));
        }

        public override AbstractVertex this[int y, int x]
        {
            get => Vertexes[y, x];
            protected set => Vertexes[y, x] = value;
        }
    }
}