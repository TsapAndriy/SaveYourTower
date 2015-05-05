using System.Windows.Forms;

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
            this.btnHitAll = new System.Windows.Forms.Button();
            this.btnSlowAll = new System.Windows.Forms.Button();
            this.bntNextLevel = new System.Windows.Forms.Button();
            this.btnTurret = new System.Windows.Forms.Button();
            this.pFieldView = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLose = new System.Windows.Forms.Button();
            this.btnWin = new System.Windows.Forms.Button();
            this.btnWinLevel = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pFieldView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHitAll
            // 
            this.btnHitAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHitAll.BackColor = System.Drawing.Color.Black;
            this.btnHitAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHitAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHitAll.Font = new System.Drawing.Font("Papyrus", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHitAll.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnHitAll.Location = new System.Drawing.Point(220, 449);
            this.btnHitAll.Name = "btnHitAll";
            this.btnHitAll.Size = new System.Drawing.Size(54, 51);
            this.btnHitAll.TabIndex = 10;
            this.btnHitAll.Text = "Hit All";
            this.btnHitAll.UseVisualStyleBackColor = false;
            this.btnHitAll.Click += new System.EventHandler(this.btnHitAll_Click);
            // 
            // btnSlowAll
            // 
            this.btnSlowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSlowAll.BackColor = System.Drawing.Color.Black;
            this.btnSlowAll.BackgroundImage = global::SaveYourTower.DesktopUI.Properties.Resources.outer_space_star_field;
            this.btnSlowAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSlowAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSlowAll.Font = new System.Drawing.Font("Papyrus", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnSlowAll.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSlowAll.Location = new System.Drawing.Point(280, 449);
            this.btnSlowAll.Name = "btnSlowAll";
            this.btnSlowAll.Size = new System.Drawing.Size(54, 51);
            this.btnSlowAll.TabIndex = 11;
            this.btnSlowAll.Text = "Slow All";
            this.btnSlowAll.UseVisualStyleBackColor = false;
            this.btnSlowAll.Click += new System.EventHandler(this.btnSlowAll_Click);
            // 
            // bntNextLevel
            // 
            this.bntNextLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bntNextLevel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.bntNextLevel.Font = new System.Drawing.Font("Papyrus", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.bntNextLevel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.bntNextLevel.Image = global::SaveYourTower.DesktopUI.Properties.Resources.Star_field_5;
            this.bntNextLevel.Location = new System.Drawing.Point(96, 369);
            this.bntNextLevel.Name = "bntNextLevel";
            this.bntNextLevel.Size = new System.Drawing.Size(298, 69);
            this.bntNextLevel.TabIndex = 7;
            this.bntNextLevel.Text = "NextLevel";
            this.bntNextLevel.UseVisualStyleBackColor = false;
            this.bntNextLevel.Visible = false;
            this.bntNextLevel.Click += new System.EventHandler(this.bntNextLevel_Click);
            // 
            // btnTurret
            // 
            this.btnTurret.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTurret.BackColor = System.Drawing.Color.Black;
            this.btnTurret.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTurret.CausesValidation = false;
            this.btnTurret.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTurret.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTurret.Image = global::SaveYourTower.DesktopUI.Properties.Resources.Turret;
            this.btnTurret.Location = new System.Drawing.Point(161, 449);
            this.btnTurret.Name = "btnTurret";
            this.btnTurret.Size = new System.Drawing.Size(53, 51);
            this.btnTurret.TabIndex = 8;
            this.btnTurret.UseVisualStyleBackColor = false;
            this.btnTurret.Click += new System.EventHandler(this.btnTurret_Click);
            // 
            // pFieldView
            // 
            this.pFieldView.BackColor = System.Drawing.Color.Transparent;
            this.pFieldView.BackgroundImage = global::SaveYourTower.DesktopUI.Properties.Resources.Space_field_by_blood_mind;
            this.pFieldView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pFieldView.Location = new System.Drawing.Point(0, 0);
            this.pFieldView.Name = "pFieldView";
            this.pFieldView.Size = new System.Drawing.Size(500, 500);
            this.pFieldView.TabIndex = 0;
            this.pFieldView.TabStop = false;
            this.pFieldView.Click += new System.EventHandler(this.pFieldView_Click_1);
            this.pFieldView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pFieldView_PreviewKeyDown);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(580, 663);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(37, 26);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnLose
            // 
            this.btnLose.Location = new System.Drawing.Point(459, 444);
            this.btnLose.Name = "btnLose";
            this.btnLose.Size = new System.Drawing.Size(38, 26);
            this.btnLose.TabIndex = 4;
            this.btnLose.Text = "Lose";
            this.btnLose.UseVisualStyleBackColor = true;
            this.btnLose.Click += new System.EventHandler(this.btnLose_Click);
            // 
            // btnWin
            // 
            this.btnWin.Location = new System.Drawing.Point(459, 415);
            this.btnWin.Name = "btnWin";
            this.btnWin.Size = new System.Drawing.Size(38, 26);
            this.btnWin.TabIndex = 5;
            this.btnWin.Text = "Win";
            this.btnWin.UseVisualStyleBackColor = true;
            this.btnWin.Click += new System.EventHandler(this.btnWin_Click);
            // 
            // btnWinLevel
            // 
            this.btnWinLevel.Location = new System.Drawing.Point(458, 386);
            this.btnWinLevel.Name = "btnWinLevel";
            this.btnWinLevel.Size = new System.Drawing.Size(38, 26);
            this.btnWinLevel.TabIndex = 6;
            this.btnWinLevel.Text = "WinLevel";
            this.btnWinLevel.UseVisualStyleBackColor = true;
            this.btnWinLevel.Click += new System.EventHandler(this.btnWinLevel_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(458, 357);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(38, 26);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // PlaingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSlowAll);
            this.Controls.Add(this.btnHitAll);
            this.Controls.Add(this.bntNextLevel);
            this.Controls.Add(this.btnWinLevel);
            this.Controls.Add(this.btnWin);
            this.Controls.Add(this.btnLose);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnTurret);
            this.Controls.Add(this.pFieldView);
            this.Name = "PlaingPage";
            this.Size = new System.Drawing.Size(500, 500);
            ((System.ComponentModel.ISupportInitialize)(this.pFieldView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pFieldView;
        private Button bntNextLevel;
        private Button btnTurret;
        private Button btnHitAll;
        private Button btnSlowAll;
        private Button btnExit;
        private Button btnLose;
        private Button btnWin;
        private Button btnWinLevel;
        private Button btnStart;
    }
}
