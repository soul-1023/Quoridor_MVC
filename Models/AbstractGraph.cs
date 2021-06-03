using System;

namespace Quoridor_MVC.Models
{
    abstract class AbstractGraph: ICloneable
    {
        public AbstractVertex[,] Vertexes { get; set; }

        protected abstract void FillGraph(int size);

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public abstract AbstractVertex this[int x, int y] { get; protected set; }
    }
}
