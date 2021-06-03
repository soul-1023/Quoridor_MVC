using Quoridor_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Quoridor_MVC;
namespace Quoridor_MVC.Controllers
{
    abstract class AbstractGameActivities
    {
        GraphController LinkManager { get; set; }
        public IActivitiesChecker ActivitiesChecker { get; set; }
        public AbstractCharactersManager CharactersManager { get; set; }
        public AbstractGraph Graph { get; private set; }

        public AbstractCharacter Winner
        {
            get
            {
                return ActivitiesChecker.DefineWinner(
                        GetActiveCharacter(), 
                        getWinPositions(GetActiveCharacter())
                    );
            }
        }

        public void ActionOfAI()
        {
            const int INTERVAL = 2000;
            var c = GetActiveCharacter();
            while  (GetActiveCharacter() is AbstractAI)
            {
                //расстановка доп. связей для бота
                setAdditionalLinks(GetActiveCharacter().CurrentPosition);
                //Thread.Sleep(INTERVAL);
                var Ai=GetActiveCharacter() as AI;
                Ai.Ruller = this;
                Ai.CHMO = CharactersManager;
                Ai.check = this.ActivitiesChecker as ActivitiesChecker;
               
                Ai.TestAll(Graph);

                // действие ИИ от Паши
                //
                //var a = GetActiveCharacter().CurrentPosition;
                //List<Coords> Edges = Graph[GetActiveCharacter().CurrentPosition.y, GetActiveCharacter().CurrentPosition.x].Edges;
                //int range = Edges.Count;
                //Random rand = new Random();
                ////Ошибка
                //if (GetActiveCharacter().CurrentPosition.y <= 4)
                //    MoveCharacter(GetActiveCharacter().CurrentPosition, Edges.Find(x => x.y == GetActiveCharacter().CurrentPosition.y + 1));
                //else


                //CharactersManager.SwitchTurn();

                //SwitchTurn();

            }
           
                //расстановка доп. связей для игрока
                setAdditionalLinks(GetActiveCharacter().CurrentPosition);
        }

        public void FinishGame()
        {
            if (Winner != null)
            {
                MessageBox.Show($"Игру выиграл игрок { Winner.Name }");
            }
        }

        public AbstractGameActivities()
        {
            LinkManager = new GraphController();
            ActivitiesChecker = new ActivitiesChecker();
            CharactersManager = new CharactersManager();
        }

        public void InitializeSession(int sizeOfField, int charactersQuantity)
        {
            Graph = new Graph(sizeOfField);

            FillByCharacters(charactersQuantity, GetStartCoords(sizeOfField));
            CharactersManager.MixPlayers();

            CharactersManager.Characters.ForEach(character =>
            {
                CharactersManager.SetWinningSide(character.CurrentPosition, sizeOfField);
                Graph[character.CurrentPosition.y, character.CurrentPosition.x].ToggleIsCharacter();
            });
            
        }

        public Coords[] getWinPositions(AbstractCharacter character)
        {
            return CharactersManager.WinPositions[character];
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
                averageTopSide = averageRightSide = (graphRowSize - 1) / 2;
                averageBottomSide = averageLeftSide = ((graphRowSize - 1) / 2) + 1;
            }
            else
            {
                averageTopSide = averageRightSide = (int)Math.Ceiling((decimal)(graphRowSize - 1 / 2));
                averageBottomSide = averageLeftSide = (int)Math.Floor((decimal)(graphRowSize - 1 / 2));
            }

            startPos.Add("Top", new Coords(averageTopSide, 0));
            startPos.Add("Bottom", new Coords(averageBottomSide, graphRowSize - 1));
            startPos.Add("Right", new Coords(graphRowSize - 1, averageRightSide));
            startPos.Add("Left", new Coords(0, averageLeftSide));

            return startPos;
        }
        public AbstractCharacter GetActiveCharacter() => CharactersManager.Characters[0];

        public void SwitchTurn() => CharactersManager.SwitchTurn();


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

            Graph clone = new Graph(Graph.Vertexes.GetLength(0));

            clone =( Graph as Graph).Clone() as Graph;

            LinkManager.RemoveLinks(clone, linkedVertexes.Item1, linkedVertexes.Item2);




            if (
                    ActivitiesChecker.CanPlaceWall(Graph, linkedVertexes,
                    CharactersManager.Characters, CharactersManager.WinPositions) && (ActivitiesChecker as ActivitiesChecker).CanGameProceed(clone, CharactersManager.Characters, CharactersManager.WinPositions)
                )
            {
                LinkManager.RemoveLinks(Graph, linkedVertexes.Item1, linkedVertexes.Item2);
                character.SpendWall();
                CharactersManager.SwitchTurn();
            }
          
        }

        public bool OlegChek(((Coords, Coords), (Coords, Coords)) linkedVertexes)
        {
            Graph clone = new Graph(Graph.Vertexes.GetLength(0));

            clone = (Graph as Graph).Clone() as Graph;

            LinkManager.RemoveLinks(clone, linkedVertexes.Item1, linkedVertexes.Item2);




            if (
                    ActivitiesChecker.CanPlaceWall(Graph, linkedVertexes,
                    CharactersManager.Characters, CharactersManager.WinPositions) && (ActivitiesChecker as ActivitiesChecker).CanGameProceed(clone, CharactersManager.Characters, CharactersManager.WinPositions)
                )
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public void MoveCharacter(Coords characterPosition, Coords chosenPosition)
        {
            bool check = ActivitiesChecker.CanMove(Graph, characterPosition, chosenPosition);
            if (ActivitiesChecker.CanMove(Graph, characterPosition, chosenPosition))
            {
                CharactersManager.Characters
                    .Where(character => character.CurrentPosition.x == characterPosition.x && character.CurrentPosition.y == characterPosition.y)
                    .First()
                    .Move(chosenPosition);

                Graph[characterPosition.y, characterPosition.x].ToggleIsCharacter();
                Graph[chosenPosition.y, chosenPosition.x].ToggleIsCharacter();
                //Убрать временные связи
                LinkManager.ResertVertexEdges(Graph, chosenPosition, true);

                LinkManager.ResertVertexEdges(Graph, characterPosition, false);

                FinishGame();

                CharactersManager.SwitchTurn();
            }

        }

        public List<Coords> GetMovementOptions(Coords currentPos)
        {
            return Graph[currentPos.x, currentPos.y].Edges;
        }
        //Пересмотреть метод
        private void setAdditionalLinks(Coords characterPos)
        {
            Coords searchVertexIsBehindOpponent(Coords vertexWithOpponent, (int x, int y) directionToJump) {
                return Graph[vertexWithOpponent.y, vertexWithOpponent.x].Edges.Find(e => {
                    if (Graph[e.y, e.x].IsCharacter == false)
                    {
                        if (directionToJump.x == 1) return e.x == vertexWithOpponent.x + 1;
                        else if (directionToJump.x == -1) return e.x == vertexWithOpponent.x - 1;
                        else if (directionToJump.y == 1) return e.y == vertexWithOpponent.y + 1;
                        else if (directionToJump.y == -1) return e.y == vertexWithOpponent.y - 1;
                    }

                    return false;
                });
            }

            Coords[] searchVertexBySidesFromOpponent(Coords vertexWithOpponent)
            {
                return Graph[vertexWithOpponent.y, vertexWithOpponent.x].Edges.Where(e =>
                            Graph[e.y, e.x].IsCharacter == false
                        ).ToArray();
            }

            List<Coords> posibleChanges = new List<Coords>();

            foreach (Coords coords in Graph[characterPos.y, characterPos.x].Edges)
            {
                if (Graph[coords.y, coords.x].IsCharacter)
                {
                    Coords AnotherCharacter = coords;
                    
                    var directionToJump = (
                        x: AnotherCharacter.x - characterPos.x,
                        y: AnotherCharacter.y - characterPos.y
                    );
                    Coords vertexIsBehindOpponent = searchVertexIsBehindOpponent(AnotherCharacter, directionToJump);

                    if (vertexIsBehindOpponent != null)
                    {
                        posibleChanges.Add(vertexIsBehindOpponent);
                    }
                    else
                    {
                        List<Coords> vertexesForDiagonalJump = new List<Coords>();

                        foreach (var vertex in searchVertexBySidesFromOpponent(AnotherCharacter))
                        {
                            posibleChanges.Add(vertex);
                        }
                    }
                }
            }

            if(posibleChanges != null)
            {
                LinkManager.AddLinks(Graph, characterPos, posibleChanges.ToArray());
            }
        }
    }
}
