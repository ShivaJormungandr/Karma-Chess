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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Karma_Chess.Properties.Resources.Chess_Board;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(913, 559);
            this.Controls.Add(this.btEmptyBoard);
            this.Controls.Add(this.btStartGame);
            this.Controls.Add(this.tbFen);
            this.Controls.Add(this.lbFen);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lbFen;
        private TextBox tbFen;
        private Button btStartGame;
        private Button btEmptyBoard;
    }
}