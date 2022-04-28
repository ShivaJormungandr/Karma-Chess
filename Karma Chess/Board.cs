using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma_Chess
{
    public class Board
    {
        public byte[] Squares;

        public Board()
        {
            Squares = new byte[64];

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

            Squares[56] = (byte)Color.WHITE | (byte)Piece.ROOK;
            Squares[57] = (byte)Color.WHITE | (byte)Piece.KNIGHT;
            Squares[58] = (byte)Color.WHITE | (byte)Piece.BISHOP;
            Squares[59] = (byte)Color.WHITE | (byte)Piece.KING;
            Squares[60] = (byte)Color.WHITE | (byte)Piece.QUEEN;
            Squares[61] = (byte)Color.WHITE | (byte)Piece.BISHOP;
            Squares[62] = (byte)Color.WHITE | (byte)Piece.KNIGHT;
            Squares[63] = (byte)Color.WHITE | (byte)Piece.ROOK;

            for (int i = 48; i < 56; i++)
            {
                Squares[i] = (byte)Color.WHITE | (byte)Piece.PAWN;
            }
        }

        private void InitBlackPieces()
        {
            Squares[0] = (byte)Color.BLACK | (byte)Piece.ROOK;
            Squares[1] = (byte)Color.BLACK | (byte)Piece.KNIGHT;
            Squares[2] = (byte)Color.BLACK | (byte)Piece.BISHOP;
            Squares[3] = (byte)Color.BLACK | (byte)Piece.KING;
            Squares[4] = (byte)Color.BLACK | (byte)Piece.QUEEN;
            Squares[5] = (byte)Color.BLACK | (byte)Piece.BISHOP;
            Squares[6] = (byte)Color.BLACK | (byte)Piece.KNIGHT;
            Squares[7] = (byte)Color.BLACK | (byte)Piece.ROOK;

            for (int i = 8; i < 16; i++)
            {
                Squares[i] = (byte)Color.BLACK | (byte)Piece.PAWN;
            }
        }

        //astea sigur ne vor folosi aici sau in alta parte

        public bool IsPieceBlack(byte piece)
        {
            if ((piece & 128) == 128)
            {
                return true;
            }

            return false;
        }

        public bool IsPieceWhite(byte piece)
        {
            if ((piece & 64) == 64)
            {
                return true;
            }

            return false;
        }

        public bool IsPieceKing(byte piece)
        {
            if ((piece & 32) == 32)
            {
                return true;
            }

            return false;
        }

        public bool IsPieceQueen(byte piece)
        {
            if ((piece & 16) == 16)
            {
                return true;
            }

            return false;
        }

        public bool IsPieceBishop(byte piece)
        {
            if ((piece & 8) == 8)
            {
                return true;
            }

            return false;
        }

        public bool IsPieceKnight(byte piece)
        {
            if ((piece & 4) == 4)
            {
                return true;
            }

            return false;
        }

        public bool IsPieceRook(byte piece)
        {
            if ((piece & 2) == 2)
            {
                return true;
            }

            return false;
        }

        public bool IsPiecePawn(byte piece)
        {
            if ((piece & 1) == 1)
            {
                return true;
            }

            return false;
        }

    }

    public enum Color
    {
        BLACK = 128,
        WHITE = 64,
    }

    public enum Piece
    {
        KING = 32,
        QUEEN = 16,
        BISHOP = 8,
        KNIGHT = 4,
        ROOK = 2,
        PAWN = 1,
    }
}
