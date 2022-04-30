﻿namespace Karma_Chess
{
    public class Board
    {
        public Pieces[] Squares;
        public Turn Turn;
        public Castling Castling;
        //-1 For no target, 0-63 for EnPassant spre target index
        public int EnPassantTarget;
        public int HalfmoveClock;
        public int FullmoveNumber;
        public Form Mask;

        public Board(Form mask)
        {
            Squares = new Pieces[64];
            Mask = mask;
        }

        #region Board Inits
        public void InitStartBoard()
        {
            InitBlackPieces();
            InitWhitePieces();
            InitFlags();
        }

        public void drawBoard()
        {
            for (int i = 0; i<64; i++)
            {
                if (Squares[i] == Pieces.None)
                {
                    continue;
                }

                int x = i % 8;
                int y = i / 8;
                var piece = new Piece(x, y, Mask, Squares[i]);
            }
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

            Squares[56] = Pieces.White | Pieces.Rook;
            Squares[57] = Pieces.White | Pieces.Knight;
            Squares[58] = Pieces.White | Pieces.Bishop;
            Squares[59] = Pieces.White | Pieces.King;
            Squares[60] = Pieces.White | Pieces.Queen;
            Squares[61] = Pieces.White | Pieces.Bishop;
            Squares[62] = Pieces.White | Pieces.Knight;
            Squares[63] = Pieces.White | Pieces.Rook;

            for (int i = 48; i < 56; i++)
            {
                Squares[i] = Pieces.White | Pieces.Pawn;
            }
        }

        private void InitBlackPieces()
        {
            Squares[0] = Pieces.Black | Pieces.Rook;
            Squares[1] = Pieces.Black | Pieces.Knight;
            Squares[2] = Pieces.Black | Pieces.Bishop;
            Squares[3] = Pieces.Black | Pieces.King;
            Squares[4] = Pieces.Black | Pieces.Queen;
            Squares[5] = Pieces.Black | Pieces.Bishop;
            Squares[6] = Pieces.Black | Pieces.Knight;
            Squares[7] = Pieces.Black | Pieces.Rook;

            for (int i = 8; i < 16; i++)
            {
                Squares[i] = Pieces.Black | Pieces.Pawn;
            }
        }

        private void InitFlags()
        {
            Turn = Turn.White;
            Castling = Castling.BlackKingSide
                    | Castling.BlackQueenSide
                    | Castling.WhiteKingSide
                    | Castling.WhiteQueenSide;
            EnPassantTarget = -1;
            HalfmoveClock = 0;
            FullmoveNumber = 0;
        }

        public void FenToBoard(string fen)
        {
            Array.Clear(Squares, 0, Squares.Length);
            int sqareCount = 0;
            foreach (var c in fen)
            {
                if (sqareCount > 63) break;
                switch (c)
                {
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                        int emptyCount = (int)char.GetNumericValue(c);
                        sqareCount += emptyCount;
                        break;
                    case 'p':
                        Squares[sqareCount] = Pieces.Black | Pieces.Pawn;
                        sqareCount++;
                        break;
                    case 'n':
                        Squares[sqareCount] = Pieces.Black | Pieces.Knight;
                        sqareCount++;
                        break;
                    case 'b':
                        Squares[sqareCount] = Pieces.Black | Pieces.Bishop;
                        sqareCount++;
                        break;
                    case 'r':
                        Squares[sqareCount] = Pieces.Black | Pieces.Rook;
                        sqareCount++;
                        break;
                    case 'q':
                        Squares[sqareCount] = Pieces.Black | Pieces.Queen;
                        sqareCount++;
                        break;
                    case 'k':
                        Squares[sqareCount] = Pieces.Black | Pieces.King;
                        sqareCount++;
                        break;
                    case 'P':
                        Squares[sqareCount] = Pieces.White | Pieces.Pawn;
                        sqareCount++;
                        break;
                    case 'N':
                        Squares[sqareCount] = Pieces.White | Pieces.Knight;
                        sqareCount++;
                        break;
                    case 'B':
                        Squares[sqareCount] = Pieces.White | Pieces.Bishop;
                        sqareCount++;
                        break;
                    case 'R':
                        Squares[sqareCount] = Pieces.White | Pieces.Rook;
                        sqareCount++;
                        break;
                    case 'Q':
                        Squares[sqareCount] = Pieces.White | Pieces.Queen;
                        sqareCount++;
                        break;
                    case 'K':
                        Squares[sqareCount] = Pieces.White | Pieces.King;
                        sqareCount++;
                        break;
                    case '/':
                        break;
                    default:
                        throw new Exception("Illegal character in FEN.");
                }
            }//pnbrqk

            var flags = fen.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            InitFlagsFen(flags[1..]);

        }

        private void InitFlagsFen(string[] flags)
        {
            var turn = flags[0].ToLower();
            switch (turn)
            {
                case "w":
                    Turn = Turn.White;
                    break;
                case "b":
                    Turn = Turn.Black;
                    break;
            }

            var castling = flags[1];

            Castling = Castling.None;
            foreach (char c in castling)
            {
                switch (c)
                {
                    case 'k':
                        Castling = Castling | Castling.BlackKingSide;
                        break;
                    case 'K':
                        Castling = Castling | Castling.WhiteKingSide;
                        break;
                    case 'q':
                        Castling = Castling | Castling.BlackQueenSide;
                        break;
                    case 'Q':
                        Castling = Castling | Castling.WhiteQueenSide;
                        break;
                    case '-':
                    default:
                        break;
                }

            }

            EnPassantTarget = AlgebircToBoardIndex(flags[2].ToLower());
            HalfmoveClock = int.Parse(flags[3]);
            FullmoveNumber = int.Parse(flags[4]);
        }
        #endregion

        #region Helper Methods
        private int AlgebircToBoardIndex(string algebircNotation)
        {
            return (algebircNotation[0] - 96) + 8 * ((int)char.GetNumericValue(algebircNotation[1]) - 1) - 1;
        }
        #endregion

    }
}
