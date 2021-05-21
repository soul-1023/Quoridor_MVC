namespace Quoridor_MVC.Models
{
    abstract class AbstractGraph
    {
        protected Vertex[,] Vertexes { get; set; }

        protected abstract void FillGraph(int size);
    }
}
