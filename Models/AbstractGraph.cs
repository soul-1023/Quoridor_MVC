using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor_MVC.Models
{
    abstract class AbstractGraph
    {
        public abstract Vertex[,] Vertexes { get; set; }

        protected abstract void FillGraph(int size);
    }
}
