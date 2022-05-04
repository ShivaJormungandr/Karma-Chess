using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma_Chess
{
    public static class ExtensionMethods
    {
        public static void drawBoard(this Form form, Board board)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board.Squares[x, y] == Pieces.None)
                    {
                        continue;
                    }
                    var piece = new DrawPiece(x, y, form, board.Squares[x, y]);
                }
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
