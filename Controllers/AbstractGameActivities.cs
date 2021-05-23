using Quoridor_MVC.Models;
using System.Linq;

namespace Quoridor_MVC.Controllers
{
    abstract class AbstractGameActivities
    {
        IGraphController LinkManager { get; set; }

        IActivitiesChecker ActivitiesChecker { get; set; }

        AbstractCharactersManager CharactersManager { get; set; }

        public AbstractGameActivities()
        {
            LinkManager = new GraphController();
            ActivitiesChecker = new ActivitiesChecker();
            CharactersManager = new CharactersManager();
        }

        public abstract bool PlaceWall();

        public void MoveCharacter(AbstractGraph graph, Coords characterPosition, Coords chosenPosition)
        {
            if(ActivitiesChecker.CanMove(graph, characterPosition, chosenPosition))
            {
                CharactersManager.Characters
                    .Where(character => character.CurrentPosition == characterPosition)
                    .First()
                    .Move(chosenPosition);

                graph[characterPosition.x, characterPosition.y].ToggleIsCharacter();
                graph[chosenPosition.x, chosenPosition.y].ToggleIsCharacter();
            }
        }
    }
}
