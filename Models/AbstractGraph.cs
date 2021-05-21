namespace Quoridor_MVC.Models
{
    abstract class AbstractGraph
    {
        public Vertex[,] Vertexes { get; set; }

        protected abstract void FillGraph(int size);
    }
}
