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
            InitializeComponent(); 

            Oleg oleg = new Oleg();

            oleg.Start(tableLayoutPanel1, labelWallsYou, SetWallBtn);
            PlayBtn.MouseClick += (s, e) => oleg.Button1_Click(
                tableLayoutPanel1, 
                SetWallBtn, 
                PlayBtn, 
                labelWallsYou, 
                labelWallsEnemy
            );

            SetWallBtn.MouseClick += (s, e) => oleg.ButtonSpendWall_Click(s, e);
        }

    }
}
