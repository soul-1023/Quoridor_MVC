using Quoridor_MVC.Models;
using System.Collections.Generic;
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

        public void PlaceWall(
                AbstractCharacter character, 
                AbstractGraph graph, 
                ((Coords, Coords), (Coords, Coords)) linkedVertexes
            )
        {
            if(ActivitiesChecker.CanPlaceWall(graph, linkedVertexes))
            {
                LinkManager.RemoveLinks(graph, linkedVertexes.Item1, linkedVertexes.Item2);
                character.SpendWall();
            }
        }

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

        public List<Coords> getMovementOptions(AbstractGraph graph, Coords currentPos)
        {
            return graph[currentPos.x, currentPos.y].Edges;
        }

        private void setAdditionalLinks(AbstractGraph graph, Coords characterPos)
        {
            graph[characterPos.x, characterPos.y].Edges.ForEach(vertex => {
                if(graph[vertex.x, vertex.y].IsCharacter)
                {
                    //TODO: если условия прыжка не соблюдены по каким-либо причинам, то нужно найти диагонали, на которые можно прыгнуть.
                    int x = vertex.x - characterPos.x;
                    int y = vertex.y - characterPos.y;

                    if(x == 1)
                    {
                        LinkManager.AddLinks(graph, characterPos, 
                            graph[vertex.x, vertex.y]
                                .Edges
                                .Find(e => e.x == vertex.x + 1 && !graph[e.x, e.y].IsCharacter)
                        );                      
                    } 
                    else if(x == -1)
                    {
                        LinkManager.AddLinks(graph,characterPos,
                            graph[vertex.x, vertex.y].Edges
                                .Find(e => e.x == vertex.x - 1 && !graph[e.x, e.y].IsCharacter)
                        );
                    } 
                    else if(y == 1)
                    {
                        LinkManager.AddLinks(graph, characterPos,
                            graph[vertex.x, vertex.y].Edges
                                .Find(e => e.y == vertex.y + 1 && !graph[e.x, e.y].IsCharacter)
                        );
                    } 
                    else if(y == -1)
                    {
                        LinkManager.AddLinks(graph, characterPos,
                            graph[vertex.x, vertex.y].Edges
                                .Find(e => e.y == vertex.y - 1 && !graph[e.x, e.y].IsCharacter)
                        );
                    }

                    
                }
            });
        }
    }
}
