using Quoridor_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor_MVC.Controllers
{
    class CharactersManager : AbstractCharactersManager
    {
        protected override bool CreateAI(string name, Coords startPosition)
        {
            throw new NotImplementedException();
        }

        protected override bool CreatePlayer(string name, Coords startPosition)
        {
            throw new NotImplementedException();
        }

        protected override AbstractCharacter GetActiveCharacter()
        {
            throw new NotImplementedException();
        }

        protected override bool MoveCharacter(AbstractGraph graph, params Coords[] coords)
        {
            throw new NotImplementedException();
        }

        protected override AbstractCharactersManager SwitchTurn()
        {
            throw new NotImplementedException();
        }
    }
}
