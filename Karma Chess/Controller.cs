using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.Control;

namespace Karma_Chess
{
    public class Controller
    {
        public Board board;
        public ControlCollection controls;
        public int[] positionsFile = { 6, 75, 144, 213, 282, 352, 420, 489 };
        public int[] positionsRank = { 489, 420, 351, 282, 213, 144, 75, 6 };
        public Form mask;
        public List<((int file, int rank) from, (int file, int rank) to, int Special)> LegalCertainMoves;

        public Controller(Board board, ControlCollection controlCollection, Form mask)
        {
            this.board = board;
            this.mask = mask;
            controls = controlCollection;
            AddClickHandler();
        }

        public void AddClickHandler()
        {
            foreach (Control control in controls)
            {
                control.MouseClick += ControlsMouseClick;
            }
        }
        public void ControlsMouseClick(object sender, MouseEventArgs args)
        {
            DeleteButtons();

            if (sender is PictureBox piece)
            {
                var pieceFile = positionsFile.ToList().IndexOf(piece.Location.X);
                var pieceRank = positionsRank.ToList().IndexOf(piece.Location.Y);

                GetMovesFrom(pieceFile, pieceRank);
            }
            else if (sender is Button button)
            {

            }
        }

        public void GetMovesFrom(int file, int rank)
        {
            var LegalCertainMoves = board.LegalMoves.Where(x => x.from.file == file && x.from.rank == rank).ToList();

            foreach (var move in LegalCertainMoves)
            {
                var button = new Button();
                button.Location = new Point(positionsFile[move.to.file] + 27, positionsRank[move.to.rank] + 27);
                button.Size = new Size(15, 15);
                button.BackColor = Color.Lime;
                button.Visible = true;
                mask.Controls.Add(button);
                button.BringToFront();
            }
        }

        public void DeleteButtons()
        {
            foreach (var control in controls)
            {
                if (control is Button)
                {
                    mask.Controls.Remove((Control)control);
                }
            }
        }
    }
}
