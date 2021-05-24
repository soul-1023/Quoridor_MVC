using Quoridor_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quoridor_MVC.Controllers
{
    abstract class AbstractGameActivities
    {
        IGraphController LinkManager { get; set; }

        IActivitiesChecker ActivitiesChecker { get; set; }

        AbstractCharactersManager CharactersManager { get; set; }

        public AbstractGraph Graph { get; set; }

        public AbstractGameActivities()
        {
            LinkManager = new GraphController();
            ActivitiesChecker = new ActivitiesChecker();
            CharactersManager = new CharactersManager();
        }
        private Dictionary<string, Coords> GetStartCoords(int graphRowSize)
        {
            Dictionary<string, Coords> startPos = new Dictionary<string, Coords>();
            int averageTopSide,
                averageBottomSide,
                averageRightSide,
                averageLeftSide;

            if (graphRowSize % 2 == 0)
            {
                averageTopSide = averageRightSide = graphRowSize / 2;
                averageBottomSide = averageLeftSide = (graphRowSize / 2) + 1;
            } else
            {
                averageTopSide = averageRightSide = (int)Math.Ceiling((decimal)(graphRowSize / 2));
                averageBottomSide = averageLeftSide = (int)Math.Floor((decimal)(graphRowSize / 2));
            }

            startPos.Add("Top", new Coords(averageTopSide, 0));
            startPos.Add("Bottom", new Coords(averageBottomSide, graphRowSize - 1));
            startPos.Add("Right", new Coords(graphRowSize - 1, averageRightSide));
            startPos.Add("Left", new Coords(0, averageLeftSide));

            return startPos;
        }
       
        public void InitializeSession(int size, int charactersQuantity)
        {
            Graph = new Graph(size);
            FillByCharacters(charactersQuantity, GetStartCoords(size));
            CharactersManager.MixPlayers();
        }

        public AbstractCharacter GetActiveCharacter() => CharactersManager.Characters[0];

        private void FillByCharacters(int charactersQuantity, Dictionary<string, Coords> startCoords)
        {
            if(charactersQuantity == 2)
            {
                CharactersManager.CreatePlayer("name", startCoords["Bottom"]);
                CharactersManager.CreateAI("AI", startCoords["Top"]);
            } 
            else if(charactersQuantity == 4)
            {
                CharactersManager.CreatePlayer("name", startCoords["Bottom"]);
                CharactersManager.CreateAI("AI", startCoords["Top"]);
                CharactersManager.CreateAI("AI", startCoords["Right"]);
                CharactersManager.CreateAI("AI", startCoords["Left"]);
            }
            else
            {
                throw new Exception("Неверное количство игроков. Их может быть только 2 или 4.");
            }
        }

        public void PlaceWall(AbstractCharacter character, ((Coords, Coords), (Coords, Coords)) linkedVertexes)
        {
            if(ActivitiesChecker.CanPlaceWall(Graph, linkedVertexes))
            {
                LinkManager.RemoveLinks(Graph, linkedVertexes.Item1, linkedVertexes.Item2);
                character.SpendWall();
            }
        }

        public void MoveCharacter(Coords characterPosition, Coords chosenPosition)
        {
            if(ActivitiesChecker.CanMove(Graph, characterPosition, chosenPosition))
            {
                CharactersManager.Characters
                    .Where(character => character.CurrentPosition == characterPosition)
                    .First()
                    .Move(chosenPosition);

                Graph[characterPosition.x, characterPosition.y].ToggleIsCharacter();
                Graph[chosenPosition.x, chosenPosition.y].ToggleIsCharacter();
            }
        }

        public List<Coords> GetMovementOptions(Coords currentPos)
        {
            return Graph[currentPos.x, currentPos.y].Edges;
        }

        private void setAdditionalLinks(Coords characterPos)
        {
            Graph[characterPos.x, characterPos.y].Edges.ForEach(vertex => {
                if(Graph[vertex.x, vertex.y].IsCharacter)
                {
                    //TODO: если условия прыжка не соблюдены по каким-либо причинам, то нужно найти диагонали, на которые можно прыгнуть.
                    int x = vertex.x - characterPos.x;
                    int y = vertex.y - characterPos.y;

                    if(x == 1)
                    {
                        LinkManager.AddLinks(Graph, characterPos,
                            Graph[vertex.x, vertex.y]
                                .Edges
                                .Find(e => e.x == vertex.x + 1 && !Graph[e.x, e.y].IsCharacter)
                        );                      
                    } 
                    else if(x == -1)
                    {
                        LinkManager.AddLinks(Graph, characterPos,
                            Graph[vertex.x, vertex.y].Edges
                                .Find(e => e.x == vertex.x - 1 && !Graph[e.x, e.y].IsCharacter)
                        );
                    } 
                    else if(y == 1)
                    {
                        LinkManager.AddLinks(Graph, characterPos,
                            Graph[vertex.x, vertex.y].Edges
                                .Find(e => e.y == vertex.y + 1 && !Graph[e.x, e.y].IsCharacter)
                        );
                    } 
                    else if(y == -1)
                    {
                        LinkManager.AddLinks(Graph, characterPos,
                            Graph[vertex.x, vertex.y].Edges
                                .Find(e => e.y == vertex.y - 1 && !Graph[e.x, e.y].IsCharacter)
                        );
                    }

                    
                }
            });
        }
    }
}
