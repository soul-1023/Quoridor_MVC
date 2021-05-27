using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quoridor_MVC.Controllers;
using Quoridor_MVC.Models;

namespace Quoridor_MVC.View
{
    sealed class GameRouter
    {
        delegate void MoveHandler( (Coords from, Coords to) direction );
        delegate void ObstacleHandler( 
                (
                    (Coords, Coords) firstLink, 
                    (Coords, Coords) secondLink
                ) linkedCells
            );
        delegate void AI_Handler();

        AbstractGameActivities gameActivities = new GameActivities();

        public GameRouter(TableLayoutPanel field, int sizeOfField, int countOfCharacters)
        {
            gameActivities.InitializeSession(sizeOfField, countOfCharacters);
            AbstractCharacter character = gameActivities.GetActiveCharacter();
            var winPositions = gameActivities.getWinPositions(character);
        }





    }
}
