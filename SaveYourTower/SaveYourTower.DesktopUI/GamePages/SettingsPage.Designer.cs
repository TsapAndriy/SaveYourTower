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
            this.label1 = new System.Windows.Forms.Label();
            this.lblFullScreen = new System.Windows.Forms.Label();
            this.dbLayoutPanel2 = new GameUserElements.DBLayoutPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.dbLayoutPanel1.SuspendLayout();
            this.dbLayoutPanel2.SuspendLayout();
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
            this.dbLayoutPanel1.Controls.Add(this.label1, 1, 2);
            this.dbLayoutPanel1.Controls.Add(this.lblFullScreen, 3, 2);
            this.dbLayoutPanel1.Controls.Add(this.dbLayoutPanel2, 1, 8);
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
            this.ResumeLayout(false);

        }

        #endregion

        private GameUserElements.DBLayoutPanel dbLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFullScreen;
        private GameUserElements.DBLayoutPanel dbLayoutPanel2;
        private System.Windows.Forms.Button btnExit;
    }
}
