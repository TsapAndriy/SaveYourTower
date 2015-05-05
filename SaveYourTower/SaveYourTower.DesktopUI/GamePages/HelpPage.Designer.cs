namespace SaveYourTower.DesktopUI.GamePages
{
    partial class HelpPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pctEnemy = new System.Windows.Forms.PictureBox();
            this.pctTurret = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctEnemy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctTurret)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.91954F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.08046F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Controls.Add(this.pctEnemy, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pctTurret, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.31034F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.68966F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 193F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(389, 405);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pctEnemy
            // 
            this.pctEnemy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pctEnemy.Image = global::SaveYourTower.DesktopUI.Properties.Resources.top;
            this.pctEnemy.InitialImage = global::SaveYourTower.DesktopUI.Properties.Resources.Grass;
            this.pctEnemy.Location = new System.Drawing.Point(3, 3);
            this.pctEnemy.Name = "pctEnemy";
            this.pctEnemy.Size = new System.Drawing.Size(32, 79);
            this.pctEnemy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pctEnemy.TabIndex = 3;
            this.pctEnemy.TabStop = false;
            this.pctEnemy.Click += new System.EventHandler(this.pctEnemy_Click);
            // 
            // pctTurret
            // 
            this.pctTurret.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pctTurret.Image = global::SaveYourTower.DesktopUI.Properties.Resources.default_30;
            this.pctTurret.InitialImage = global::SaveYourTower.DesktopUI.Properties.Resources.default_30;
            this.pctTurret.Location = new System.Drawing.Point(3, 88);
            this.pctTurret.Name = "pctTurret";
            this.pctTurret.Size = new System.Drawing.Size(32, 66);
            this.pctTurret.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pctTurret.TabIndex = 3;
            this.pctTurret.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.InitialImage = global::SaveYourTower.DesktopUI.Properties.Resources.default_30;
            this.pictureBox1.Location = new System.Drawing.Point(3, 160);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 52);
            this.label1.TabIndex = 5;
            this.label1.Text = "Cruel enemies\r\n\r\nThey want to destroy YOUR tower. \r\nYou must kill them to win.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 39);
            this.label2.TabIndex = 6;
            this.label2.Text = "Brawe Tower.\r\n\r\nMain thing in the game. Save it and get a gift).";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(240, 65);
            this.label3.TabIndex = 7;
            this.label3.Text = "Helpfull turrets.\r\n\r\nIf you have anought points, you can buy\r\nthis awesome turren" +
    "t. And they will defend you to \r\nthe last bit of thear hearts.";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(41, 214);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // HelpPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "HelpPage";
            this.Size = new System.Drawing.Size(395, 411);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctEnemy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctTurret)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pctTurret;
        private System.Windows.Forms.PictureBox pctEnemy;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

    }
}
