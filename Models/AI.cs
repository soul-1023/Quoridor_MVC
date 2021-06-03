using System;
using System.Collections.Generic;
using System.Linq;

namespace Quoridor_MVC.Models
{
    class AI : AbstractAI 
    {
        public bool[,] BasicMatrix;
        // public AbstractCharacter[] Characters;

        //public List<Coords[]> WiningPoints = new List<Coords[]>();

        public Controllers.AbstractCharactersManager CHMO;
        public Controllers.AbstractGameActivities Ruller;
        public Controllers.ActivitiesChecker check;

        public AI(string name, Coords currentPosition)
        {
            Name = name;
            CurrentPosition = currentPosition;
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
            foreach (bool b in bools)
            {
                if (b == false)
                    i++;

            }
            return i == 0 || (i == 1 && (b3 == false || b4 == false));

            //может ли игра продолжаться
            //&& CanGameProceed(graph, Characters, WinPositions);
        }

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










        public void TestAll(AbstractGraph g)
        {
            

            InitAdjacencyMatrix(g);
            //BasicMatrix[3 * 8 + 4, 4 * 8 + 4] = false;
            //BasicMatrix[3 * 8 + 5, 4 * 8 + 5] = false;

            //BasicMatrix[4 * 8 + 4, 3 * 8 + 4] = false;
            //BasicMatrix[4 * 8 + 5, 3 * 8 + 5] = false;
            List<Matrix> mar = new List<Matrix>();
            foreach (var item in CHMO.Characters)
            {
                Matrix m = new Matrix(BasicMatrix, item.CurrentPosition, CHMO.WinPositions[item]);
                Console.WriteLine(item.Name);
                m.VivodMatix(CHMO.WinPositions[item]);
                Console.WriteLine();
                mar.Add(m);

            }
            //mar[1].AdjacencyMatrix[4 * 8 + 7, 2 * 8 + 7] = true;
            // mar[1].Deikkstra(CHMO.Characters[1].CurrentPosition.y, CHMO.Characters[1].CurrentPosition.x, CHMO.WinPositions.ElementAt(1).Value);
            // mar[1].VivodMatix(CHMO.WinPositions.ElementAt(1).Value);
            //m.VivodMatix(CHMO.WinPositions[this]);


           
            //mar[0].AdjacencyMatrix[]
            //JumpPredicitionWithNoWalsWay(mar.ToArray());

            for (int i = 0; i < CHMO.Characters.Count; i++)
            {
                Console.WriteLine(CHMO.Characters[i].Name);
                mar[i].VivodMatix(CHMO.WinPositions[CHMO.Characters[i]]);
            }

            if (mar[0].cost <= mar[1].cost)
            {
                Console.WriteLine($"We should go on {mar[0].DaWay[1].x}  {mar[0].DaWay[1].y}");

                if(
                    check.CanMove(g,this.CurrentPosition, mar[0].DaWay[1]))
                        {
                        Ruller.MoveCharacter(CurrentPosition, mar[0].DaWay[1]);
                        return;
                    }
                else
                { 
                    mar[0].Isolatioon(this.CurrentPosition ,mar[0].DaWay[1]);
                    mar[0].Deikkstra(this.CurrentPosition.y, this.CurrentPosition.x, CHMO.WinPositions[this]);
                    
                        
                        Ruller.MoveCharacter(CurrentPosition, mar[0].DaWay[1]);
                        return;
                    }
              
            }

            List<(Matrix, Matrix)> mar2 = new List<(Matrix, Matrix)>();

            List<(Coords, Coords, Coords, Coords)> test = new List<(Coords, Coords, Coords, Coords)>();
            for (int i = 0; i < mar[1].cost - 1; i++)
            {
                if(mar[1].DaWay ==null || mar[1].DaWay[i]==null|| mar[1].DaWay[i + 1] == null)
                {
                    break;
                }

                List<(Coords, Coords, Coords, Coords)> a = GenerateBlock(mar[1].DaWay[i], mar[1].DaWay[i + 1]);

                for (int j = 0; j < a.Count; j++)
                {

                    if (a[j].Item1.y == 8 || a[j].Item1.x == 8 || a[j].Item2.y == 8 || a[j].Item2.x == 8 || a[j].Item3.y == 8 || a[j].Item3.x == 8 || a[j].Item4.y == 8 || a[j].Item4.x == 8) 
                    {
                        a.RemoveAt(j);
                        j--;
                    }

                    else if (a[j].Item1.y == -1  || a[j].Item1.x == -1 || a[j].Item2.y == -1 || a[j].Item2.x == -1 || a[j].Item3.y == -1 || a[j].Item3.x == -1 || a[j].Item4.y == -1 || a[j].Item4.x == -1)
                    {
                        a.RemoveAt(j);
                        j--;
                    }


                }
                foreach (var item in a)
                {

                    mar2.Add((new Matrix(BasicMatrix, CHMO.Characters[0].CurrentPosition, CHMO.WinPositions.ElementAt(0).Value, item), new Matrix(BasicMatrix, CHMO.Characters[1].CurrentPosition, CHMO.WinPositions.ElementAt(1).Value, item)));
                    test.Add(item);
                }


                //JumpPredicitionWithNoWalsWay(mar2[i].Item1, mar2[i].Item2);
            }
            foreach (var item in mar2)
            {
                JumpPredicitionWithNoWalsWay(item.Item1, item.Item2);
            }

            for (int i = 0; i < mar2.Count; i++)
            {
                Console.WriteLine(i);
                Console.WriteLine();
                Console.WriteLine(1);
                Console.WriteLine();
                mar2[i].Item1.VivodMatix(CHMO.WinPositions[CHMO.Characters[0]]);

                Console.WriteLine(2);
                Console.WriteLine();
                mar2[i].Item2.VivodMatix(CHMO.WinPositions[CHMO.Characters[1]]);
                Console.WriteLine();
                Console.WriteLine();
            }
            for (int i = 0; i < mar2.Count; i++)
            {
                if (mar2[i].Item1.cost == 255 || mar2[i].Item2.cost == 255  ||!  CanPlaceWall(g,( (test[i].Item1, test[i].Item2), (test[i].Item3, test[i].Item4)), CHMO.Characters, CHMO.WinPositions) )
                {
                    mar2.RemoveAt(i);
                    test.RemoveAt(i);

                    i--;
                    
                }
            }



            for (int i = 0; i < mar2.Count; i++)
            {
                Console.WriteLine(i);
                Console.WriteLine();
                Console.WriteLine(1);
                Console.WriteLine();
                mar2[i].Item1.VivodMatix(CHMO.WinPositions[CHMO.Characters[0]]);

                Console.WriteLine(mar2[i].Item1.cost);

                Console.WriteLine(2);
                Console.WriteLine();
                mar2[i].Item2.VivodMatix(CHMO.WinPositions[CHMO.Characters[1]]);
                Console.WriteLine(mar2[i].Item2.cost);
                Console.WriteLine();
                Console.WriteLine();
            }
            

            int id = 0;
            int cost = 255;
            int diff = 0;


            for (int i = 0; i < mar2.Count; i++)
            {
                if (mar2[i].Item1.cost < cost && mar2[i].Item2.cost > mar2[i].Item1.cost + 1)
                {
                    id = i;
                    cost = mar2[i].Item1.cost;
                    diff = mar2[i].Item2.cost - mar2[i].Item1.cost - 1;
                }
                else if (mar2[i].Item1.cost == mar2[i].Item2.cost + 1 && mar2[i].Item2.cost - mar2[i].Item1.cost - 1 > diff)
                {
                    id = i;
                    cost = mar2[i].Item1.cost;
                    diff = mar2[i].Item2.cost - mar2[i].Item1.cost - 1;
                }

            }
            if (test.Count == 0)
            {
                if (mar[0].DaWay == null)
                {
                    Console.WriteLine($"We");
                    mar[0].Deikkstra(this.CurrentPosition.y, this.CurrentPosition.x, CHMO.WinPositions[this]);
                }
                Console.WriteLine($"We should go on {mar[0].DaWay[1].x}  {mar[0].DaWay[1].y}");

                if (
                    check.CanMove(g, this.CurrentPosition, mar[0].DaWay[1]))
                {
                    Ruller.MoveCharacter(CurrentPosition, mar[0].DaWay[1]);
                    return;
                }
                else
                {
                    mar[0].Isolatioon(this.CurrentPosition, mar[0].DaWay[1]);
                    mar[0].Deikkstra(this.CurrentPosition.y, this.CurrentPosition.x, CHMO.WinPositions[this]);


                    Ruller.MoveCharacter(CurrentPosition, mar[0].DaWay[1]);
                    return;
                }
            }
            Console.WriteLine();
            Console.WriteLine("WP");
            Ruller.PlaceWall(this,((test[id].Item1, test[id].Item2), (test[id].Item3, test[id].Item4)));

            oleja.PaintEnemyWalls((test[id].Item1, test[id].Item2), (test[id].Item3, test[id].Item4));

            mar2[id].Item1.VivodMatix(CHMO.WinPositions[CHMO.Characters[0]]);
            mar2[id].Item2.VivodMatix(CHMO.WinPositions[CHMO.Characters[1]]);

        }

        public void JumpPredicitionWithNoWalsWay(params Matrix[] m)
        {


            int min = m.Min(x => x.cost);


            int i = 1;

            while (i < m.Min(x => x.cost))
            {
                min = m.Min(x => x.cost);
                //сначала надо проверить своё положение со втророго хода
                //и запилить вторую чистую матрицу.
                //



                //потом прогноз для остальных кстати маршрут может укоротиться. надо предусмотреть
                for (int j = 0; j < m.Length; j++)
                {


                    if (j == 1)
                    {

                        //{
                        if (m[0].DaWay != null)
                        {
                            //if (CheckOnJump(m[1].DaWay[i], m[0].DaWay[i +1]))
                            //{
                            foreach (Coords item in FourCoords(m[0].DaWay[i + 1], i, ref m[j]))
                            {
                                m[j].JumpSetter(item, m[0].DaWay[i + 1], CHMO.Characters[j].CurrentPosition, CHMO.WinPositions[CHMO.Characters[j]]);
                                if (m.Min(x => x.cost) < i)
                                {
                                    return;
                                }
                            }

                        }
                        else
                        {

                        }
                        //}
                    }



                    if (j == 0)
                    {

                        //    if (CheckOnJump(m[0].DaWay[i], m[1].DaWay[i]))
                        //{
                        if (m[1].DaWay != null)
                        {
                            foreach (var item in FourCoords(m[1].DaWay[i], i, ref m[j]))
                            {
                                m[j].JumpSetter(item, m[1].DaWay[i], CHMO.Characters[j].CurrentPosition, CHMO.WinPositions[CHMO.Characters[j]]);
                                if (m.Min(x => x.cost) < i)
                                {
                                    return;
                                }
                            }
                        }
                    }
                    //}
                }

                i++;
            }
        }






        private List<(Coords, Coords, Coords, Coords)> GenerateBlock(Coords a, Coords b)
        {
            List<(Coords, Coords, Coords, Coords)> linkedCoords = new List<(Coords, Coords, Coords, Coords)>();

            if (Math.Abs(a.x - b.x + a.y - b.y) == 1)
            {
                int dirx = a.x - b.x;
                int diry = a.y - b.y;
                if (a.x - diry < 8 && a.x - diry > 0 && b.y - diry < 8 && b.x - diry > 0)
                {
                    linkedCoords.Add((new Coords(a.x - diry, a.y - dirx), new Coords(b.x - diry, b.y - dirx), a, b));
                }
                if (a.x + diry < 8 && a.x + diry > 0 && b.y + diry < 8 && b.x + diry > 0)
                {
                    linkedCoords.Add((a, b, new Coords(a.x + diry, a.y + dirx), new Coords(b.x + diry, b.y + dirx)));
                }

            }


            else if (Math.Abs(a.x - b.x) == 2 || Math.Abs(a.y - b.y) == 2)
            {
                int dirx = a.x - b.x;
                int diry = a.y - b.y;
                int subDirx = dirx / 2;
                int subDiry = diry / 2;

                Coords subb = new Coords(b.x - subDirx, b.y - subDiry);


                linkedCoords.Add((new Coords(a.x - subDiry, a.y - subDiry), new Coords(subb.x - subDiry, subb.y - subDiry), a, subb));
                linkedCoords.Add((new Coords(a.x + subDiry, a.y + subDiry), new Coords(subb.x + subDiry, subb.y + subDiry), a, subb));

                linkedCoords.Add((new Coords(subb.x - subDiry, subb.y - subDiry), new Coords(b.x - subDiry, b.y - subDiry), subb, b));

                linkedCoords.Add((new Coords(subb.x + subDiry, subb.y + subDiry), new Coords(b.x + subDiry, b.y + subDiry), subb, b));




            }
            else if (Math.Abs(a.x - b.x) == 1 && Math.Abs(a.y - b.y) == 1)
            {

            }


            return linkedCoords;

        }



        public bool CheckOnJump(Coords a, Coords b)
        {
            if (Math.Abs(a.y - b.y) < 2 && Math.Abs(a.x - b.x) < 2 && Math.Abs(a.y - b.y + a.x - b.x) < 2)
            {
                return true;
            }
            return false;
        }










        private List<Coords> FourCoords(Coords c, int cost, ref Matrix m)
        {
            if (c == null)
            {
                return null;
            }
            List<Coords> RetCoord = new List<Coords>();
            if (c.x != 0 && m.costMatrix[c.y, c.x - 1] == cost)
            {
                RetCoord.Add(new Coords(c.x - 1, c.y));
            }
            if (c.x != 7 && m.costMatrix[c.y, c.x + 1] == cost)
            {
                RetCoord.Add(new Coords(c.x + 1, c.y));
            }
            if (c.y != 0 && m.costMatrix[c.y - 1, c.x] == cost)
            {
                RetCoord.Add(new Coords(c.x, c.y - 1));
            }
            if (c.y != 7 && m.costMatrix[c.y + 1, c.x] == cost)
            {
                RetCoord.Add(new Coords(c.x, c.y + 1));
            }
            return RetCoord;

        }









        private void InitAdjacencyMatrix(AbstractGraph graph)
        {
            int numOfElemsInMatrixRow = graph.Vertexes.GetLength(0);
            int numOfElemsInMatrix = (int)Math.Pow(numOfElemsInMatrixRow, 2);
            int CurrentIdVertex = 0;
            BasicMatrix = new bool[numOfElemsInMatrix, numOfElemsInMatrix];

            for (int y = 0; y < numOfElemsInMatrixRow; y++)
            {
                for (int x = 0; x < numOfElemsInMatrixRow; x++)
                {
                    foreach (Coords edge in ((Graph)graph)[y, x].Edges)
                    {
                        BasicMatrix[CurrentIdVertex, edge.x + numOfElemsInMatrixRow * edge.y] = true;
                    }

                    CurrentIdVertex++;
                }
            }
        }

        public byte[,] Deikkstra(bool[,] AdjacencyMatrix, int coordStartPositionY, int coordStartPositionX)
        {
            int size = (int)Math.Sqrt(AdjacencyMatrix.GetLength(0));
            //вычисление длин маршрутов по алгоритму дейкстры его модификация может служить и для метода canplace
            //напомните мне его написать. если пондобится

            byte[,] field = new byte[size, size];   // таблица цены до клетки аналог филда
            int numOfElemsInFieldRow = field.GetLength(0);
            int numOfElemsInMatrixRow = AdjacencyMatrix.GetLength(0);
            bool[] mark = new bool[numOfElemsInMatrixRow];  //служебное поле посещенна клетка или нет

            for (int y = 0; y < numOfElemsInFieldRow; y++)
            {
                for (int x = 0; x < numOfElemsInFieldRow; x++)
                {
                    field[y, x] = byte.MaxValue;
                }
            }

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

                for (int endPoint = 0; endPoint < numOfElemsInMatrixRow; endPoint++)
                {
                    if (AdjacencyMatrix[startPoint, endPoint])
                    {
                        byte aroundRoutePrice = field[endPoint / size, endPoint % size];
                        byte centralRoutePrice = (byte)(field[yind, xind] + 1);

                        if (aroundRoutePrice > centralRoutePrice)
                        {
                            field[endPoint / size, endPoint % size] = centralRoutePrice;
                        }

                    }
                }

                mark[startPoint] = true;
            }

            return field;
        }



        public void VivodMatix(bool[,] matrix)
        {

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{(matrix[i, j] == true ? 'y' : 'x'),-2}");
                }
                Console.WriteLine();
            }
        }





        public void VivodMatix(byte[,] matrix, Coords[] winx)
        {

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (Array.Find(winx, x => x.x == j && x.y == i) != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{matrix[i, j],-2}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write($"{matrix[i, j],-2}");
                    }
                }
                Console.WriteLine();
            }
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