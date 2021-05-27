using System;
using System.Linq;

namespace Quoridor_MVC.Models
{
    class AI : AbstractAI
    {
        private bool[,] AdjacencyMatrix;

        public AI(string name, Coords currentPosition)
        {
            Name = name;
            CurrentPosition = currentPosition;
        }

        public void InitAdjacencyMatrix(AbstractGraph graph)
        {
            int numOfElemsInMatrixRow = graph.Vertexes.GetLength(0);
            int numOfElemsInMatrix = (int)Math.Pow(numOfElemsInMatrixRow, 2);
            int CurrentIdVertex = 0;
            AdjacencyMatrix = new bool[numOfElemsInMatrix, numOfElemsInMatrix];

            for (int y = 0; y < numOfElemsInMatrixRow; y++)
            {
                for (int x = 0; x < numOfElemsInMatrixRow; x++)
                {
                    foreach (Coords edge in ((Graph)graph)[y, x].Edges)
                    {
                        AdjacencyMatrix[CurrentIdVertex, edge.x + numOfElemsInMatrixRow * edge.y] = true;
                    }

                    CurrentIdVertex++;
                }
            }
        }

        private byte[,] Deikkstra(
                bool[,] AdjacencyMatrix, 
                int size, 
                int coordStartPositionY, 
                int coordStartPositionX
            )
        {
            //вычисление длин маршрутов по алгоритму дейкстры его модификация может служить и для метода canplace
            //напомните мне его написать. если понадобится

            byte[,] field = new byte[size, size];   // таблица цены до клетки аналог филда
            int numOfElemsInFieldRow = field.GetLength(0);
            int numOfElemsInMatrixRow = AdjacencyMatrix.GetLength(0);
            bool[] mark = new bool[numOfElemsInMatrixRow];  //служебное поле посещена клетка или нет

            byte[,] SetField(int length)
            {
                byte[,] arr = new byte[length, length];

                for (int y = 0; y < length; y++)
                {
                    for (int x = 0; x < length; x++)
                    {
                        arr[y, x] = byte.MaxValue;
                    }
                }

                return arr;
            }

            byte[,] UpdateField(byte[,] arr, int width, bool[,] matrix, int start, int length, int x, int y)
            {
                byte[,] newArr = arr;

                for (int endPoint = 0; endPoint < width; endPoint++)
                {
                    if (matrix[start, endPoint])
                    {
                        byte aroundRoutePrice = newArr[endPoint / length, endPoint % length];
                        byte centralRoutePrice = (byte)(newArr[y, x] + 1);

                        if (aroundRoutePrice > centralRoutePrice)
                        {
                            newArr[endPoint / length, endPoint % length] = centralRoutePrice;
                        }

                    }
                }

                return newArr;
            }

            field = SetField(numOfElemsInFieldRow);

            field[coordStartPositionY, coordStartPositionX] = 0;   //выставление точки отсчета

            while (mark.Contains(false))
            {
                byte max = byte.MaxValue;
                int yind = 0;
                int xind = 0;

                for (int y = 0; y < numOfElemsInFieldRow; y++)
                {
                    for (int x = 0; x < numOfElemsInFieldRow; x++)
                    {
                        bool MinUnvisitedVal = max > field[y, x] && !mark[x + y * size];

                        if (MinUnvisitedVal)
                        {
                            yind = y;
                            xind = x;
                            max = field[y, x];
                        }
                    }
                }

                int startPoint = xind + yind * size;   //так как в таблице смежности используются номера клеток

                field = UpdateField(field, numOfElemsInMatrixRow, AdjacencyMatrix, startPoint, size, xind, yind);

                mark[startPoint] = true;
            }

            return field;
        }
    }
}
