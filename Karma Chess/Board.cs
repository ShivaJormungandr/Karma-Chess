namespace Karma_Chess
{
    public class Board
    {
        public Pieces[,] Squares;
        public Turn Turn;
        public Castling Castling;
        //-1 For no target, 0-63 for EnPassant spre target index
        public int EnPassantTarget;
        public int HalfmoveClock;
        public int FullmoveNumber;
        public List<(int from, int to, int promote)> PseudoLegalMoves;


        public Board(Form mask)
        {
            Squares = new Pieces[8, 8];
            PseudoLegalMoves = new List<(int from, int to, int promote)>();
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

            Squares[0, 7] = Pieces.White | Pieces.Rook;
            Squares[1, 7] = Pieces.White | Pieces.Knight;
            Squares[2, 7] = Pieces.White | Pieces.Bishop;
            Squares[3, 7] = Pieces.White | Pieces.King;
            Squares[4, 7] = Pieces.White | Pieces.Queen;
            Squares[5, 7] = Pieces.White | Pieces.Bishop;
            Squares[6, 7] = Pieces.White | Pieces.Knight;
            Squares[7, 7] = Pieces.White | Pieces.Rook;

            for (int i = 0; i < 8; i++)
            {
                Squares[i, 6] = Pieces.White | Pieces.Pawn;
            }
        }

        private void InitBlackPieces()
        {
            Squares[0, 0] = Pieces.Black | Pieces.Rook;
            Squares[1, 0] = Pieces.Black | Pieces.Knight;
            Squares[2, 0] = Pieces.Black | Pieces.Bishop;
            Squares[3, 0] = Pieces.Black | Pieces.King;
            Squares[4, 0] = Pieces.Black | Pieces.Queen;
            Squares[5, 0] = Pieces.Black | Pieces.Bishop;
            Squares[6, 0] = Pieces.Black | Pieces.Knight;
            Squares[7, 0] = Pieces.Black | Pieces.Rook;

            for (int i = 0; i < 8; i++)
            {
                Squares[i, 1] = Pieces.Black | Pieces.Pawn;
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
            int XCount = 0;
            int YCount = 0;
            foreach (var c in fen)
            {
                if (XCount > 7)
                {
                    YCount++;
                    XCount = 0;
                    if (YCount > 7)
                    {
                        break;
                    }
                }

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
                        XCount += emptyCount;
                        break;
                    case 'p':
                        Squares[XCount, YCount] = Pieces.Black | Pieces.Pawn;
                        XCount++;
                        break;
                    case 'n':
                        Squares[XCount, YCount] = Pieces.Black | Pieces.Knight;
                        XCount++;
                        break;
                    case 'b':
                        Squares[XCount, YCount] = Pieces.Black | Pieces.Bishop;
                        XCount++;
                        break;
                    case 'r':
                        Squares[XCount, YCount] = Pieces.Black | Pieces.Rook;
                        XCount++;
                        break;
                    case 'q':
                        Squares[XCount, YCount] = Pieces.Black | Pieces.Queen;
                        XCount++;
                        break;
                    case 'k':
                        Squares[XCount, YCount] = Pieces.Black | Pieces.King;
                        XCount++;
                        break;
                    case 'P':
                        Squares[XCount, YCount] = Pieces.White | Pieces.Pawn;
                        XCount++;
                        break;
                    case 'N':
                        Squares[XCount, YCount] = Pieces.White | Pieces.Knight;
                        XCount++;
                        break;
                    case 'B':
                        Squares[XCount, YCount] = Pieces.White | Pieces.Bishop;
                        XCount++;
                        break;
                    case 'R':
                        Squares[XCount, YCount] = Pieces.White | Pieces.Rook;
                        XCount++;
                        break;
                    case 'Q':
                        Squares[XCount, YCount] = Pieces.White | Pieces.Queen;
                        XCount++;
                        break;
                    case 'K':
                        Squares[XCount, YCount] = Pieces.White | Pieces.King;
                        XCount++;
                        break;
                    case '/':
                        break;
                    default:
                        throw new Exception("Illegal character in FEN.");
                }
            }

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

            if (flags[2].Equals("-"))
            {
                EnPassantTarget = -1;
            }
            else
            {
                EnPassantTarget = AlgebircToBoardIndex(flags[2].ToLower());
            }
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

        /// <summary>
        /// Returns true if a move was made on the board, false otherwhise.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public bool Move(int from, int to)
        {
            if (from < 0 || from > 63 || to < 0 || to > 63) return false;
            return false;
        }

        public void CalculatePseudoLegalMoves()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    switch (Squares[i, j])
                    {
                        case Pieces.Pawn:
                            if (Squares[i, j].IsWhite())
                            {

                            }
                            else if (Squares[i, j].IsBlack())
                            {

                            }
                            break;
                        case Pieces.Knight:
                            break;
                        case Pieces.Bishop:
                            break;
                        case Pieces.Rook:
                            break;
                        case Pieces.Queen:
                            break;
                        case Pieces.King:
                            break;
                        case Pieces.None:
                        default:
                            break;
                    }
                }
            }
        }
    }
}
