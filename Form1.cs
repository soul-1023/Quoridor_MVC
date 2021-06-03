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


namespace Quoridor_MVC
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            Oleg oleg = new Oleg();
            InitializeComponent();
            oleg.Start(labelWallsYou, buttonWall);
            button1.MouseClick += (s, e) => oleg.Button1_Click(buttonWall, button1, labelWallsYou, labelWallsEnemy2, labelWallsEnemy3, radioButton1,radioButton2, radioButton3);
            buttonWall.MouseClick += (s, e) => oleg.ButtonSpendWall_Click(s, e);
            this.Controls.Add(oleg.tableLayoutPanel);
            this.Controls.Add(oleg.labelWallsEnemy);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
