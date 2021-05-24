using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quoridor_MVC.Controllers;
using Quoridor_MVC.Models;

namespace Quoridor_MVC.View
{
    sealed class Game
    {
        private AbstractGameActivities gameActivities = new GameActivities();

        public Game()
        {

        }
    }
}
