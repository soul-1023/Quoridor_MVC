using Quoridor_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quoridor_MVC.Controllers
{
    abstract class AbstractCharactersManager
    {
        public AbstractCharacter[] Characters { get; protected set; }

        public Dictionary<AbstractCharacter, Coords[]> WinPositions;

        protected Coords[] SetWinningSide(Coords startPositionOfCharacter, int sizeOfGraphSide)
        {
            Coords[] coords = new Coords[sizeOfGraphSide];
            bool isTopSide = startPositionOfCharacter.y == 0;
            bool isRightSide = startPositionOfCharacter.x == sizeOfGraphSide - 1;
            bool isBottomSide = startPositionOfCharacter.y == sizeOfGraphSide - 1;
            bool isLeftSide = startPositionOfCharacter.x == 0;

            if (isTopSide)
            {
                return coords.Select((_, i) => new Coords(i, sizeOfGraphSide - 1)).ToArray();
            } 
            else if (isRightSide)
            {
                return coords.Select((_, i) => new Coords(0, i)).ToArray();
            } 
            else if (isBottomSide)
            {
                return coords.Select((_, i) => new Coords(i, 0)).ToArray();
            } 
            else if (isLeftSide)
            {
                return coords.Select((_, i) => new Coords(sizeOfGraphSide - 1, i)).ToArray();
            }

            return null;
        }

        public void MixPlayers()
        {
            
        }

        protected abstract AbstractCharactersManager SwitchTurn();

        protected abstract AbstractCharacter GetActiveCharacter();

        protected abstract bool MoveCharacter(AbstractGraph graph, params Coords[] coords);

        protected abstract bool CreatePlayer(string name, Coords startPosition);

        protected abstract bool CreateAI(string name, Coords startPosition);
    }
}
