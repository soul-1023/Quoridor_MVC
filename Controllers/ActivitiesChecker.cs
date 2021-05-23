using Quoridor_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor_MVC.Controllers
{
    class ActivitiesChecker : IActivitiesChecker
    {
        public bool CanMove(AbstractGraph graph, Coords characterPosition, Coords chosenPosition)
        {
            return SuitsConditions(graph, characterPosition, chosenPosition);
        }

        public bool CanPlaceWall(AbstractGraph graph, ((Coords, Coords), (Coords, Coords)) CoordPairs)
        {
            return SuitsConditions(graph, CoordPairs.Item1.Item1, CoordPairs.Item1.Item2)
              && SuitsConditions(graph, CoordPairs.Item1.Item2, CoordPairs.Item2.Item1)
              && SuitsConditions(graph, CoordPairs.Item2.Item1, CoordPairs.Item2.Item2);
        }

        public AbstractCharacter DefineWinner(AbstractCharacter character, Coords[] winPoints)
        {
            foreach (Coords pos in winPoints)
            {
                if (pos == character.CurrentPosition)
                    return character;
            }

            return null;
        }

        private bool SuitsConditions(AbstractGraph graph, Coords firstPosition, Coords secondPosition)
        {
            bool xNotSmall = secondPosition.x >= 0;
            bool xNotBig = secondPosition.x < graph.Vertexes.GetLength(0);
            bool yNotSmall = secondPosition.y >= 0;
            bool yNotBig = secondPosition.y < graph.Vertexes.GetLength(0);
            bool characterHasLink = graph[firstPosition.x, firstPosition.y].Edges.Contains(secondPosition);

            return (xNotSmall && xNotBig && yNotSmall && yNotBig && characterHasLink);
        }
    }
}
