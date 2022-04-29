namespace Karma_Chess
{
    public class Board
    {
        public Piece[] Squares;
        public Turn Turn;
        public Castling Castling;
        //-1 For no target, 0-63 for EnPassant sqre target index
        public int EnPassantTarget;
        public int HalfmoveClock;
        public int FullmoveNumber;

        public Board()
        {
            Squares = new Piece[64];
        }

        #region Board Inits
        public void InitStartBoard()
        {
            InitBlackPieces();
            InitWhitePieces();
            InitFlags();
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
                        Squares[sqareCount] = Piece.Black | Piece.Pawn;
                        sqareCount++;
                        break;
                    case 'n':
                        Squares[sqareCount] = Piece.Black | Piece.Knight;
                        sqareCount++;
                        break;
                    case 'b':
                        Squares[sqareCount] = Piece.Black | Piece.Bishop;
                        sqareCount++;
                        break;
                    case 'r':
                        Squares[sqareCount] = Piece.Black | Piece.Rook;
                        sqareCount++;
                        break;
                    case 'q':
                        Squares[sqareCount] = Piece.Black | Piece.Queen;
                        sqareCount++;
                        break;
                    case 'k':
                        Squares[sqareCount] = Piece.Black | Piece.King;
                        sqareCount++;
                        break;
                    case 'P':
                        Squares[sqareCount] = Piece.White | Piece.Pawn;
                        sqareCount++;
                        break;
                    case 'N':
                        Squares[sqareCount] = Piece.White | Piece.Knight;
                        sqareCount++;
                        break;
                    case 'B':
                        Squares[sqareCount] = Piece.White | Piece.Bishop;
                        sqareCount++;
                        break;
                    case 'R':
                        Squares[sqareCount] = Piece.White | Piece.Rook;
                        sqareCount++;
                        break;
                    case 'Q':
                        Squares[sqareCount] = Piece.White | Piece.Queen;
                        sqareCount++;
                        break;
                    case 'K':
                        Squares[sqareCount] = Piece.White | Piece.King;
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

        #region Piece Checks
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
        #endregion
    }
}
