namespace Quoridor_MVC
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelWallsYou = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonWall = new System.Windows.Forms.Button();
            this.labelWallsEnemy = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Enabled = false;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(132, 77);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Visible = false;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.TableLayoutPanel1_Paint);
            // 
            // labelWallsYou
            // 
            this.labelWallsYou.AutoSize = true;
            this.labelWallsYou.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWallsYou.Location = new System.Drawing.Point(649, 174);
            this.labelWallsYou.Name = "labelWallsYou";
            this.labelWallsYou.Size = new System.Drawing.Size(0, 25);
            this.labelWallsYou.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(654, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(232, 59);
            this.button1.TabIndex = 2;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // buttonWall
            // 
            this.buttonWall.Enabled = false;
            this.buttonWall.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonWall.Location = new System.Drawing.Point(654, 112);
            this.buttonWall.Name = "buttonWall";
            this.buttonWall.Size = new System.Drawing.Size(232, 59);
            this.buttonWall.TabIndex = 2;
            this.buttonWall.Text = "Wall";
            this.buttonWall.UseVisualStyleBackColor = true;
            this.buttonWall.Visible = false;
            this.buttonWall.Click += new System.EventHandler(this.ButtonSpendWall_Click);
            // 
            // labelWallsEnemy
            // 
            this.labelWallsEnemy.AutoSize = true;
            this.labelWallsEnemy.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWallsEnemy.Location = new System.Drawing.Point(649, 216);
            this.labelWallsEnemy.Name = "labelWallsEnemy";
            this.labelWallsEnemy.Size = new System.Drawing.Size(0, 25);
            this.labelWallsEnemy.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(898, 558);
            this.Controls.Add(this.buttonWall);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelWallsEnemy);
            this.Controls.Add(this.labelWallsYou);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelWallsYou;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonWall;
        private System.Windows.Forms.Label labelWallsEnemy;
    }
}

