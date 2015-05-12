using System;
using System.Windows.Forms;
using GameUserElements;
using SaveYourTower.DesktopUI.GamePages;

namespace SaveYourTower.DesktopUI
{
    partial class PlaingPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaingPage));
            this.tblMainGameView = new GameUserElements.DBLayoutPanel();
            this.tblButtons = new GameUserElements.DBLayoutPanel();
            this.btnTurret = new System.Windows.Forms.Button();
            this.btnHitAll = new System.Windows.Forms.Button();
            this.btnSlowAll = new System.Windows.Forms.Button();
            this.bntNextLevel = new System.Windows.Forms.Button();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.bntExit = new System.Windows.Forms.Button();
            this.bntPause = new System.Windows.Forms.Button();
            this.tblMainGameView.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMainGameView
            // 
            this.tblMainGameView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblMainGameView.BackColor = System.Drawing.Color.Transparent;
            this.tblMainGameView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tblMainGameView.CausesValidation = false;
            this.tblMainGameView.ColumnCount = 3;
            this.tblMainGameView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.28894F));
            this.tblMainGameView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.9534F));
            this.tblMainGameView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.75766F));
            this.tblMainGameView.Controls.Add(this.tblButtons, 1, 4);
            this.tblMainGameView.Controls.Add(this.bntNextLevel, 1, 2);
            this.tblMainGameView.Controls.Add(this.btnStartGame, 1, 1);
            this.tblMainGameView.Controls.Add(this.bntExit, 1, 3);
            this.tblMainGameView.Controls.Add(this.bntPause, 2, 0);
            this.tblMainGameView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainGameView.ForeColor = System.Drawing.Color.White;
            this.tblMainGameView.Location = new System.Drawing.Point(0, 0);
            this.tblMainGameView.Name = "tblMainGameView";
            this.tblMainGameView.RowCount = 5;
            this.tblMainGameView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.75761F));
            this.tblMainGameView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tblMainGameView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tblMainGameView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.24239F));
            this.tblMainGameView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblMainGameView.Size = new System.Drawing.Size(500, 500);
            this.tblMainGameView.TabIndex = 13;
            this.tblMainGameView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tblMainGameView_MouseClick);
            // 
            // tblButtons
            // 
            this.tblButtons.BackColor = System.Drawing.Color.Transparent;
            this.tblButtons.ColumnCount = 3;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblButtons.Controls.Add(this.btnTurret, 0, 0);
            this.tblButtons.Controls.Add(this.btnHitAll, 1, 0);
            this.tblButtons.Controls.Add(this.btnSlowAll, 2, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblButtons.Location = new System.Drawing.Point(169, 442);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(168, 55);
            this.tblButtons.TabIndex = 0;
            // 
            // btnTurret
            // 
            this.btnTurret.BackColor = System.Drawing.Color.Transparent;
            this.btnTurret.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTurret.CausesValidation = false;
            this.btnTurret.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTurret.FlatAppearance.BorderSize = 0;
            this.btnTurret.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTurret.Font = new System.Drawing.Font("Agency FB", 35F, System.Drawing.FontStyle.Bold);
            this.btnTurret.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.btnTurret.Image = ((System.Drawing.Image)(resources.GetObject("btnTurret.Image")));
            this.btnTurret.Location = new System.Drawing.Point(3, 3);
            this.btnTurret.Name = "btnTurret";
            this.btnTurret.Size = new System.Drawing.Size(50, 49);
            this.btnTurret.TabIndex = 8;
            this.btnTurret.UseVisualStyleBackColor = false;
            this.btnTurret.Click += new System.EventHandler(this.btnTurret_Click);
            // 
            // btnHitAll
            // 
            this.btnHitAll.BackColor = System.Drawing.Color.Transparent;
            this.btnHitAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHitAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHitAll.FlatAppearance.BorderSize = 0;
            this.btnHitAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHitAll.Font = new System.Drawing.Font("Agency FB", 8F, System.Drawing.FontStyle.Bold);
            this.btnHitAll.ForeColor = System.Drawing.Color.Blue;
            this.btnHitAll.Image = global::SaveYourTower.DesktopUI.Properties.Resources.HitAll;
            this.btnHitAll.Location = new System.Drawing.Point(59, 3);
            this.btnHitAll.Name = "btnHitAll";
            this.btnHitAll.Size = new System.Drawing.Size(50, 49);
            this.btnHitAll.TabIndex = 10;
            this.btnHitAll.UseVisualStyleBackColor = false;
            this.btnHitAll.Click += new System.EventHandler(this.btnHitAll_Click);
            // 
            // btnSlowAll
            // 
            this.btnSlowAll.BackColor = System.Drawing.Color.Transparent;
            this.btnSlowAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSlowAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSlowAll.FlatAppearance.BorderSize = 0;
            this.btnSlowAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSlowAll.Font = new System.Drawing.Font("Agency FB", 8F, System.Drawing.FontStyle.Bold);
            this.btnSlowAll.ForeColor = System.Drawing.Color.Blue;
            this.btnSlowAll.Image = global::SaveYourTower.DesktopUI.Properties.Resources.SlowAll;
            this.btnSlowAll.Location = new System.Drawing.Point(115, 3);
            this.btnSlowAll.Name = "btnSlowAll";
            this.btnSlowAll.Size = new System.Drawing.Size(50, 49);
            this.btnSlowAll.TabIndex = 11;
            this.btnSlowAll.UseVisualStyleBackColor = false;
            this.btnSlowAll.Click += new System.EventHandler(this.btnSlowAll_Click);
            // 
            // bntNextLevel
            // 
            this.bntNextLevel.BackColor = System.Drawing.Color.Transparent;
            this.bntNextLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bntNextLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntNextLevel.Font = new System.Drawing.Font("Agency FB", 25F, System.Drawing.FontStyle.Bold);
            this.bntNextLevel.ForeColor = System.Drawing.Color.Lime;
            this.bntNextLevel.Location = new System.Drawing.Point(169, 300);
            this.bntNextLevel.Name = "bntNextLevel";
            this.bntNextLevel.Size = new System.Drawing.Size(168, 59);
            this.bntNextLevel.TabIndex = 7;
            this.bntNextLevel.Text = "Next Level";
            this.bntNextLevel.UseVisualStyleBackColor = false;
            this.bntNextLevel.Visible = false;
            this.bntNextLevel.Click += new System.EventHandler(this.bntNextLevel_Click);
            // 
            // btnStartGame
            // 
            this.btnStartGame.BackColor = System.Drawing.Color.Transparent;
            this.btnStartGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStartGame.FlatAppearance.BorderSize = 0;
            this.btnStartGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartGame.Font = new System.Drawing.Font("Agency FB", 25F, System.Drawing.FontStyle.Bold);
            this.btnStartGame.ForeColor = System.Drawing.Color.Lime;
            this.btnStartGame.Location = new System.Drawing.Point(169, 140);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(168, 154);
            this.btnStartGame.TabIndex = 12;
            this.btnStartGame.Text = "Start";
            this.btnStartGame.UseVisualStyleBackColor = false;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // bntExit
            // 
            this.bntExit.BackColor = System.Drawing.Color.Transparent;
            this.bntExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bntExit.FlatAppearance.BorderSize = 0;
            this.bntExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntExit.Font = new System.Drawing.Font("Agency FB", 40F, System.Drawing.FontStyle.Bold);
            this.bntExit.ForeColor = System.Drawing.Color.Lime;
            this.bntExit.Location = new System.Drawing.Point(169, 365);
            this.bntExit.Name = "bntExit";
            this.bntExit.Size = new System.Drawing.Size(168, 71);
            this.bntExit.TabIndex = 14;
            this.bntExit.Text = "Exit";
            this.bntExit.UseVisualStyleBackColor = false;
            this.bntExit.Visible = false;
            this.bntExit.Click += new System.EventHandler(this.bntExit_Click);
            // 
            // bntPause
            // 
            this.bntPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bntPause.BackColor = System.Drawing.Color.Transparent;
            this.bntPause.FlatAppearance.BorderSize = 0;
            this.bntPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntPause.Font = new System.Drawing.Font("Agency FB", 18F, System.Drawing.FontStyle.Bold);
            this.bntPause.ForeColor = System.Drawing.Color.Lime;
            this.bntPause.Location = new System.Drawing.Point(426, 3);
            this.bntPause.Name = "bntPause";
            this.bntPause.Size = new System.Drawing.Size(71, 39);
            this.bntPause.TabIndex = 13;
            this.bntPause.Text = "Pause";
            this.bntPause.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bntPause.UseVisualStyleBackColor = false;
            this.bntPause.Visible = false;
            this.bntPause.Click += new System.EventHandler(this.bntPause_Click);
            // 
            // PlaingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.tblMainGameView);
            this.Name = "PlaingPage";
            this.Size = new System.Drawing.Size(500, 500);
            this.tblMainGameView.ResumeLayout(false);
            this.tblButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Button bntNextLevel;
        private Button btnTurret;
        private Button btnHitAll;
        private Button btnSlowAll;
        private Button btnStartGame;
        private DBLayoutPanel tblMainGameView;
        private DBLayoutPanel tblButtons;
        private Button bntPause;
        private Button bntExit;
    }
}
