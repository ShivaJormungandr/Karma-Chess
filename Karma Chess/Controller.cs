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
            LegalCertainMoves = new List<((int file, int rank) from, (int file, int rank) to, int Special)>();
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
            if (sender is PictureBox piece)
            {
                DeleteButtons();
                board.CalculateLegalMoves();

                var pieceFile = positionsFile.ToList().IndexOf(piece.Location.X);
                var pieceRank = positionsRank.ToList().IndexOf(piece.Location.Y);

                GetMovesFrom(pieceFile, pieceRank);
            }
            else if (sender is Button button)
            {
                var buttonFile = positionsFile.ToList().IndexOf(button.Location.X - 27);
                var buttonRank = positionsRank.ToList().IndexOf(button.Location.Y - 27);
                var selectedMove = LegalCertainMoves.Where(x => x.to.file == buttonFile && x.to.rank == buttonRank).ToList().First();
                DeleteButtons();

                if (board.Move(selectedMove.from, selectedMove.to))
                {
                    var pieceToMove = GetPieceToMove(selectedMove);

                    if (pieceToMove != null)
                    {
                        pieceToMove.Location = new Point(positionsFile[selectedMove.to.file], positionsRank[selectedMove.to.rank]);
                        mask.Controls.Add(pieceToMove);
                        pieceToMove.MouseClick += ControlsMouseClick;
                    }
                }
            }
        }

        public void GetMovesFrom(int file, int rank)
        {
            LegalCertainMoves = board.LegalMoves.Where(x => x.from.file == file && x.from.rank == rank).ToList();

            foreach (var move in LegalCertainMoves)
            {
                var button = new Button();
                button.Location = new Point(positionsFile[move.to.file] + 27, positionsRank[move.to.rank] + 27);
                button.Size = new Size(15, 15);
                button.BackColor = Color.Lime;
                button.Visible = true;
                mask.Controls.Add(button);
                button.BringToFront();
                button.MouseClick += ControlsMouseClick;
            }
        }

        public void DeleteButtons()
        {
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i] is Button)
                {
                    mask.Controls.RemoveAt(i);
                    i--;
                }
            }
        }

        public PictureBox GetPieceToMove(((int file, int rank) from, (int file, int rank) to, int Special) selectedMove)
        {
            foreach (Control control in controls)
            {
                if (control is PictureBox pictureBox)
                {
                    if (pictureBox.Location == new Point(positionsFile[selectedMove.from.file], positionsRank[selectedMove.from.rank]))
                    {
                        return pictureBox;
                    }
                }
            }
            return null;
        }
    }
}
