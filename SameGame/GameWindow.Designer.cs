namespace SameGame
{
    partial class GameWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.GameBoard = new System.Windows.Forms.TableLayoutPanel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x10ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.x5ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.trackBar = new System.Windows.Forms.TrackBar();
			this.EasyVersion = new System.Windows.Forms.RadioButton();
			this.HardVersion = new System.Windows.Forms.RadioButton();
			this.HighScoreList = new System.Windows.Forms.ListView();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
			this.SuspendLayout();
			// 
			// GameBoard
			// 
			this.GameBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GameBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.GameBoard.ColumnCount = 2;
			this.GameBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.GameBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.GameBoard.Location = new System.Drawing.Point(0, 73);
			this.GameBoard.Margin = new System.Windows.Forms.Padding(0);
			this.GameBoard.Name = "GameBoard";
			this.GameBoard.RowCount = 2;
			this.GameBoard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.GameBoard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.GameBoard.Size = new System.Drawing.Size(418, 192);
			this.GameBoard.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(418, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// newGameToolStripMenuItem
			// 
			this.newGameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x5ToolStripMenuItem,
            this.x10ToolStripMenuItem,
            this.x10ToolStripMenuItem1,
            this.x5ToolStripMenuItem1});
			this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
			this.newGameToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
			this.newGameToolStripMenuItem.Text = "New Game";
			// 
			// x5ToolStripMenuItem
			// 
			this.x5ToolStripMenuItem.Name = "x5ToolStripMenuItem";
			this.x5ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.x5ToolStripMenuItem.Text = "5 x 5 ";
			this.x5ToolStripMenuItem.Click += new System.EventHandler(this.Start5X5Game);
			// 
			// x10ToolStripMenuItem
			// 
			this.x10ToolStripMenuItem.Name = "x10ToolStripMenuItem";
			this.x10ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.x10ToolStripMenuItem.Text = "10  x 10";
			this.x10ToolStripMenuItem.Click += new System.EventHandler(this.Start10X10Game);
			// 
			// x10ToolStripMenuItem1
			// 
			this.x10ToolStripMenuItem1.Name = "x10ToolStripMenuItem1";
			this.x10ToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
			this.x10ToolStripMenuItem1.Text = "5  x 10 ";
			this.x10ToolStripMenuItem1.Click += new System.EventHandler(this.Start5X10Game);
			// 
			// x5ToolStripMenuItem1
			// 
			this.x5ToolStripMenuItem1.Name = "x5ToolStripMenuItem1";
			this.x5ToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
			this.x5ToolStripMenuItem1.Text = "10 x 5 ";
			this.x5ToolStripMenuItem1.Click += new System.EventHandler(this.Start10X5Game);
			// 
			// trackBar
			// 
			this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar.Location = new System.Drawing.Point(12, 27);
			this.trackBar.Minimum = 1;
			this.trackBar.Name = "trackBar";
			this.trackBar.Size = new System.Drawing.Size(394, 45);
			this.trackBar.TabIndex = 2;
			this.trackBar.Value = 5;
			this.trackBar.ValueChanged += new System.EventHandler(this.ChangeColorNumber);
			// 
			// EasyVersion
			// 
			this.EasyVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.EasyVersion.AutoSize = true;
			this.EasyVersion.Checked = true;
			this.EasyVersion.Location = new System.Drawing.Point(12, 284);
			this.EasyVersion.Name = "EasyVersion";
			this.EasyVersion.Size = new System.Drawing.Size(48, 17);
			this.EasyVersion.TabIndex = 3;
			this.EasyVersion.TabStop = true;
			this.EasyVersion.Text = "Easy";
			this.EasyVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.EasyVersion.UseVisualStyleBackColor = true;
			this.EasyVersion.CheckedChanged += new System.EventHandler(this.ChooseEasyVersion);
			// 
			// HardVersion
			// 
			this.HardVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.HardVersion.AutoSize = true;
			this.HardVersion.Location = new System.Drawing.Point(143, 284);
			this.HardVersion.Name = "HardVersion";
			this.HardVersion.Size = new System.Drawing.Size(48, 17);
			this.HardVersion.TabIndex = 4;
			this.HardVersion.Text = "Hard";
			this.HardVersion.UseVisualStyleBackColor = true;
			this.HardVersion.CheckedChanged += new System.EventHandler(this.ChooseHardVersion);
			// 
			// HighScoreList
			// 
			this.HighScoreList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.HighScoreList.Location = new System.Drawing.Point(297, 73);
			this.HighScoreList.Name = "HighScoreList";
			this.HighScoreList.Size = new System.Drawing.Size(121, 192);
			this.HighScoreList.TabIndex = 5;
			this.HighScoreList.UseCompatibleStateImageBehavior = false;
			this.HighScoreList.Visible = false;
			// 
			// GameWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(418, 313);
			this.Controls.Add(this.HighScoreList);
			this.Controls.Add(this.HardVersion);
			this.Controls.Add(this.EasyVersion);
			this.Controls.Add(this.trackBar);
			this.Controls.Add(this.GameBoard);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "GameWindow";
			this.Text = "Clickmania";
			this.Load += new System.EventHandler(this.InitializeGame);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel GameBoard;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x10ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem x5ToolStripMenuItem1;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.RadioButton EasyVersion;
        private System.Windows.Forms.RadioButton HardVersion;
        private System.Windows.Forms.ListView HighScoreList;
    }
}

