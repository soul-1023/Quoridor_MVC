using Quoridor_MVC.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Quoridor_MVC.Controllers
{
    abstract class AbstractCharactersManager
    {
        public List<AbstractCharacter> Characters { get; protected set; }

        public Dictionary<AbstractCharacter, Coords[]> WinPositions { get; protected set; }

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

        //Протестировать
        public void MixPlayers()
        {
            Random rnd = new Random();
            int countOfCharactersInGame = Characters.Count;
            int idCurrentCharacter = 0;

            AbstractCharacter[] mixedCharacters = new AbstractCharacter[countOfCharactersInGame];

            while(mixedCharacters.Contains(null))
            {
                AbstractCharacter chooseCharacter = Characters[rnd.Next(0, countOfCharactersInGame)];

                if (!mixedCharacters.Contains(chooseCharacter))
                {
                    mixedCharacters[idCurrentCharacter] = chooseCharacter;
                    idCurrentCharacter++;
                }
                    
            }

            Characters = mixedCharacters.ToList();
        }

        public void SwitchTurn()
        {
            AbstractCharacter firstCharacter = Characters[0];

            Characters.Remove(firstCharacter);
            Characters.Add(firstCharacter);
        }

        protected AbstractCharacter GetActiveCharacter() => Characters[0];

        public void CreatePlayer(string name, Coords startPosition)
        {
            if (Characters.Count < 5)
                Characters.Add(new Player(name, startPosition));
            else
                throw new Exception("Количество игроков не может быть больше четырёх");
        }

        public void CreateAI(string name, Coords startPosition)
        {
            if (Characters.Count < 5)
                Characters.Add(new AI(name, startPosition));
            else
                throw new Exception("Количество игроков не может быть больше четырёх");
        }
    }
}
