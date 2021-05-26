using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quoridor_MVC.Controllers;
using Quoridor_MVC.Models;

namespace Quoridor_MVC.View
{
    sealed class Game
    {
        AbstractGameActivities gameActivities = new GameActivities();

        Control[,] verticalLabels;
        Control[,] horizontalLabels;
        Control[,] vertexLabels;

        public Game(TableLayoutPanel field, int sizeOfField, int countOfCharacters)
        {
            gameActivities.InitializeSession(sizeOfField, countOfCharacters);
            gameActivities.getWinPositions(gameActivities.GetActiveCharacter());

            //отрисовка поля
            InitializeField(field, sizeOfField);
        }

        void InitializeField(TableLayoutPanel field, int sizeOfField)
        {
            int xVertex = 0, 
                yVertex = 0, 
                yHorizontalLabel = 0, 
                xVerticalLabel = 0, 
                yVerticalLabel = 0;

            const int DIVISION_INCREASE = 2;
            const int EXCESS_WALL = 1;
            int elemsCount = (DIVISION_INCREASE * sizeOfField) - EXCESS_WALL;
            field.RowCount = field.ColumnCount = elemsCount;
            vertexLabels = new Control[sizeOfField, sizeOfField];
            horizontalLabels = new Control[sizeOfField, elemsCount];
            verticalLabels = new Control[sizeOfField, elemsCount];

            for (int i = 0; i < field.RowCount; i++)
            {
                bool switchYVertex = false;
                bool switchXVerticalLabel = false;
                bool switchYHorizontalLabel = false;

                for (int j = 0; j < field.ColumnCount; j++)
                {
                    if ((i % 2 == 0) && (j % 2 == 0))
                    {
                        VertexLabel vertex = new VertexLabel();
                        field.Controls.Add(vertex, i, j);
                        vertex.BackColor = Color.White;
                        vertex.Dock = DockStyle.Fill;
                        vertex.Height = 60;
                        vertex.Width = 60;
                        vertex.Margin = new Padding(0);
                        vertexLabels[yVertex, xVertex] = vertex;
                        xVertex++;
                        switchYVertex = true;

                    }
                    else if ((i % 2 == 0) && (j % 2 == 1))
                    {
                        WallLabel wall = new WallLabel();
                        field.Controls.Add(wall, i, j);
                        verticalLabels[xVerticalLabel, i] = wall;
                        yVerticalLabel++;
                        wall.BackColor = Color.Gray;
                        wall.Dock = DockStyle.Fill;
                        wall.Width = 15;
                        wall.Height = 15;
                        wall.Margin = new Padding(0);
                        switchXVerticalLabel = true;
                    }
                    else if ((i % 2 == 1) && (j % 2 == 0))
                    {
                        WallLabel wall = new WallLabel();
                        field.Controls.Add(wall, i, j);
                        horizontalLabels[yHorizontalLabel, j] = wall;
                        wall.BackColor = Color.Gray;
                        wall.Dock = DockStyle.Fill;
                        wall.Width = 15;
                        wall.Height = 15;
                        wall.Margin = new Padding(0);
                        switchYHorizontalLabel = true;
                    }
                    else
                    {
                        Label label = new Label();
                        field.Controls.Add(label, i, j);
                        verticalLabels[xVerticalLabel, i] = label;
                        horizontalLabels[yHorizontalLabel, j] = label;
                        label.BackColor = Color.Gray;
                        label.Dock = DockStyle.Fill;
                        label.Width = 15;
                        label.Height = 15;
                        label.Margin = new Padding(0);
                    }
                }

                if (switchYVertex == true)
                {
                    switchYVertex = false;
                    yVertex++;
                    xVertex = 0;
                }
                if (switchXVerticalLabel == true)
                {
                    switchXVerticalLabel = false;
                    xVerticalLabel++;
                }
                if (switchYHorizontalLabel == true)
                {
                    switchYHorizontalLabel = false;
                    yHorizontalLabel++;
                }
            }
        }
    }
}
