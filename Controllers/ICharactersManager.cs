using Quoridor_MVC.Models;

namespace Quoridor_MVC.Controllers
{
    abstract class ICharactersManager
    {
        public AbstractCharacter[] Characters { get; protected set; }

        public abstract void MixPlayers();

        protected abstract ICharactersManager SwitchTurn();

        protected abstract AbstractCharacter GetActiveCharacter();

        protected abstract bool MoveCharacter(AbstractGraph graph, params Coords[] coords);

        protected abstract bool CreatePlayer(string name, Coords startPosition);

        protected abstract bool CreateAI(string name, Coords startPosition);
    }
}
