using Quoridor_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quoridor_MVC.View
{
    class VertexLabel : Label
    {
        public Coords position { get; set; }
        public bool isCharacter { get; set; }
    }
}
