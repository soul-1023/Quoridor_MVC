using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quoridor_MVC.Models;
using Quoridor_MVC.View;

namespace Quoridor_MVC.View
{
    public class Oleg
    {
        private Graph graph = new Models.Graph(8);
        public VertexLabel[][] vertexLabelList1;
        public Control[][] horizontalWallArray;
        public Control[][] verticalWallArray;

        bool wallSpend = false;
        public int walls = 10;

        public int x = 0;
        public  int y = 0;
        public  int xE = 4;
        public  int yE = 0;
        public  Button buttonWall;

        public Oleg()
        {

        }

        public void Start(TableLayoutPanel table, Label labelWallsYou, Button buttonWallSend)
        {
            buttonWall = buttonWallSend;
            vertexLabelList1 = new VertexLabel[graph.Vertexes.GetLength(0)][];
            for (int i = 0; i < vertexLabelList1.GetLength(0); i++)
            {
                vertexLabelList1[i] = new VertexLabel[graph.Vertexes.GetLength(0)];
            }
            verticalWallArray = new Control[graph.Vertexes.GetLength(0)][];
            horizontalWallArray = new Control[graph.Vertexes.GetLength(0)][];
            for (int i = 0; i < verticalWallArray.GetLength(0); i++)
            {
                verticalWallArray[i] = new Control[graph.Vertexes.GetLength(0) + graph.Vertexes.GetLength(0) - 1];
                horizontalWallArray[i] = new Control[2 * (graph.Vertexes.GetLength(0)) - 1];
            }
            int xVertex = 0, yVertex = 0, yHorizontalLabel = 0, yHorizontalWall = 0;
            
            table.RowCount = (2 * graph.Vertexes.GetLength(0)) - 1;
            table.ColumnCount = (2 * graph.Vertexes.GetLength(0)) - 1;
            table.BackColor = Color.Black;
            for (int i = 0; i < table.RowCount; i++)
            {
                bool switchYVertex = false;
                bool switchYHorizontalLabel = false;
                yHorizontalWall = 0;
                for (int j = 0; j < table.ColumnCount; j++)
                {

                    if ((i % 2 == 0) && (j % 2 == 0))
                    {
                        VertexLabel vertex = new VertexLabel();
                        table.Controls.Add(vertex, i, j);
                        //vertex.BackColor = Color.White;
                        //vertex.Dock = DockStyle.Fill;
                        //vertex.Height = 60;
                        //vertex.Width = 60;
                        //vertex.Margin = Padding;
                        SetFormat(vertex, 60, Color.White);
                        vertex.Click += (s, e) => label_Click_vertex(vertex);
                        vertex.MouseLeave += (s, e) => label_mouse_leave_vertex(vertex);
                        vertexLabelList1[yVertex][xVertex] = vertex;
                        xVertex++;
                        switchYVertex = true;

                    }
                    else if ((i % 2 == 0) && (j % 2 == 1))
                    {
                        WallLabel wall = new WallLabel();
                        table.Controls.Add(wall, i, j);
                        horizontalWallArray[yHorizontalWall][i] = wall;
                        //wall.BackColor = Color.Gray;
                        //wall.Dock = DockStyle.Fill;
                        //wall.Width = 15;
                        //wall.Height = 15;
                        //wall.Margin = Padding;
                        SetFormat(wall, 15, Color.Gray);
                        wall.Click += (s, e) => label_Click_wall(wall, labelWallsYou);
                        wall.MouseEnter += (s, e) => label_mouse_enter_wall(wall);
                        wall.MouseLeave += (s, e) => label_mouse_leave_wall(wall);
                        yHorizontalWall++;
                    }
                    else if ((i % 2 == 1) && (j % 2 == 0))
                    {
                        WallLabel wall = new WallLabel();
                        table.Controls.Add(wall, i, j);
                        verticalWallArray[yHorizontalLabel][j] = wall;
                        //wall.BackColor = Color.Gray;
                        //wall.Dock = DockStyle.Fill;
                        //wall.Width = 15;
                        //wall.Height = 15;
                        //wall.Margin = Padding;
                        SetFormat(wall, 15, Color.Gray);
                        wall.Click += (s, e) => label_Click_wall(wall, labelWallsYou);



                        wall.MouseEnter += (s, e) => label_mouse_enter_wall(wall);
                        wall.MouseLeave += (s, e) => label_mouse_leave_wall(wall);
                        switchYHorizontalLabel = true;
                    }
                    else
                    {
                        Label label = new Label();
                        table.Controls.Add(label, i, j);
                        horizontalWallArray[yHorizontalWall][i] = label;
                        verticalWallArray[yHorizontalLabel][j] = label;
                        //label.BackColor = Color.Gray;
                        //label.Dock = DockStyle.Fill;
                        //label.Width = 15;
                        //label.Height = 15;
                        //label.Margin = Padding;
                        SetFormat(label, 15, Color.Gray);
                        yHorizontalWall++;
                    }
                }
                if (switchYVertex == true)
                {
                    switchYVertex = false;
                    yVertex++;
                    xVertex = 0;
                }

                if (switchYHorizontalLabel == true)
                {
                    switchYHorizontalLabel = false;
                    yHorizontalLabel++;
                }
            }

            SetCharacterStart(x, y);
            CanMove(vertexLabelList1[x][y]);
        }

        public  void SetFormat(Control control, int size, Color color)
        {
            Padding padding =  new Padding(0);
            control.BackColor = color;
            control.Dock = DockStyle.Fill;
            control.Height = size;
            control.Width = size;
            control.Margin = padding;
        }

        public  void SetCharacterStart(int x, int y)
        {
            vertexLabelList1[y][x].isCharacter = true;
            vertexLabelList1[y][x].BackColor = Color.Green;
            vertexLabelList1[xE][yE].isCharacter = true;
            vertexLabelList1[xE][yE].BackColor = Color.Orange;
        }

        public  void CanMove(VertexLabel vertex)
        {
            for (int i = 0; x == -1; i++)
            {
                x = Array.IndexOf(vertexLabelList1[i], vertex);
                y = i;
            }
            if (y + 1 < vertexLabelList1.GetLength(0))
            {
                vertexLabelList1[y + 1][x].BackColor = Color.GreenYellow;
            }

            if (y - 1 > -1)
            {
                vertexLabelList1[y - 1][x].BackColor = Color.GreenYellow;
            }
            if (x - 1 > -1)
            {
                vertexLabelList1[y][x - 1].BackColor = Color.GreenYellow;
            }
            if (x + 1 < vertexLabelList1.GetLength(0))
            {
                vertexLabelList1[y][x + 1].BackColor = Color.GreenYellow;
            }
        }

        private void label_Click_vertex(VertexLabel vertex)
        {
            if (wallSpend != true)
            {
                CancelCanMove(vertexLabelList1[y][x]);
                SetCharacter(vertex);
                CanMove(vertex);
            }
        }

        public  void CancelCanMove(VertexLabel vertex)
        {
            for (int i = 0; x == -1; i++)
            {
                x = Array.IndexOf(vertexLabelList1[i], vertex);
                y = i;
            }
            try
            {
                vertexLabelList1[y + 1][x].BackColor = Color.White;
            }
            catch
            {

            }
            try
            {
                vertexLabelList1[y - 1][x].BackColor = Color.White;
            }
            catch
            {

            }
            try
            {
                vertexLabelList1[y][x - 1].BackColor = Color.White;
            }
            catch
            {

            }
            try
            {
                vertexLabelList1[y][x + 1].BackColor = Color.White;
            }
            catch
            {

            }
        }

        private void label_mouse_leave_vertex(VertexLabel vertex)
        {
            if (vertex.BackColor == Color.Wheat)
            {
                vertex.BackColor = Color.White;
            }
        }

        public void label_Click_wall(WallLabel wall, Label labelWallsYou)
        {

            if ((wallSpend == true) && (walls > 0))
            {
                wall.MouseEnter -= (s, e) => label_mouse_enter_wall(wall);
                wall.MouseLeave -= (s, e) => label_mouse_leave_wall(wall);
                SpendWall(wall);
                labelWallsYou.Text = "You walls: " + walls;

            }
        }

        private void label_mouse_enter_wall(WallLabel wall)
        {
            if (wallSpend == true)
            {
                EnterWall(wall);
            }
        }

        private void label_mouse_leave_wall(WallLabel wall)
        {
            if (wallSpend == true)
            {
                LeaveWall(wall);
            }
        }

        public void Button1_Click(TableLayoutPanel table,Button buttonWall,Button button1, Label labelWallsYou, Label labelWallsEnemy)
        {
            table.Visible = true;
            buttonWall.Enabled = true;
            buttonWall.Visible = true;
            button1.Visible = false;
            button1.Enabled = false;
            labelWallsYou.Text = "You walls: " + walls;
            labelWallsEnemy.Text = "Enemy walls: 10";
        }

        public void ButtonSpendWall_Click(object sender, EventArgs e)
        {
            if ((wallSpend != true) && (walls > 0))
            {
                wallSpend = true;
                buttonWall.Text = "Cancel wall";
                CancelCanMove(vertexLabelList1[y][x]);
            }
            else
            {
                wallSpend = false;
                buttonWall.Text = "Wall";
                CanMove(vertexLabelList1[y][x]);
            }
        }
        public  void SetEnemyStart(int xE, int yE)
        {
            vertexLabelList1[xE][yE].isCharacter = false;
            vertexLabelList1[xE][yE].BackColor = Color.White;
        }

        public  void SetCharacter(VertexLabel vertex)
        {
            vertexLabelList1[y][x].isCharacter = false;
            vertexLabelList1[y][x].BackColor = Color.White;
            vertex.isCharacter = true;
            vertex.BackColor = Color.Green;
            x = -1;
            for (int i = 0; x == -1; i++)
            {
                x = Array.IndexOf(vertexLabelList1[i], vertex);
                y = i;
            }
        }

        public  void SpendWall(WallLabel wall)
        {
            int xWall = -1, yWall = -1;

            for (int i = 0; i < verticalWallArray.GetLength(0); i++)
            {
                foreach (Control element in verticalWallArray[i])
                {
                    xWall = Array.IndexOf(verticalWallArray[i], wall);
                    if (xWall != -1)
                    {
                        yWall = i;
                        break;
                    }
                }
                if (yWall != -1)
                {
                    break;
                }
            }
            if (yWall != -1)
            {
                if(xWall+2<verticalWallArray[yWall].Length)
                { 

                    verticalWallArray[yWall][xWall].Visible = false;
                    verticalWallArray[yWall][xWall + 1].Enabled = false;
                    verticalWallArray[yWall][xWall + 2].Visible = false;
                    verticalWallArray[yWall][xWall].BackColor = Color.Black;
                    verticalWallArray[yWall][xWall + 1].BackColor = Color.Black;
                    verticalWallArray[yWall][xWall + 2].BackColor = Color.Black;

                    walls--;
                }
            }
            else
            {
                for (int i = 0; i < horizontalWallArray.GetLength(0); i++)
                {
                    foreach (Control element in horizontalWallArray[i])
                    {
                        xWall = Array.IndexOf(horizontalWallArray[i], wall);
                        if (xWall != -1)
                        {
                            yWall = i;
                            break;
                        }
                    }
                    if (yWall != -1)
                    {
                        break;
                    }
                }
                if (xWall + 2 < horizontalWallArray[yWall].Length)
                {
                    horizontalWallArray[yWall][xWall].Visible = false;
                    horizontalWallArray[yWall][xWall + 1].Enabled = false;
                    horizontalWallArray[yWall][xWall + 2].Visible = false;
                    horizontalWallArray[yWall][xWall].BackColor = Color.Black;
                    horizontalWallArray[yWall][xWall + 1].BackColor = Color.Black;
                    horizontalWallArray[yWall][xWall + 2].BackColor = Color.Black;
                    walls--;
                }
            }
        }

        public  void EnterWall(WallLabel wall)
        {
            int xWall = -1, yWall = -1;

            for (int i = 0; i < verticalWallArray.GetLength(0); i++)
            {
                foreach (Control element in verticalWallArray[i])
                {
                    xWall = Array.IndexOf(verticalWallArray[i], wall);
                    if (xWall != -1)
                    {
                        yWall = i;
                        break;
                    }
                }
                if (yWall != -1)
                {
                    break;
                }
            }
            if (yWall != -1)
            {
                if (xWall + 2 < verticalWallArray[yWall].Length)
                {
                    verticalWallArray[yWall][xWall].BackColor = Color.Red;
                    verticalWallArray[yWall][xWall + 2].BackColor = Color.Red;
                }
            }
            else
            {
                for (int i = 0; i < horizontalWallArray.GetLength(0); i++)
                {
                    foreach (Control element in horizontalWallArray[i])
                    {
                        xWall = Array.IndexOf(horizontalWallArray[i], wall);
                        if (xWall != -1)
                        {
                            yWall = i;
                            break;
                        }
                    }
                    if (yWall != -1)
                    {
                        break;
                    }
                }
                if (xWall + 2 < horizontalWallArray[yWall].Length)
                {
                    horizontalWallArray[yWall][xWall].BackColor = Color.Red;
                    horizontalWallArray[yWall][xWall + 2].BackColor = Color.Red;
                }
            }


        }

        public  void LeaveWall(WallLabel wall)
        {
            int xWall = -1, yWall = -1;

            for (int i = 0; i < verticalWallArray.GetLength(0); i++)
            {
                foreach (Control element in verticalWallArray[i])
                {
                    xWall = Array.IndexOf(verticalWallArray[i], wall);
                    if (xWall != -1)
                    {
                        yWall = i;
                        break;
                    }
                }
                if (yWall != -1)
                {
                    break;
                }
            }
            if (yWall != -1)
            {
                if (xWall + 2 < verticalWallArray[yWall].Length)
                {
                    verticalWallArray[yWall][xWall].BackColor = Color.Gray;
                    verticalWallArray[yWall][xWall + 2].BackColor = Color.Gray;
                }
            }
            else
            {
                for (int i = 0; i < horizontalWallArray.GetLength(0); i++)
                {
                    foreach (Control element in horizontalWallArray[i])
                    {
                        xWall = Array.IndexOf(horizontalWallArray[i], wall);
                        if (xWall != -1)
                        {
                            yWall = i;
                            break;
                        }
                    }
                    if (yWall != -1)
                    {
                        break;
                    }
                }
                if (xWall + 2 < horizontalWallArray[yWall].Length)
                {
                    horizontalWallArray[yWall][xWall].BackColor = Color.Gray;
                    horizontalWallArray[yWall][xWall + 2].BackColor = Color.Gray;
                }
            }


        }
    }
}
