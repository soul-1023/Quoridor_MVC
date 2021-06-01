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
            this.labelWallsEnemy1 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.labelWallsEnemy2 = new System.Windows.Forms.Label();
            this.labelWallsEnemy3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.labelGreen = new System.Windows.Forms.Label();
            this.labelOrange = new System.Windows.Forms.Label();
            this.labelCoral = new System.Windows.Forms.Label();
            this.labelCrymson = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(77, 61);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Visible = false;
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
            this.button1.Location = new System.Drawing.Point(316, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(232, 59);
            this.button1.TabIndex = 2;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
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
            // 
            // labelWallsEnemy1
            // 
            this.labelWallsEnemy1.AutoSize = true;
            this.labelWallsEnemy1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWallsEnemy1.Location = new System.Drawing.Point(649, 216);
            this.labelWallsEnemy1.Name = "labelWallsEnemy1";
            this.labelWallsEnemy1.Size = new System.Drawing.Size(0, 25);
            this.labelWallsEnemy1.TabIndex = 1;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton1.Location = new System.Drawing.Point(316, 181);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(107, 29);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1 player";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton2.Location = new System.Drawing.Point(316, 216);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(118, 29);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "2 players";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton3.Location = new System.Drawing.Point(316, 251);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(118, 29);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "3 players";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // labelWallsEnemy2
            // 
            this.labelWallsEnemy2.AutoSize = true;
            this.labelWallsEnemy2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWallsEnemy2.Location = new System.Drawing.Point(649, 253);
            this.labelWallsEnemy2.Name = "labelWallsEnemy2";
            this.labelWallsEnemy2.Size = new System.Drawing.Size(0, 25);
            this.labelWallsEnemy2.TabIndex = 1;
            // 
            // labelWallsEnemy3
            // 
            this.labelWallsEnemy3.AutoSize = true;
            this.labelWallsEnemy3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWallsEnemy3.Location = new System.Drawing.Point(649, 288);
            this.labelWallsEnemy3.Name = "labelWallsEnemy3";
            this.labelWallsEnemy3.Size = new System.Drawing.Size(0, 25);
            this.labelWallsEnemy3.TabIndex = 1;
            // 
            // labelGreen
            // 
            this.labelGreen.AutoSize = true;
            this.labelGreen.BackColor = System.Drawing.Color.Green;
            this.labelGreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGreen.Location = new System.Drawing.Point(613, 174);
            this.labelGreen.Name = "labelGreen";
            this.labelGreen.Size = new System.Drawing.Size(30, 25);
            this.labelGreen.TabIndex = 4;
            this.labelGreen.Text = "   ";
            this.labelGreen.Visible = false;
            // 
            // labelOrange
            // 
            this.labelOrange.AutoSize = true;
            this.labelOrange.BackColor = System.Drawing.Color.Orange;
            this.labelOrange.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelOrange.Location = new System.Drawing.Point(613, 216);
            this.labelOrange.Name = "labelOrange";
            this.labelOrange.Size = new System.Drawing.Size(30, 25);
            this.labelOrange.TabIndex = 4;
            this.labelOrange.Text = "   ";
            this.labelOrange.Visible = false;
            // 
            // labelCoral
            // 
            this.labelCoral.AutoSize = true;
            this.labelCoral.BackColor = System.Drawing.Color.Coral;
            this.labelCoral.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCoral.Location = new System.Drawing.Point(613, 253);
            this.labelCoral.Name = "labelCoral";
            this.labelCoral.Size = new System.Drawing.Size(30, 25);
            this.labelCoral.TabIndex = 4;
            this.labelCoral.Text = "   ";
            this.labelCoral.Visible = false;
            // 
            // labelCrymson
            // 
            this.labelCrymson.AutoSize = true;
            this.labelCrymson.BackColor = System.Drawing.Color.Crimson;
            this.labelCrymson.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCrymson.Location = new System.Drawing.Point(613, 288);
            this.labelCrymson.Name = "labelCrymson";
            this.labelCrymson.Size = new System.Drawing.Size(30, 25);
            this.labelCrymson.TabIndex = 4;
            this.labelCrymson.Text = "   ";
            this.labelCrymson.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(898, 558);
            this.Controls.Add(this.labelCrymson);
            this.Controls.Add(this.labelCoral);
            this.Controls.Add(this.labelOrange);
            this.Controls.Add(this.labelGreen);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.buttonWall);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelWallsEnemy3);
            this.Controls.Add(this.labelWallsEnemy2);
            this.Controls.Add(this.labelWallsEnemy1);
            this.Controls.Add(this.labelWallsYou);
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonWall;
        private System.Windows.Forms.Label labelWallsEnemy1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Label labelWallsYou;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label labelWallsEnemy2;
        private System.Windows.Forms.Label labelWallsEnemy3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label labelGreen;
        private System.Windows.Forms.Label labelOrange;
        private System.Windows.Forms.Label labelCoral;
        private System.Windows.Forms.Label labelCrymson;
    }
}

