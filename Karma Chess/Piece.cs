using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma_Chess
{
    public class Piece
    {
        public int[] positions = { 18, 87, 156, 225, 294, 363, 432, 501 };

        public Piece(int x, int y, Form mask, Pieces type)
        {
            PictureBox piece = new PictureBox();

            if (IsPieceBishop(type))
            {
                if (IsPieceBlack(type))
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\bishopB.png");
                }
                else if (IsPieceWhite(type))
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\bishop.png");
                }
            }
            else if (IsPieceRook(type))
            {
                if (IsPieceBlack(type))
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\rookB.png");
                }
                else if (IsPieceWhite(type))
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\rook.png");
                }
            }
            else if (IsPieceKnight(type))
            {
                if (IsPieceBlack(type))
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\knightB.png");
                }
                else if (IsPieceWhite(type))
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\knight.png");
                }
            }
            else if (IsPiecePawn(type))
            {
                if (IsPieceBlack(type))
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\pawnB.png");
                }
                else if (IsPieceWhite(type))
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\pawn.png");
                }
            }
            else if (IsPieceQueen(type))
            {
                if (IsPieceBlack(type))
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\queenB.png");
                }
                else if (IsPieceWhite(type))
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\queen.png");
                }
            }
            else if (IsPieceKing(type))
            {
                if (IsPieceBlack(type))
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\kingB.png");
                }
                else if (IsPieceWhite(type))
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\king.png");
                }
            }

            piece.Size = new Size(64, 64);
            piece.Location = new Point(positions[x], positions[y]);
            //piece.Layout = ImageLayout.Center;
            piece.Visible = true;
            mask.Controls.Add(piece);
            piece.BringToFront();
        }


        #region Piece Checks
        public bool IsPieceBlack(Pieces piece)
        {
            return (piece & Pieces.Black) == Pieces.Black ? true : false;
        }

        public bool IsPieceWhite(Pieces piece)
        {
            return (piece & Pieces.White) == Pieces.White ? true : false;
        }

        public bool IsPieceKing(Pieces piece)
        {
            return (piece & Pieces.King) == Pieces.King ? true : false;
        }

        public bool IsPieceQueen(Pieces piece)
        {
            return (piece & Pieces.Queen) == Pieces.Queen ? true : false;
        }

        public bool IsPieceBishop(Pieces piece)
        {
            return (piece & Pieces.Bishop) == Pieces.Bishop ? true : false;

        }

        public bool IsPieceKnight(Pieces piece)
        {
            return (piece & Pieces.Knight) == Pieces.Knight ? true : false;
        }

        public bool IsPieceRook(Pieces piece)
        {
            return (piece & Pieces.Rook) == Pieces.Rook ? true : false;
        }

        public bool IsPiecePawn(Pieces piece)
        {
            return (piece & Pieces.Pawn) == Pieces.Pawn ? true : false;
        }

        public bool IsPieceEmpty(Pieces piece)
        {
            return piece == Pieces.None;
        }
        #endregion
    }

    [Flags]
    public enum Pieces
    {
        None =      0b00000000,
        Pawn =      0b00000001,
        Knight =    0b00000010,
        Bishop =    0b00000100,
        Rook =      0b00001000,
        Queen =     0b00010000,
        King =      0b00100000,

        White =     0b01000000,
        Black =     0b10000000,
    }

    [Flags]
    public enum Turn
    {
        White = 0,
        Black = 1,
    }

    [Flags]
    public enum Castling
    {
        None =           0b0000,
        BlackKingSide =  0b0001,
        BlackQueenSide = 0b0010,
        WhiteKingSide =  0b0100,
        WhiteQueenSide = 0b1000,
    }
}
