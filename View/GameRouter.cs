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
        AbstractGameActivities gameActivities;

        public GameRouter(TableLayoutPanel field, int sizeOfField, int countOfCharacters)
        {
            gameActivities = new GameActivities();
            gameActivities.InitializeSession(sizeOfField, countOfCharacters);
        }

        public void HandleAction( (Coords from, Coords to) direction )
        {
            gameActivities.MoveCharacter(direction.from, direction.to);

            gameActivities.FinishGame();
            gameActivities.ActionOfAI();
        }

        public void HandleAction( ( (Coords, Coords) firstLink, (Coords, Coords) secondLink ) cells )
        {
            gameActivities.PlaceWall(
                    gameActivities.GetActiveCharacter(),
                    cells
                );

            gameActivities.ActionOfAI();
        }
    }
}
