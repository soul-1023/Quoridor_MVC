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
            int count = 1;
            new System.Windows.Forms.Padding(0);
            tableLayoutPanel1.RowCount = (2 * graph.Vertexes.GetLength(1)) - 1;
            tableLayoutPanel1.ColumnCount = (2 * graph.Vertexes.GetLength(0)) - 1;
            tableLayoutPanel1.BackColor = Color.Black;
            for (int i = 0; i < tableLayoutPanel1.RowCount; i = i + 2)
            {
                for (int j = 0; j < tableLayoutPanel1.ColumnCount; j = j + 2)
                {
                    Label label = new Label();
                    tableLayoutPanel1.Controls.Add(label, i, j);
                    label.BackColor = Color.White;
                    label.Dock = DockStyle.Fill;
                    label.Height = 60;
                    label.Width = 60;
                    label.Margin = Padding;
                    label.Click += (s, e) => label_Click_vertex(label);
                    label.MouseEnter += (s, e) => label_mouse_enter_vertex(label);
                    label.MouseLeave += (s, e) => label_mouse_leave_vertex(label);
                    vertexLabelList.Add(label);
                }
            }
            for (int j = 1; j < tableLayoutPanel1.RowCount; j = j + 2)
            {
                for (int i = 0; i < tableLayoutPanel1.ColumnCount; i = i + 2)
                {
                    Label label = new Label();
                    tableLayoutPanel1.Controls.Add(label, i, j);
                    label.BackColor = Color.Gray;
                    label.Dock = DockStyle.Fill;
                    label.Width = 20;
                    label.Height = 20;
                    label.Margin = Padding;
                    horizontalLabelList.Add(label);

                    label.Click += (s, e) => label_Click_wall(label);
                    label.MouseEnter += (s, e) => label_mouse_enter_wall(label);
                    label.MouseLeave += (s, e) => label_mouse_leave_wall(label);
                }
            }
            for (int i = 1; i < tableLayoutPanel1.RowCount; i = i + 2)
            {
                for (int j = 0; j < tableLayoutPanel1.ColumnCount; j = j + 2)
                {
                    Label label = new Label();
                    tableLayoutPanel1.Controls.Add(label, i, j);
                    label.BackColor = Color.Gray;
                    label.Dock = DockStyle.Fill;
                    label.Width = 20;
                    label.Margin = Padding;
                    verticalLabelList.Add(label);

                    label.Click += (s, e) => label_Click_wall(label);
                    label.MouseEnter += (s, e) => label_mouse_enter_wall(label);
                    label.MouseLeave += (s, e) => label_mouse_leave_wall(label);
                }
            }
            //for (int i = 1; i < tableLayoutPanel1.RowCount; i = i + 2)
            //{
            //    for (int j = 1; j < tableLayoutPanel1.ColumnCount; j = j + 2)
            //    {
            //        Label label = new Label();
            //        tableLayoutPanel1.Controls.Add(label, i, j);
            //        label.BackColor = Color.Gray;
            //        label.Dock = DockStyle.Fill;
            //        label.Width = 20;
            //        label.Height = 20;
            //        label.Margin = Padding;
            //        //label1.Text = Convert.ToString(count);
            //        verticalLabelList.Insert(count, label);
            //        count += 2;
            //    }
            //    count++;
            //}
            //for (int i = 0; i < verticalLabelList.Count; i++)
            //{
            //    verticalLabelList[i].Text = Convert.ToString(i);
            //}
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label_Click_vertex(Label label)
        {
            if (wallSpend != true)
            {

                playerVertex.BackColor = Color.White;
                Can_Move_Cancel_Select(playerVertex);
                playerVertex = label;
                Can_Move_Select(playerVertex);
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
            tableLayoutPanel1.Enabled = true;
            tableLayoutPanel1.Visible = true;
            buttonWall.Enabled = true;
            buttonWall.Visible = true;
            button1.Visible = false;
            button1.Enabled = false;
            labelWallsYou.Text = "You walls: 10";
            labelWallsEnemy.Text = "Enemy walls: 10";
            vertexLabelList[32].BackColor = Color.Orange;
            vertexLabelList[39].BackColor = Color.Fuchsia;
            playerVertex = vertexLabelList[39];
            Can_Move_Select(playerVertex);
        }

        public void ButtonSpendWall_Click(object sender, EventArgs e)
        {
            if ((wallSpend != true) && (walls > 0))
            {
                wallSpend = true;
                buttonWall.Text = "Cancel wall";
                Can_Move_Cancel_Select(playerVertex);
            }
            else
            {
                wallSpend = false;
                buttonWall.Text = "Wall";
                Can_Move_Select(playerVertex);
            }
        }

        public void Can_Move_Select(Control label)
        {
            int indexOf = vertexLabelList.IndexOf(label);
            try
            {
                if ((indexOf - 1) % graph.Vertexes.GetLength(0) != graph.Vertexes.GetLength(0) - 1)
                {
                    vertexLabelList[indexOf - 1].BackColor = Color.Aqua;
                }
            }
            catch { }
            try
            {
                if ((indexOf + 1) % graph.Vertexes.GetLength(0) != 0)
                {


                    vertexLabelList[indexOf + 1].BackColor = Color.Aqua;
                }
            }
            catch { }
            try
            {
                vertexLabelList[indexOf - graph.Vertexes.GetLength(0)].BackColor = Color.Aqua;
            }
            catch { }
            try
            {
                vertexLabelList[indexOf + graph.Vertexes.GetLength(0)].BackColor = Color.Aqua;
            }
            catch { }
        }

        public void Can_Move_Cancel_Select(Control label)
        {
            int indexOf = vertexLabelList.IndexOf(label);
            try
            {
                if ((indexOf - 1) % graph.Vertexes.GetLength(0) != graph.Vertexes.GetLength(0) - 1)
                {
                    vertexLabelList[indexOf - 1].BackColor = Color.White;
                }
            }
            catch { }
            try
            {
                if ((indexOf + 1) % graph.Vertexes.GetLength(0) != 0)
                {


                    vertexLabelList[indexOf + 1].BackColor = Color.White;
                }
            }
            catch { }
            try
            {
                vertexLabelList[indexOf - graph.Vertexes.GetLength(0)].BackColor = Color.White;
            }
            catch { }
            try
            {
                vertexLabelList[indexOf + graph.Vertexes.GetLength(0)].BackColor = Color.White;
            }
            catch { }
        }
    }
}
