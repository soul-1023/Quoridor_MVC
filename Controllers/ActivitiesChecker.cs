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

        public bool CanPlaceWall(AbstractGraph graph, ((Coords, Coords), (Coords, Coords)) CoordPairs,
            List<AbstractCharacter> Characters, Dictionary<AbstractCharacter, Coords[]> WinPositions)
        {
            return SuitsConditions(graph, CoordPairs.Item1.Item1, CoordPairs.Item1.Item2)
              && SuitsConditions(graph, CoordPairs.Item1.Item2, CoordPairs.Item2.Item1)
              && SuitsConditions(graph, CoordPairs.Item2.Item1, CoordPairs.Item2.Item2)
              //может ли игра продолжаться
              && CanGameProceed(graph, Characters, WinPositions);
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

        //Проверка на продолжение игры
        private bool CanGameProceed(AbstractGraph graph, List<AbstractCharacter> Characters, Dictionary<AbstractCharacter, Coords[]> WinPositions)
        {
            bool[,] checkMatrix;
            int[,] costMatrix;
            int length = graph.Vertexes.GetLength(0);
            Dictionary<AbstractCharacter, bool> charactersWithTrails = new Dictionary<AbstractCharacter, bool>();
            const int HYPOTETIC_MAX_VALUE = 1000000;
            int[,] costMatrixPattern = new int[length, length];

            bool IsVictoryPossible(Dictionary<AbstractCharacter, bool> charactersWithWays)
            {
                foreach (var character in charactersWithWays)
                {
                    if (character.Value == false)
                    {
                        return false;
                    }
                }

                return true;
            }

            void CalculateCostMatrix(List<Coords> edges, Coords minCoords, ref int[,] matrixOfCosts)
            {
                foreach (Coords edge in edges)
                {
                    if (matrixOfCosts[edge.y, edge.x] > matrixOfCosts[minCoords.y, minCoords.x] + 1)
                    {
                        matrixOfCosts[edge.y, edge.x] = matrixOfCosts[minCoords.y, minCoords.x] + 1;
                    }
                }
            }

            void SetVictoryPosibility(AbstractCharacter character, int[,] matrixOfCosts, ref Dictionary<AbstractCharacter, bool> charactersWithWays)
            {
                foreach (Coords coords in WinPositions[character])
                {
                    if (matrixOfCosts[coords.y, coords.x] < HYPOTETIC_MAX_VALUE)
                    {
                        charactersWithWays[character] = true;
                    }
                }
            }

            void FillCostMatrix(ref int[,] matrixOfCosts)
            {
                for (int y = 0; y < length; y++)
                {
                    for (int x = 0; x < length; x++)
                    {
                        matrixOfCosts[y, x] = HYPOTETIC_MAX_VALUE;
                    }
                }
            }

            void SetUncheckedMinValues(ref Coords minimalCoords, ref int minimal, int[,] matrixOfCosts, bool[,] matrixOfChecked)
            {
                for (int y = 0; y < length; y++)
                {
                    for (int x = 0; x < length; x++)
                    {
                        if (matrixOfCosts[y, x] < minimal && !matrixOfChecked[y, x])
                        {
                            minimalCoords = new Coords(x, y);
                            minimal = matrixOfCosts[y, x];
                        }
                    }
                }
            }

            foreach (var character in Characters)
            {
                charactersWithTrails.Add(character, false);
                checkMatrix = new bool[length, length];
                costMatrix = costMatrixPattern;
                costMatrix[character.CurrentPosition.y, character.CurrentPosition.x] = 0;

                FillCostMatrix(ref costMatrix);

                while (CheckMarked(checkMatrix))
                {
                    int min = int.MaxValue;
                    Coords minCoords = new Coords(0, 0);

                    SetUncheckedMinValues(ref minCoords, ref min, costMatrix, checkMatrix);

                    CalculateCostMatrix(graph[minCoords.y, minCoords.x].Edges, minCoords, ref costMatrix);

                    checkMatrix[minCoords.y, minCoords.x] = true;
                }

                SetVictoryPosibility(character, costMatrix, ref charactersWithTrails);
            }

            return IsVictoryPossible(charactersWithTrails);
        }

        //Проверка элементов матрицы смежности
        private bool CheckMarked(bool[,] matrix)
        {
            int length = matrix.GetLength(0);

            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    if (matrix[y, x] == false)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}