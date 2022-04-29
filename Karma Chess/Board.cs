using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma_Chess
{
    public class Board
    {
        public Piece[] Squares;

        public Board()
        {
            Squares = new Piece[64];

            InitBlackPieces();
            InitWhitePieces();
        }

        private void InitWhitePieces()
        {
            //  0  1  2  3  4  5  6  7
            //  8  9 10 11 12 13 14 15
            // 16 17 18 19 20 21 22 23
            // 24 25 26 27 28 29 30 31
            // 32 33 34 35 36 37 38 39
            // 40 41 42 43 44 45 46 47
            // 48 49 50 51 52 53 54 55
            // 56 57 58 59 60 61 62 63

            Squares[56] = Piece.White | Piece.Rook;
            Squares[57] = Piece.White | Piece.Knight;
            Squares[58] = Piece.White | Piece.Bishop;
            Squares[59] = Piece.White | Piece.King;
            Squares[60] = Piece.White | Piece.Queen;
            Squares[61] = Piece.White | Piece.Bishop;
            Squares[62] = Piece.White | Piece.Knight;
            Squares[63] = Piece.White | Piece.Rook;

            for (int i = 48; i < 56; i++)
            {
                Squares[i] = Piece.White | Piece.Pawn;
            }
        }

        private void InitBlackPieces()
        {
            Squares[0] = Piece.Black | Piece.Rook;
            Squares[1] = Piece.Black | Piece.Knight;
            Squares[2] = Piece.Black | Piece.Bishop;
            Squares[3] = Piece.Black | Piece.King;
            Squares[4] = Piece.Black | Piece.Queen;
            Squares[5] = Piece.Black | Piece.Bishop;
            Squares[6] = Piece.Black | Piece.Knight;
            Squares[7] = Piece.Black | Piece.Rook;

            for (int i = 8; i < 16; i++)
            {
                Squares[i] = Piece.Black | Piece.Pawn;
            }
        }

        //astea sigur ne vor folosi aici sau in alta parte

        public bool IsPieceBlack(Piece piece)
        {
            return (piece & Piece.Black) == Piece.Black ? true : false;
        }

        public bool IsPieceWhite(Piece piece)
        {
            return (piece & Piece.White) == Piece.White ? true : false;
        }

        public bool IsPieceKing(Piece piece)
        {
            return (piece & Piece.King) == Piece.King ? true : false;
        }

        public bool IsPieceQueen(Piece piece)
        {
            return (piece & Piece.Queen) == Piece.Queen ? true : false;
        }

        public bool IsPieceBishop(Piece piece)
        {
            return (piece & Piece.Bishop) == Piece.Bishop ? true : false;

        }

        public bool IsPieceKnight(Piece piece)
        {
            return (piece & Piece.Knight) == Piece.Knight ? true : false;
        }

        public bool IsPieceRook(Piece piece)
        {
            return (piece & Piece.Rook) == Piece.Rook ? true : false;
        }

        public bool IsPiecePawn(Piece piece)
        {
            return (piece & Piece.Pawn) == Piece.Pawn ? true : false;
        }
        public bool IsPieceEmpty(Piece piece)
        {
            return piece == Piece.None;
        }
    }
}
