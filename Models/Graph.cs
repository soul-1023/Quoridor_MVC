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
            Vertex[][] vertexes = (Vertex[][])Enumerable.Range(0, 8)
                .Select((_) => (new Vertex[8]).Select((_e) => new Vertex())).ToArray();

            //for (int y = 0; y < size; y++)
            //{
            //    for (int x = 0; x < size; x++)
            //    {
            //        Vertexes[x, y] = new Vertex();

            //        if (x != 0)
            //        {
            //            Vertexes[x, y].Edges.Add(new Coords(x - 1, y));
            //        }
            //        if (x != size - 1)
            //        {
            //            Vertexes[x, y].Edges.Add(new Coords(x + 1, y));
            //        }
            //        if (y != 0)
            //        {
            //            Vertexes[x, y].Edges.Add(new Coords(x, y - 1));
            //        }
            //        if (y != size - 1)
            //        {
            //            Vertexes[x, y].Edges.Add(new Coords(x, y + 1));
            //        }
            //    }
            //}
        }

        public IVertex this[int x, int y]
        {
            get => Vertexes[x, y];
        }
    }
}
