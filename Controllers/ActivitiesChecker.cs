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
        //Отрефакторить
        private bool CanGameProceed(AbstractGraph graph, List<AbstractCharacter> Characters, Dictionary<AbstractCharacter, Coords[]> WinPositions)
        {
            int length = graph.Vertexes.GetLength(0);
            const int HYPOTETIC_MAX_VALUE = 1000000;
            int[,] costMatrixPattern = new int[length, length];
            Dictionary<AbstractCharacter, bool> CharacterBool = new Dictionary<AbstractCharacter, bool>();

            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    costMatrixPattern[y, x] = HYPOTETIC_MAX_VALUE;
                }
            }

            foreach (AbstractCharacter character in Characters)
            {
                CharacterBool.Add(character, false);
                int[,] costMatrix = costMatrixPattern;
                bool[,] checkMatrix = new bool[length, length];
                costMatrix[character.CurrentPosition.y, character.CurrentPosition.x] = 0;

                while (CheckAdjMatrix(checkMatrix) == true)
                {
                    int min = int.MaxValue;
                    Coords minCords = new Coords(0, 0);

                    for (int y = 0; y < length; y++)
                    {
                        for (int x = 0; x < length; x++)
                        {
                            if (costMatrix[y, x] < min && checkMatrix[y, x] == false)
                            {
                                minCords = new Coords(x, y);
                                min = costMatrix[y, x];
                            }
                        }
                    }

                    foreach (Coords Edge in graph[minCords.y, minCords.x].Edges)
                    {
                        if (costMatrix[Edge.y, Edge.x] > costMatrix[minCords.y, minCords.x] + 1)
                        {
                            costMatrix[Edge.y, Edge.x] = costMatrix[minCords.y, minCords.x] + 1;
                        }
                    }

                    checkMatrix[minCords.y, minCords.x] = true;
                }

                foreach (Coords coords in WinPositions[character])
                {
                    if (costMatrix[coords.y, coords.x] < HYPOTETIC_MAX_VALUE)
                    {
                        CharacterBool[character] = true;
                        continue;
                    }
                }
            }

            foreach (var item in CharacterBool)
            {
                if (item.Value == false)
                {
                    return false;
                }
            }

            return true;
        }

        //Проверка элементов матрицы смежности
        private bool CheckAdjMatrix(bool[,] AdjMatrix)
        {
            int length = AdjMatrix.GetLength(0);

            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    if (AdjMatrix[y, x] == false)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}