using Quoridor_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quoridor_MVC.View
{
    class WallLabel : Label
    {
        (Coords, Coords) Link { get; set; }
    }
}
