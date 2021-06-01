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

//Это высрал Саша Кусь, при этом мне сосал Вадим

namespace Quoridor_MVC
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            Oleg oleg = new Oleg();
            InitializeComponent();
            oleg.Start(tableLayoutPanel1, labelWallsYou, buttonWall);
            button1.MouseClick += (s, e) => oleg.Button1_Click(tableLayoutPanel1, buttonWall, button1, labelWallsYou, labelWallsEnemy1, labelWallsEnemy2, labelWallsEnemy3, radioButton1,radioButton2, radioButton3);
            buttonWall.MouseClick += (s, e) => oleg.ButtonSpendWall_Click(s, e);
        }
    }
}
