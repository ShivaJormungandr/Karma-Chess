using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma_Chess
{
    public class DrawPiece
    {
        public int[] positions = { 489, 420, 351, 282, 213, 144, 75, 6 };

        public DrawPiece(int file, int rank, Form mask, Pieces pieceSqare)
        {
            PictureBox piece = new PictureBox();

            if (pieceSqare.IsBishop())
            {
                if (pieceSqare.IsBlack())
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\bishopB.png");
                }
                else if (pieceSqare.IsWhite())
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\bishop.png");
                }
            }
            else if (pieceSqare.IsRook())
            {
                if (pieceSqare.IsBlack())
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\rookB.png");
                }
                else if (pieceSqare.IsWhite())
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\rook.png");
                }
            }
            else if (pieceSqare.IsKnight())
            {
                if (pieceSqare.IsBlack())
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\knightB.png");
                }
                else if (pieceSqare.IsWhite())
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\knight.png");
                }
            }
            else if (pieceSqare.IsPawn())
            {
                if (pieceSqare.IsBlack())
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\pawnB.png");
                }
                else if (pieceSqare.IsWhite())
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\pawn.png");
                }
            }
            else if (pieceSqare.IsQueen())
            {
                if (pieceSqare.IsBlack())
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\queenB.png");
                }
                else if (pieceSqare.IsWhite())
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\queen.png");
                }
            }
            else if (pieceSqare.IsKing())
            {
                if (pieceSqare.IsBlack())
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\kingB.png");
                }
                else if (pieceSqare.IsWhite())
                {
                    piece.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\king.png");
                }
            }
            
            piece.Size = new Size(64, 64);
            piece.Location = new Point(positions[file], positions[rank]);
            piece.BackColor=Color.Transparent;    
            //piece.Layout = ImageLayout.Center;
            piece.Visible = true;
            mask.Controls.Add(piece);
            piece.BringToFront();
        }
    }
}
