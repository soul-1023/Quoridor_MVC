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
            return SuitsConditions(graph, characterPosition, chosenPosition, true);
        }

        public bool CanPlaceWall(AbstractGraph graph, ((Coords, Coords), (Coords, Coords)) CoordPairs,
            List<AbstractCharacter> Characters, Dictionary<AbstractCharacter, Coords[]> WinPositions)
        {
            bool b1 = SuitsConditions(graph, CoordPairs.Item1.Item1, CoordPairs.Item1.Item2, false);
            bool b2 = SuitsConditions(graph, CoordPairs.Item2.Item1, CoordPairs.Item2.Item2, false);
            bool b3 = SuitsConditions(graph, CoordPairs.Item1.Item1, CoordPairs.Item2.Item1, false);
            bool b4 = SuitsConditions(graph, CoordPairs.Item1.Item2, CoordPairs.Item2.Item2, false);
            int i = 0;
            List<bool> bools = new List<bool> { b1, b2, b3, b4 };
            foreach(bool b in bools)
            {
                if (b == false)
                    i++;

            }
            var one=CanGameProceed(graph, Characters, WinPositions);
            return i == 0 || (i == 1 && (b3 == false || b4 == false));

            //может ли игра продолжаться
           
        }

        public AbstractCharacter DefineWinner(AbstractCharacter character, Coords[] winPoints)
        {

            foreach (Coords pos in winPoints)
            {
                if (pos.x == character.CurrentPosition.x && pos.y == character.CurrentPosition.y)
                    return character;
            }

            return null;
        }

        //Неправильные вершины
        private bool SuitsConditions(AbstractGraph graph, Coords firstPosition, Coords secondPosition, bool isMove)
        {
            bool xNotSmall = secondPosition.x >= 0;
            bool xNotBig = secondPosition.x < graph.Vertexes.GetLength(0);
            bool yNotSmall = secondPosition.y >= 0;
            bool yNotBig = secondPosition.y < graph.Vertexes.GetLength(0);
            bool characterHasLink;
            if (isMove)
            {
                characterHasLink = graph[firstPosition.y, firstPosition.x].Edges.
                    Find(e => e.x == secondPosition.x && e.y == secondPosition.y && graph[e.y, e.x].IsCharacter == false)
                    != null;
            }
            else
            {
                characterHasLink = graph[firstPosition.y, firstPosition.x].Edges.
                    Find(e => e.x == secondPosition.x && e.y == secondPosition.y)
                    != null;
            }

            return (xNotSmall && xNotBig && yNotSmall && yNotBig && characterHasLink);
        }

        //Проверка на продолжение игры
        public bool CanGameProceed(AbstractGraph graph, List<AbstractCharacter> Characters, Dictionary<AbstractCharacter, Coords[]> WinPositions)
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
                
                FillCostMatrix(ref costMatrix);
                costMatrix[character.CurrentPosition.y, character.CurrentPosition.x] = 0;

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