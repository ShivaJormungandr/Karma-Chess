using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Karma_Chess
{
    public static class ExtensionMethods
    {
        public static void DrawBoard(this Form form, Board board)
        {
            for (int file = 0; file < 8; file++)
            {
                for (int rank = 0; rank < 8; rank++)
                {
                    if (board.Squares[file, rank] == Pieces.None)
                    {
                        continue;
                    }

                    _ = new DrawPiece(file, rank, form, board.Squares[file, rank]);
                }
            }
        }

        public static void EmptyBoard(this Form form)
        {
            //TODO: Implement this
            RemoveControls(form, typeof(PictureBox));
        }

        private static void RemoveControls(Control control, Type type)
        {
            List<Control> controls = new List<Control>();

            Stack<Control> stack = new Stack<Control>();
            stack.Push(control);

            while (stack.Count > 0)
            {
                Control ctrl = stack.Pop();
                foreach (Control child in ctrl.Controls)
                {
                    if (child.GetType() == type)
                    {
                        controls.Add(child);
                    }
                    else if (true == child.HasChildren)
                    {
                        stack.Push(child);
                    }
                }
            }

            foreach (Control ctrl in controls)
            {
                control.Controls.Remove(ctrl);

                ctrl.Dispose();
            }
        }

        public static void UpdateBoard(this Form form, Board board,
            PictureBox PictureToUpdate, PictureBox? PieceToDelete = null)
        {
            //TODO: Implement this
            if (PieceToDelete != null)
            {
                //Delete PictureBox;
            }

            var Sqares = board.Squares;

        }

        public static T CopyObject<T>(this object objSource)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, objSource);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }

        #region Piece Checks
        public static bool IsBlack(this Pieces piece)
        {
            return (piece & Pieces.Black) == Pieces.Black ? true : false;
        }

        public static bool IsWhite(this Pieces piece)
        {
            return (piece & Pieces.White) == Pieces.White ? true : false;
        }

        public static bool IsKing(this Pieces piece)
        {
            return (piece & Pieces.King) == Pieces.King ? true : false;
        }

        public static bool IsQueen(this Pieces piece)
        {
            return (piece & Pieces.Queen) == Pieces.Queen ? true : false;
        }

        public static bool IsBishop(this Pieces piece)
        {
            return (piece & Pieces.Bishop) == Pieces.Bishop ? true : false;

        }

        public static bool IsKnight(this Pieces piece)
        {
            return (piece & Pieces.Knight) == Pieces.Knight ? true : false;
        }

        public static bool IsRook(this Pieces piece)
        {
            return (piece & Pieces.Rook) == Pieces.Rook ? true : false;
        }

        public static bool IsPawn(this Pieces piece)
        {
            return (piece & Pieces.Pawn) == Pieces.Pawn ? true : false;
        }

        public static bool IsEmpty(this Pieces piece)
        {
            return piece == Pieces.None;
        }
        #endregion
    }
}
