namespace Karma_Chess
{
    public class Board
    {
        public int[] positions = { 6, 75, 144, 213, 282, 351, 420, 489, 558 };

        public Pieces[,] Squares;
        public Turn Turn;
        public Castling Castling;
        //-1 For no target, 0-63 for EnPassant spre target index
        public int EnPassantTarget;
        public int HalfmoveClock;
        public int FullmoveNumber;
        public Form Mask;

        public Board(Form mask)
        {
            Squares = new Pieces[8, 8];
            Mask = mask;
        }

        public void ReceiveClickedPiece(int x, int y)
        {
            int? Xpositions = null;
            int? Ypositions = null;

            for (int i = 0; i < 8; i++)
            {
                if (x >= positions[i] && x < positions[i + 1])
                {
                    Xpositions = i;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                if (y >= positions[i] && y < positions[i + 1])
                {
                    Ypositions = i;
                }
            }
            if (Xpositions != null && Ypositions != null)
            {
                var clickedPiece = Squares[(int)Xpositions, (int)Ypositions];
            }

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
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (Squares[x, y] == Pieces.None)
                    {
                        continue;
                    }

                    var piece = new Piece(x, y, Mask, Squares[x, y]);
                }
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
                    if (XCount > 7)
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
