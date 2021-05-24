using Quoridor_MVC.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quoridor_MVC
{
    public partial class Form1 : Form
    {
        Models.Graph graph = new Models.Graph(8);
        List<Control> verticalLabelList = new List<Control>();
        List<Control> horizontalLabelList = new List<Control>();
        List<Control> vertexLabelList = new List<Control>();
        bool wallSpend = false;
        int walls = 10;
        Control playerVertex;

        public Form1()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Game game = new Game(tableLayoutPanel1, 8, 2);
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label_Click_vertex(Label label)
        {
            if (wallSpend != true)
            {

                playerVertex.BackColor = Color.White;
                playerVertex = label;
                label.BackColor = Color.Fuchsia;
            }
        }

        private void label_mouse_enter_vertex(Label label)
        {
            if ((wallSpend != true) && (label.BackColor == Color.White))
            {
                label.BackColor = Color.Wheat;
            }
        }

        private void label_mouse_leave_vertex(Label label)
        {
            if (label.BackColor == Color.Wheat)
            {
                label.BackColor = Color.White;
            }
        }

        public void label_Click_wall(Label label)
        {
            if ((wallSpend == true) && (walls > 0))
            {
                int indexOf = horizontalLabelList.IndexOf(label);
                if (indexOf % graph.Vertexes.GetLength(1) != graph.Vertexes.GetLength(1) - 1)
                {
                    if (indexOf != -1)
                    {
                        //label.Text = Convert.ToString("Wall");
                        label.Visible = false;
                        label.MouseLeave += (s, e) => label_mouse_leave_wall(label);
                        horizontalLabelList[indexOf + 1].MouseLeave += (s, e) => label_mouse_leave_wall(label);
                        horizontalLabelList[indexOf + 1].Visible = false;
                        //horizontalLabelList[indexOf + 1].Text = "Wall";
                        walls--;
                        labelWallsYou.Text = "You walls: " + walls;
                    }
                    else
                    {

                        indexOf = verticalLabelList.IndexOf(label);
                        if (indexOf % graph.Vertexes.GetLength(1) != graph.Vertexes.GetLength(1) - 1)
                        {
                            //label.Text = "Wall";
                            label.Visible = false;
                            label.MouseLeave += (s, e) => label_mouse_leave_wall(label);
                            verticalLabelList[indexOf + 1].MouseLeave += (s, e) => label_mouse_leave_wall(label);
                            //verticalLabelList[indexOf + 1].Text = "Wall";
                            verticalLabelList[indexOf + 1].Visible = false;
                            //verticalLabelList[indexOf + 2].BackColor = Color.Red;
                            walls--;
                            labelWallsYou.Text = "You walls: " + walls;
                        }
                    }

                }
            }
        }

        private void label_mouse_enter_wall(Label label)
        {
            if (wallSpend == true)
            {
                int indexOf = horizontalLabelList.IndexOf(label);
                if (indexOf % graph.Vertexes.GetLength(1) != graph.Vertexes.GetLength(1) - 1)
                {
                    if (indexOf != -1)
                    {
                        label.BackColor = Color.Red;
                        horizontalLabelList[indexOf + 1].BackColor = Color.Red;
                    }
                    else
                    {
                        indexOf = verticalLabelList.IndexOf(label);
                        if (indexOf % graph.Vertexes.GetLength(1) != graph.Vertexes.GetLength(1) - 1)
                        {
                            label.BackColor = Color.Red;
                            verticalLabelList[indexOf + 1].BackColor = Color.Red;
                            //verticalLabelList[indexOf + 2].BackColor = Color.Red;
                        }
                    }
                }
            }
        }

        private void label_mouse_leave_wall(Label label)
        {
            if (wallSpend == true)
            {
                int indexOf = horizontalLabelList.IndexOf(label);
                if (indexOf % graph.Vertexes.GetLength(1) != graph.Vertexes.GetLength(1) - 1)
                {
                    if (indexOf != -1)
                    {
                        label.BackColor = Color.Gray;
                        horizontalLabelList[indexOf + 1].BackColor = Color.Gray;
                    }
                    else
                    {
                        indexOf = verticalLabelList.IndexOf(label);
                        if (indexOf % graph.Vertexes.GetLength(0) != graph.Vertexes.GetLength(0) - 1)
                        {
                            label.BackColor = Color.Gray;
                            verticalLabelList[indexOf + 1].BackColor = Color.Gray;
                            //verticalLabelList[indexOf + 2].BackColor = Color.Gray;
                        }
                    }
                }
            }
        }

        public void Button1_Click(object sender, EventArgs e)
        {
            //tableLayoutPanel1.Enabled = true;
            //tableLayoutPanel1.Visible = true;
            //buttonWall.Enabled = true;
            //buttonWall.Visible = true;
            //button1.Visible = false;
            //button1.Enabled = false;
            //labelWallsYou.Text = "You walls: 10";
            //labelWallsEnemy.Text = "Enemy walls: 10";
            //vertexLabelList[32].BackColor = Color.Orange;
            //vertexLabelList[39].BackColor = Color.Fuchsia;
            //playerVertex = vertexLabelList[39];
            //Can_Move_Select(playerVertex);
        }

        public void ButtonSpendWall_Click(object sender, EventArgs e)
        {
            if ((wallSpend != true) && (walls > 0))
            {
                wallSpend = true;
                buttonWall.Text = "Cancel wall";
            }
            else
            {
                wallSpend = false;
                buttonWall.Text = "Wall";
            }
        }
    }
}
