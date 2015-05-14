namespace SaveYourTower.DesktopUI.GamePages
{
    partial class SettingsPage
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
            this.dbLayoutPanel1 = new GameUserElements.DBLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFullScreen = new System.Windows.Forms.Label();
            this.dbLayoutPanel2 = new GameUserElements.DBLayoutPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.dbLayoutPanel3 = new GameUserElements.DBLayoutPanel();
            this.lblDifficulty = new System.Windows.Forms.Label();
            this.lblPrevDifficulty = new System.Windows.Forms.Label();
            this.lblNextDifficulty = new System.Windows.Forms.Label();
            this.dbLayoutPanel1.SuspendLayout();
            this.dbLayoutPanel2.SuspendLayout();
            this.dbLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dbLayoutPanel1
            // 
            this.dbLayoutPanel1.BackColor = System.Drawing.Color.Black;
            this.dbLayoutPanel1.ColumnCount = 5;
            this.dbLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.03389F));
            this.dbLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.dbLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.dbLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.dbLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.96611F));
            this.dbLayoutPanel1.Controls.Add(this.label2, 1, 3);
            this.dbLayoutPanel1.Controls.Add(this.label1, 1, 2);
            this.dbLayoutPanel1.Controls.Add(this.lblFullScreen, 3, 2);
            this.dbLayoutPanel1.Controls.Add(this.dbLayoutPanel2, 1, 8);
            this.dbLayoutPanel1.Controls.Add(this.dbLayoutPanel3, 3, 3);
            this.dbLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.dbLayoutPanel1.Name = "dbLayoutPanel1";
            this.dbLayoutPanel1.RowCount = 10;
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.dbLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.dbLayoutPanel1.Size = new System.Drawing.Size(500, 500);
            this.dbLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Agency FB", 25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Lime;
            this.label2.Location = new System.Drawing.Point(109, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 49);
            this.label2.TabIndex = 1;
            this.label2.Text = "Difficulty :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Agency FB", 25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(109, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 49);
            this.label1.TabIndex = 0;
            this.label1.Text = "FullScreen :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFullScreen
            // 
            this.lblFullScreen.AutoSize = true;
            this.lblFullScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFullScreen.Font = new System.Drawing.Font("Agency FB", 25F, System.Drawing.FontStyle.Bold);
            this.lblFullScreen.ForeColor = System.Drawing.Color.Lime;
            this.lblFullScreen.Location = new System.Drawing.Point(269, 98);
            this.lblFullScreen.Name = "lblFullScreen";
            this.lblFullScreen.Size = new System.Drawing.Size(107, 49);
            this.lblFullScreen.TabIndex = 2;
            this.lblFullScreen.Text = "false";
            this.lblFullScreen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFullScreen.Click += new System.EventHandler(this.lblFullScreen_Click);
            this.lblFullScreen.MouseEnter += new System.EventHandler(this.lblFullScreen_MouseEnter);
            // 
            // dbLayoutPanel2
            // 
            this.dbLayoutPanel2.ColumnCount = 3;
            this.dbLayoutPanel1.SetColumnSpan(this.dbLayoutPanel2, 3);
            this.dbLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dbLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.dbLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dbLayoutPanel2.Controls.Add(this.btnExit, 1, 0);
            this.dbLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbLayoutPanel2.Location = new System.Drawing.Point(109, 395);
            this.dbLayoutPanel2.Name = "dbLayoutPanel2";
            this.dbLayoutPanel2.RowCount = 1;
            this.dbLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dbLayoutPanel2.Size = new System.Drawing.Size(267, 52);
            this.dbLayoutPanel2.TabIndex = 5;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Agency FB", 25F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.Lime;
            this.btnExit.Location = new System.Drawing.Point(33, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(200, 52);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Back";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.MouseEnter += new System.EventHandler(this.btnExit_MouseEnter);
            // 
            // dbLayoutPanel3
            // 
            this.dbLayoutPanel3.ColumnCount = 3;
            this.dbLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.39024F));
            this.dbLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.21952F));
            this.dbLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.39024F));
            this.dbLayoutPanel3.Controls.Add(this.lblDifficulty, 0, 0);
            this.dbLayoutPanel3.Controls.Add(this.lblPrevDifficulty, 0, 0);
            this.dbLayoutPanel3.Controls.Add(this.lblNextDifficulty, 1, 0);
            this.dbLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbLayoutPanel3.Location = new System.Drawing.Point(269, 150);
            this.dbLayoutPanel3.Name = "dbLayoutPanel3";
            this.dbLayoutPanel3.RowCount = 1;
            this.dbLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dbLayoutPanel3.Size = new System.Drawing.Size(107, 43);
            this.dbLayoutPanel3.TabIndex = 6;
            // 
            // lblDifficulty
            // 
            this.lblDifficulty.AutoSize = true;
            this.lblDifficulty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDifficulty.Font = new System.Drawing.Font("Agency FB", 25F, System.Drawing.FontStyle.Bold);
            this.lblDifficulty.ForeColor = System.Drawing.Color.Lime;
            this.lblDifficulty.Location = new System.Drawing.Point(29, 0);
            this.lblDifficulty.Name = "lblDifficulty";
            this.lblDifficulty.Size = new System.Drawing.Size(48, 43);
            this.lblDifficulty.TabIndex = 5;
            this.lblDifficulty.Text = "0";
            this.lblDifficulty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDifficulty.Visible = false;
            // 
            // lblPrevDifficulty
            // 
            this.lblPrevDifficulty.AutoSize = true;
            this.lblPrevDifficulty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrevDifficulty.Font = new System.Drawing.Font("Agency FB", 25F, System.Drawing.FontStyle.Bold);
            this.lblPrevDifficulty.ForeColor = System.Drawing.Color.Lime;
            this.lblPrevDifficulty.Location = new System.Drawing.Point(3, 0);
            this.lblPrevDifficulty.Name = "lblPrevDifficulty";
            this.lblPrevDifficulty.Size = new System.Drawing.Size(20, 43);
            this.lblPrevDifficulty.TabIndex = 4;
            this.lblPrevDifficulty.Text = "<";
            this.lblPrevDifficulty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPrevDifficulty.Visible = false;
            this.lblPrevDifficulty.Click += new System.EventHandler(this.lblPrevDifficulty_Click);
            this.lblPrevDifficulty.MouseEnter += new System.EventHandler(this.lblPrevDifficulty_MouseEnter);
            // 
            // lblNextDifficulty
            // 
            this.lblNextDifficulty.AutoSize = true;
            this.lblNextDifficulty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNextDifficulty.Font = new System.Drawing.Font("Agency FB", 25F, System.Drawing.FontStyle.Bold);
            this.lblNextDifficulty.ForeColor = System.Drawing.Color.Lime;
            this.lblNextDifficulty.Location = new System.Drawing.Point(83, 0);
            this.lblNextDifficulty.Name = "lblNextDifficulty";
            this.lblNextDifficulty.Size = new System.Drawing.Size(21, 43);
            this.lblNextDifficulty.TabIndex = 3;
            this.lblNextDifficulty.Text = ">";
            this.lblNextDifficulty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNextDifficulty.Visible = false;
            this.lblNextDifficulty.Click += new System.EventHandler(this.lblNextDifficulty_Click);
            this.lblNextDifficulty.MouseEnter += new System.EventHandler(this.lblNextDifficulty_MouseEnter);
            // 
            // SettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.Controls.Add(this.dbLayoutPanel1);
            this.Name = "SettingsPage";
            this.Size = new System.Drawing.Size(500, 500);
            this.dbLayoutPanel1.ResumeLayout(false);
            this.dbLayoutPanel1.PerformLayout();
            this.dbLayoutPanel2.ResumeLayout(false);
            this.dbLayoutPanel3.ResumeLayout(false);
            this.dbLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GameUserElements.DBLayoutPanel dbLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFullScreen;
        private System.Windows.Forms.Label lblNextDifficulty;
        private GameUserElements.DBLayoutPanel dbLayoutPanel2;
        private System.Windows.Forms.Button btnExit;
        private GameUserElements.DBLayoutPanel dbLayoutPanel3;
        private System.Windows.Forms.Label lblDifficulty;
        private System.Windows.Forms.Label lblPrevDifficulty;
    }
}
