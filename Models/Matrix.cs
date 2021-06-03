using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor_MVC.Models
{
    class Matrix
    {
        public bool[,] AdjacencyMatrix;
        public byte[,] costMatrix;
        public byte cost;
        public List <Coords> DaWay;
        private int size;

        public Matrix(bool[,] AdjacencyMatrix,Coords coords,Coords[] WinPoints)

        {
            int n= AdjacencyMatrix.GetLength(0);
            this.AdjacencyMatrix = new  bool[n,n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    this.AdjacencyMatrix[i, j] = ( AdjacencyMatrix[i, j]);
                }
            }
           
            Deikkstra( coords.y, coords.x,WinPoints);

            

        }
        public Matrix(bool[,] AdjacencyMatrix, Coords coords, Coords[] WinPoints,  (Coords, Coords,Coords,Coords) linkKoords)

        {
            int n = AdjacencyMatrix.GetLength(0);
            this.AdjacencyMatrix = new bool[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    this.AdjacencyMatrix[i, j] = (AdjacencyMatrix[i, j]);
                }
            }
            this.AdjacencyMatrix[linkKoords.Item1.y * 8+ linkKoords.Item1.x, linkKoords.Item2.y * 8 + linkKoords.Item2.x] = false;
            this.AdjacencyMatrix[linkKoords.Item2.y * 8 + linkKoords.Item2.x, linkKoords.Item1.y * 8 + linkKoords.Item1.x] = false;





            this.AdjacencyMatrix[linkKoords.Item3.y * 8 + linkKoords.Item3.x, linkKoords.Item4.y * 8 + linkKoords.Item4.x] = false;
            this.AdjacencyMatrix[linkKoords.Item4.y * 8 + linkKoords.Item4.x, linkKoords.Item3.y * 8 + linkKoords.Item3.x] = false;


            Deikkstra(coords.y, coords.x, WinPoints);



        }

        public void Deikkstra( int coordStartPositionY, int coordStartPositionX,Coords[ ] WP)
        {
            size = (int)Math.Sqrt(AdjacencyMatrix.GetLength(0));
            int mod = WP[0].y - WP[1].y==0?1:8;
            

            List<Coords>[,] CoordsPoint= new List<Coords>[size,size];

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
            CoordsPoint[coordStartPositionY, coordStartPositionX]= new List<Coords> { (new Coords(coordStartPositionX, coordStartPositionY)) };

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
                            max = field[ y ,  x ];
                        }
                    }
                }
                if (max == byte.MaxValue)
                {
                    this.costMatrix = field;
                    mark[xind + yind * size] = true;
                    Coords mb = DaWayFinder(WP);
                    if (mb == null)
                    {
                        
                        cost = 255;
                    }
                    return;
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
                            CoordsPoint[endPoint / size, endPoint % size]=new List<Coords>( CoordsPoint[yind, xind]); 
                            CoordsPoint[endPoint / size, endPoint % size].Add(new Coords(endPoint % size, endPoint / size));
                            
                        }
                        
                        else if (aroundRoutePrice == centralRoutePrice &&Math.Abs(startPoint-endPoint)==mod)
                        {
                            CoordsPoint[endPoint / size, endPoint % size] = new List<Coords>(CoordsPoint[yind, xind]);
                            CoordsPoint[endPoint / size, endPoint % size].Add(new Coords(endPoint % size, endPoint / size));
                        }

                    }
                }

                mark[startPoint] = true;

            }
            this.costMatrix = field;
            Coords WayCoord= DaWayFinder(WP);
            if (WayCoord == null)
            {

            }
            DaWay = CoordsPoint[WayCoord.y, WayCoord.x];
           // BuildHorizontal(WayCoord);
            
        }


        public Coords DaWayFinder(Coords[] WinPoints)
        {
            byte max = byte.MaxValue;
            Coords maxC= new Coords(0,0);
            foreach (var item in  WinPoints)
            {
                if (max > costMatrix[item.y, item.x])
                {
                    max = costMatrix[item.y, item.x];
                    maxC = item;
                }
            }
            cost = max;
            if (max == byte.MaxValue)
            {
                return null;
            }
            return maxC;
        }

        public void VivodMatix(Coords[] winx)
        {
            
            for (int i = 0; i < costMatrix.GetLength(0); i++)
            {
                Console.Write(i + "     ");
            }
            Console.WriteLine();
            for (int i = 0; i < costMatrix.GetLength(0); i++)
            {

                for (int j = 0; j < costMatrix.GetLength(1); j++)
                {
                    if (Array.Find(winx, x => x.x == j && x.y == i) != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{costMatrix[i, j],-3}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (DaWay != null && DaWay.Find(x => x.x == j && x.y == i) != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{costMatrix[i, j],-3}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write($"{costMatrix[i, j],-3}");
                    }
                    if (j != 7 && AdjacencyMatrix[i * costMatrix.GetLength(0) + j, i * costMatrix.GetLength(0) + j + 1] && AdjacencyMatrix[i * costMatrix.GetLength(0) + j + 1, i * costMatrix.GetLength(0) + j])
                    {
                        Console.Write($"{"-",-3}");
                    }
                    else if (j != 7 && AdjacencyMatrix[i * costMatrix.GetLength(0) + j, i * costMatrix.GetLength(0) + j + 1])
                    {
                        Console.Write($"{">",-3}");
                    }
                    else if (j != 7 && AdjacencyMatrix[i * costMatrix.GetLength(0) + j + 1, i * costMatrix.GetLength(0) + j])
                    {
                        Console.Write($"{"<",-3}");
                    }
                    else {
                        Console.Write($"{" ",-3}");
                    }

                }
                Console.WriteLine();
                for (int j = 0; j < costMatrix.GetLength(1); j++)
                {
                    if (i != 7 && AdjacencyMatrix[i * costMatrix.GetLength(0) + j, i * costMatrix.GetLength(0) + j + costMatrix.GetLength(0)] && AdjacencyMatrix[i * costMatrix.GetLength(0) + j + costMatrix.GetLength(0) , i * costMatrix.GetLength(0) + j])
                    {
                        Console.Write($"{"|",-6}");
                    }
                    else if (i != 7 && AdjacencyMatrix[i * costMatrix.GetLength(0) + j, i * costMatrix.GetLength(0) + j + costMatrix.GetLength(0)])
                    {
                        Console.Write($"{"v",-6}");
                    }
                    else if (i != 7 && AdjacencyMatrix[i * costMatrix.GetLength(0) + j + costMatrix.GetLength(0) , i * costMatrix.GetLength(0) + j])
                    {
                        Console.Write($"{"A",-6}");
                    }

                    else
                    {
                        Console.Write($"{" ",-6}");
                    }
                }
                Console.WriteLine();


            }
        }


        public void JumpSetter(Coords From, Coords Through,Coords curent, Coords[] WP)
        {

            int AdjFrom = From.y * size + From.x;
            int AdjThrough = Through.y * size + Through.x;
            int sizeOfAdj = AdjacencyMatrix.GetLength(0);

            int Diff = AdjThrough - AdjFrom;
            int revDiff = 0;
            switch (Diff)
            {
                case 8:
                    {
                        revDiff = 1;
                        break;
                    }
                case -8:
                    {
                        revDiff = 1;
                        break;
                    }
                case -1:
                    {
                        revDiff = 8;
                        break;
                    }
                case 1:
                    {
                        revDiff = 8;
                        break;
                    }
            }
             

            if (AdjacencyMatrix[AdjFrom, AdjThrough] && (AdjThrough + Diff) / 8<8 && (AdjThrough + Diff )/ 8  > 0  )
            {
                if(AdjacencyMatrix[AdjThrough, AdjThrough + Diff])
                {
                    AdjacencyMatrix[AdjFrom, AdjThrough + Diff] = true;
                  //  Isolatioon(AdjThrough);
                }
                else 
                {
                    if (AdjThrough / 8 != 0 && AdjFrom % 8 != 6 && AdjThrough +revDiff < sizeOfAdj && AdjThrough + Diff > 0 &&  AdjacencyMatrix[AdjThrough, AdjThrough + revDiff])
                    {
                        AdjacencyMatrix[AdjFrom, AdjThrough + revDiff] = true;
                        //Isolatioon(AdjThrough);
                    }
                    if (  AdjThrough / 8 !=0  && AdjFrom % 8 !=6 && AdjThrough - revDiff < sizeOfAdj && AdjThrough - Diff > 0 && AdjacencyMatrix[AdjThrough, AdjThrough - revDiff])
                    {
                        AdjacencyMatrix[AdjFrom, AdjThrough - revDiff] = true;
                       // Isolatioon(AdjThrough);
                    }


                }
            }
            Deikkstra(curent.y,curent.x,WP);

        }

        public void Isolatioon(Coords a,Coords b)
        {
            int NumOfVertex = a.y * 8 + a.x;
                int i= b.y * 8 + b.x;

            AdjacencyMatrix[NumOfVertex,i] = false;
             
        }
       
    }

   
}
