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

        private GameRouter gameRouter;
        private Graph graph = new Models.Graph(8);
        public VertexLabel[][] vertexLabelList1;
        public Control[][] horizontalWallArray;
        public Control[][] verticalWallArray;

        bool wallSpend = false;
        public int walls = 10;

        public  int xE = 4;
        public  int yE = 0;
        public bool isPlayerTurn = true;
        public  Button buttonWall;

        List<VertexLabel> vertexes = new List<VertexLabel>();

        VertexLabel currentVertex;

        public Oleg()
        {

        }

        //Вызывается при старте игры, рисует поле, заполняет массивы (array), задает координаты лейблам
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
                        VertexLabel vertex = new VertexLabel(i/2, j/2);
                        table.Controls.Add(vertex, i, j);
                        //vertex.BackColor = Color.White;
                        //vertex.Dock = DockStyle.Fill;
                        //vertex.Height = 60;
                        //vertex.Width = 60;
                        //vertex.Margin = Padding;
                        SetFormat(vertex, 60, Color.White);
                        vertex.Click += (s, e) => label_Click_vertex(vertex);
                        vertexLabelList1[yVertex][xVertex] = vertex;
                        xVertex++;
                        switchYVertex = true;
                        vertex.Enabled = false;
                        vertexes.Add(vertex);
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
        }

        //Убрать все интерактивные ячейки
        private void SetDefaultLabels()
        {
            foreach (VertexLabel vertex in vertexes)
            {
                vertex.Enabled = false;
                if (vertex.isCharacter == false)
                    vertex.BackColor = Color.White;
            }

        }

        //Кнопка СТАРТ
        public void Button1_Click(TableLayoutPanel table, Button buttonWall, Button button1, Label labelWallsYou, Label labelWallsEnemy1, Label labelWallsEnemy2, Label labelWallsEnemy3, RadioButton radioButton1, RadioButton radioButton2, RadioButton radioButton3)
        {

            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            int count = 1;

            if (radioButton3.Checked == true)
            {
                count = 4;
                labelWallsEnemy1.Text = "Enemy Orange walls: 10";
                labelWallsEnemy2.Text = "Enemy Coral walls: 10";
                labelWallsEnemy3.Text = "Enemy Crimson walls: 10";
            }
            else if (radioButton2.Checked == true)
            {
                count = 3;
                labelWallsEnemy1.Text = "Enemy Orange walls: 10";
                labelWallsEnemy2.Text = "Enemy Coral walls: 10";
            }
            else
            {
                count = 2;
                labelWallsEnemy1.Text = "Enemy Orange walls: 10";
            }

            gameRouter = new GameRouter(table, 8, count);

            SetCharacterStart();
            
            //CanMove(vertexLabelList1[x][y]);
            table.Visible = true;
            buttonWall.Enabled = true;
            buttonWall.Visible = true;
            button1.Visible = false;
            button1.Enabled = false;
            labelWallsYou.Text = "Your walls: " + walls;

        }

        //Метод устанавливает цветоразмер лейблов и ихъ производных. Призывается в старте несколько раз
        public  void SetFormat(Control control, int size, Color color)
        {
            Padding padding =  new Padding(0);
            control.BackColor = color;
            control.Dock = DockStyle.Fill;
            control.Height = size;
            control.Width = size;
            control.Margin = padding;
        }

        //Ставит играков на старт
        public void SetCharacterStart()
        {
            Color[] enemyColors = new Color[] { Color.Orange, Color.Coral, Color.Crimson };
            int i = 0;
            var characters = gameRouter.GetCharacters();

            foreach (AbstractCharacter character in characters)
            {
                if (character is Player)
                {
                    vertexLabelList1[character.CurrentPosition.x][character.CurrentPosition.y].BackColor = Color.Green;
                }
                else
                {
                    vertexLabelList1[character.CurrentPosition.x][character.CurrentPosition.y].BackColor = enemyColors[i++];
                }

                vertexLabelList1[character.CurrentPosition.x][character.CurrentPosition.y].isCharacter = true;
            }

            if (!(characters[0] is Player))
                gameRouter.gameActivities.ActionOfAI();

            SetInteractiveLabels();
        }

        //Определение интерактивных ячеек
        public void SetInteractiveLabels()
        {
           foreach (Coords coord in gameRouter.GetPlayerEdges())
           {
                vertexLabelList1[coord.y][coord.x].BackColor = Color.GreenYellow;
                vertexLabelList1[coord.y][coord.x].Enabled = true;
           }
        }

        //Движение клик
        private void label_Click_vertex(VertexLabel vertex)
        {
            if (wallSpend != true)
            {
                //Перерисовать позицию
                SetCharacter(gameRouter.gameActivities.GetActiveCharacter().CurrentPosition.x,
                    gameRouter.gameActivities.GetActiveCharacter().CurrentPosition.y, vertex);
                //Сделать все лейблы неинтерактивными
                SetDefaultLabels();
                //Действия ИИ
                gameRouter.HandleAction((currentVertex.position, vertex.position));

                //Установка интерактивных ячеек
                SetInteractiveLabels();
            }
        }

        //Переставить персонажа
        public void SetCharacter(int x, int y, VertexLabel vertex)
        {
            currentVertex = vertexLabelList1[x][y];
            Color color = currentVertex.BackColor;
            currentVertex.isCharacter = false;
            currentVertex.BackColor = Color.White;
            vertex.isCharacter = true;
            vertex.BackColor = color;
        }

        //Установка стен клик
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

        //Вход курсора на стену
        private void label_mouse_enter_wall(WallLabel wall)
        {
            if (wallSpend == true)
            {
                EnterWall(wall);
            }
        }

        //Курсор покидает стену
        private void label_mouse_leave_wall(WallLabel wall)
        {
            if (wallSpend == true)
            {
                LeaveWall(wall);
            }
        }

        //Кнопка установить стену клик
        public void ButtonSpendWall_Click(object sender, EventArgs e)
        {
            if ((wallSpend != true) && (walls > 0))
            {
                wallSpend = true;
                buttonWall.Text = "Cancel wall";
                SetDefaultLabels();
            }
            else
            {
                wallSpend = false;
                buttonWall.Text = "Wall";
                SetInteractiveLabels();
            }
        }

        //Установить стену. Вызывается в установить стену клик
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

        //Закрашивает стену при наведении
        public void EnterWall(WallLabel wall)
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

        //Открашивает стену при наведении
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
