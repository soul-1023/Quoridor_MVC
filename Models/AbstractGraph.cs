namespace Quoridor_MVC.Models
{
    abstract class AbstractGraph
    {
        public AbstractVertex[,] Vertexes { get; set; }

        protected abstract void FillGraph(int size);

        public abstract AbstractVertex this[int x, int y] { get; protected set; }
    }
}
