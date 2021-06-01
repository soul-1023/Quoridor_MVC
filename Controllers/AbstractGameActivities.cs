﻿using Quoridor_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Quoridor_MVC.Controllers
{
    abstract class AbstractGameActivities
    {
        GraphController LinkManager { get; set; }
        IActivitiesChecker ActivitiesChecker { get; set; }
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

            while  (GetActiveCharacter() is AbstractAI)
            {
                //расстановка доп. связей для бота
                setAdditionalLinks(GetActiveCharacter().CurrentPosition);
                Thread.Sleep(INTERVAL);

                // действие ИИ от Паши
                //

                List<Coords> Edges = Graph[GetActiveCharacter().CurrentPosition.x, GetActiveCharacter().CurrentPosition.y].Edges;
                int range = Edges.Count;
                Random rand = new Random();
                MoveCharacter(GetActiveCharacter().CurrentPosition, Edges[rand.Next(0, range - 1)]);
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
                averageTopSide = averageRightSide = graphRowSize / 2;
                averageBottomSide = averageLeftSide = (graphRowSize / 2) + 1;
            } else
            {
                averageTopSide = averageRightSide = (int)Math.Ceiling(graphRowSize / 2.0);
                averageBottomSide = averageLeftSide = (int)Math.Floor(graphRowSize / 2.0);
            }

            startPos.Add("Top", new Coords(averageTopSide, 0));
            startPos.Add("Bottom", new Coords(averageBottomSide, graphRowSize - 1));
            startPos.Add("Right", new Coords(graphRowSize - 1, averageRightSide));
            startPos.Add("Left", new Coords(0, averageLeftSide));

            return startPos;
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
            if (
                    ActivitiesChecker.CanPlaceWall(Graph, linkedVertexes, 
                    CharactersManager.Characters, CharactersManager.WinPositions)
                )
            {
                LinkManager.RemoveLinks(Graph, linkedVertexes.Item1, linkedVertexes.Item2);
                character.SpendWall();
                CharactersManager.SwitchTurn();
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
                //Убрать временные связи
                LinkManager.ResertVertexEdges(Graph, characterPosition);

                CharactersManager.SwitchTurn();
            }

        }

        public List<Coords> GetMovementOptions(Coords currentPos)
        {
            return Graph[currentPos.x, currentPos.y].Edges;
        }

        private void setAdditionalLinks(Coords characterPos)
        {
            Coords searchVertexIsBehindOpponent(Coords vertexWithOpponent, (int x, int y) directionToJump) {
                return Graph[vertexWithOpponent.x, vertexWithOpponent.y].Edges.Find(e => {
                    if (Graph[e.x, e.y].IsCharacter == false)
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
                return Graph[vertexWithOpponent.x, vertexWithOpponent.y].Edges.Where(e =>
                            Graph[e.x, e.y].IsCharacter == false
                        ).ToArray();
            }

            List<Coords> posibleChanges = new List<Coords>();

            foreach (Coords coords in Graph[characterPos.x, characterPos.y].Edges)
            {
                if (Graph[coords.x, coords.y].IsCharacter)
                {
                    Coords AnotherCharacter = coords;
                    var directionToJump = (
                        x: AnotherCharacter.x - characterPos.x,
                        y: AnotherCharacter.y - characterPos.y
                    );
                    Coords vertexIsBehindOpponent = searchVertexIsBehindOpponent(AnotherCharacter, directionToJump);

                    if (vertexIsBehindOpponent != null)
                    {
                        vertexIsBehindOpponent.IsTemporary = true;
                        posibleChanges.Add(vertexIsBehindOpponent);
                    }
                    else
                    {
                        List<Coords> vertexesForDiagonalJump = new List<Coords>();

                        foreach (var vertex in searchVertexBySidesFromOpponent(AnotherCharacter))
                        {
                            vertex.IsTemporary = true;
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
