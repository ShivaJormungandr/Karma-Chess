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
            this.tbTestBoard = new System.Windows.Forms.RichTextBox();
            this.btPrintBoard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbTestBoard
            // 
            this.tbTestBoard.Location = new System.Drawing.Point(11, 12);
            this.tbTestBoard.Name = "tbTestBoard";
            this.tbTestBoard.Size = new System.Drawing.Size(586, 410);
            this.tbTestBoard.TabIndex = 0;
            this.tbTestBoard.Text = "";
            // 
            // btPrintBoard
            // 
            this.btPrintBoard.Location = new System.Drawing.Point(603, 12);
            this.btPrintBoard.Name = "btPrintBoard";
            this.btPrintBoard.Size = new System.Drawing.Size(185, 23);
            this.btPrintBoard.TabIndex = 1;
            this.btPrintBoard.Text = "PrintBoard";
            this.btPrintBoard.UseVisualStyleBackColor = true;
            this.btPrintBoard.Click += new System.EventHandler(this.btPrintBoard_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btPrintBoard);
            this.Controls.Add(this.tbTestBoard);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBox tbTestBoard;
        private Button btPrintBoard;
    }
}