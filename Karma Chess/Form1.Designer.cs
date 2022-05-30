namespace Karma_Chess
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbFen = new System.Windows.Forms.Label();
            this.tbFen = new System.Windows.Forms.TextBox();
            this.btStartGame = new System.Windows.Forms.Button();
            this.btEmptyBoard = new System.Windows.Forms.Button();
            this.cbai = new System.Windows.Forms.CheckBox();
            this.tbMove = new System.Windows.Forms.TextBox();
            this.btMove = new System.Windows.Forms.Button();
            this.btBestMove = new System.Windows.Forms.Button();
            this.lbDifficulty = new System.Windows.Forms.Label();
            this.tbDifficulty = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.tbDifficulty)).BeginInit();
            this.SuspendLayout();
            // 
            // lbFen
            // 
            this.lbFen.AutoSize = true;
            this.lbFen.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbFen.Location = new System.Drawing.Point(575, 9);
            this.lbFen.Name = "lbFen";
            this.lbFen.Size = new System.Drawing.Size(45, 25);
            this.lbFen.TabIndex = 0;
            this.lbFen.Text = "FEN";
            // 
            // tbFen
            // 
            this.tbFen.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbFen.Location = new System.Drawing.Point(626, 6);
            this.tbFen.Name = "tbFen";
            this.tbFen.Size = new System.Drawing.Size(275, 32);
            this.tbFen.TabIndex = 1;
            // 
            // btStartGame
            // 
            this.btStartGame.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btStartGame.Location = new System.Drawing.Point(575, 44);
            this.btStartGame.Name = "btStartGame";
            this.btStartGame.Size = new System.Drawing.Size(326, 32);
            this.btStartGame.TabIndex = 2;
            this.btStartGame.Text = "Start Game";
            this.btStartGame.UseVisualStyleBackColor = true;
            this.btStartGame.UseWaitCursor = true;
            this.btStartGame.Click += new System.EventHandler(this.btStartGame_Click);
            // 
            // btEmptyBoard
            // 
            this.btEmptyBoard.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btEmptyBoard.Location = new System.Drawing.Point(575, 82);
            this.btEmptyBoard.Name = "btEmptyBoard";
            this.btEmptyBoard.Size = new System.Drawing.Size(326, 32);
            this.btEmptyBoard.TabIndex = 2;
            this.btEmptyBoard.Text = "Empty Board";
            this.btEmptyBoard.UseVisualStyleBackColor = true;
            this.btEmptyBoard.UseWaitCursor = true;
            this.btEmptyBoard.Click += new System.EventHandler(this.btEmptyBoard_Click);
            // 
            // cbai
            // 
            this.cbai.AutoSize = true;
            this.cbai.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbai.Location = new System.Drawing.Point(575, 120);
            this.cbai.Name = "cbai";
            this.cbai.Size = new System.Drawing.Size(48, 29);
            this.cbai.TabIndex = 4;
            this.cbai.Text = "AI";
            this.cbai.UseVisualStyleBackColor = true;
            // 
            // tbMove
            // 
            this.tbMove.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbMove.Location = new System.Drawing.Point(575, 171);
            this.tbMove.Name = "tbMove";
            this.tbMove.Size = new System.Drawing.Size(161, 32);
            this.tbMove.TabIndex = 1;
            this.tbMove.Text = "x9x9-p";
            // 
            // btMove
            // 
            this.btMove.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btMove.Location = new System.Drawing.Point(742, 170);
            this.btMove.Name = "btMove";
            this.btMove.Size = new System.Drawing.Size(159, 32);
            this.btMove.TabIndex = 2;
            this.btMove.Text = "Move";
            this.btMove.UseVisualStyleBackColor = true;
            this.btMove.UseWaitCursor = true;
            this.btMove.Click += new System.EventHandler(this.btMove_Click);
            // 
            // btBestMove
            // 
            this.btBestMove.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btBestMove.Location = new System.Drawing.Point(575, 224);
            this.btBestMove.Name = "btBestMove";
            this.btBestMove.Size = new System.Drawing.Size(326, 32);
            this.btBestMove.TabIndex = 5;
            this.btBestMove.Text = "Make Best Move";
            this.btBestMove.UseVisualStyleBackColor = true;
            this.btBestMove.Click += new System.EventHandler(this.btBestMove_Click);
            // 
            // lbDifficulty
            // 
            this.lbDifficulty.AutoSize = true;
            this.lbDifficulty.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbDifficulty.Location = new System.Drawing.Point(626, 120);
            this.lbDifficulty.Name = "lbDifficulty";
            this.lbDifficulty.Size = new System.Drawing.Size(42, 25);
            this.lbDifficulty.TabIndex = 0;
            this.lbDifficulty.Text = "Diff";
            // 
            // tbDifficulty
            // 
            this.tbDifficulty.LargeChange = 1;
            this.tbDifficulty.Location = new System.Drawing.Point(674, 120);
            this.tbDifficulty.Maximum = 7;
            this.tbDifficulty.Minimum = 2;
            this.tbDifficulty.Name = "tbDifficulty";
            this.tbDifficulty.Size = new System.Drawing.Size(227, 45);
            this.tbDifficulty.TabIndex = 6;
            this.tbDifficulty.Value = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Karma_Chess.Properties.Resources.Chess_Board;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(913, 559);
            this.Controls.Add(this.tbDifficulty);
            this.Controls.Add(this.btBestMove);
            this.Controls.Add(this.cbai);
            this.Controls.Add(this.btEmptyBoard);
            this.Controls.Add(this.btMove);
            this.Controls.Add(this.btStartGame);
            this.Controls.Add(this.tbMove);
            this.Controls.Add(this.tbFen);
            this.Controls.Add(this.lbDifficulty);
            this.Controls.Add(this.lbFen);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbDifficulty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lbFen;
        private TextBox tbFen;
        private Button btStartGame;
        private Button btEmptyBoard;
        private CheckBox cbai;
        private TextBox tbMove;
        private Button btMove;
        private Button btBestMove;
        private Label lbDifficulty;
        private TrackBar tbDifficulty;
    }
}