﻿namespace Karma_Chess
{
    public class Board
    {
        public Pieces[,] Squares;
        public Turn Turn;
        public Castling Castling;
        //(-1,-1) For no target or the indexes for the EnPasant Sqare
        public (int, int) EnPassantTarget;
        public int HalfmoveClock;
        public int FullmoveNumber;
        public List<((int file, int rank) from, (int file, int rank) to, int Promote)> PseudoLegalMoves;

        public Board()
        {
            Squares = new Pieces[8, 8];
            PseudoLegalMoves = new List<((int file, int rank) from, (int file, int rank) to, int Promote)>();
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

            Squares[0, 0] = Pieces.White | Pieces.Rook;
            Squares[1, 0] = Pieces.White | Pieces.Knight;
            Squares[2, 0] = Pieces.White | Pieces.Bishop;
            Squares[3, 0] = Pieces.White | Pieces.King;
            Squares[4, 0] = Pieces.White | Pieces.Queen;
            Squares[5, 0] = Pieces.White | Pieces.Bishop;
            Squares[6, 0] = Pieces.White | Pieces.Knight;
            Squares[7, 0] = Pieces.White | Pieces.Rook;

            for (int i = 0; i < 8; i++)
            {
                Squares[i, 1] = Pieces.White | Pieces.Pawn;
            }
        }

        private void InitBlackPieces()
        {
            Squares[0, 7] = Pieces.Black | Pieces.Rook;
            Squares[1, 7] = Pieces.Black | Pieces.Knight;
            Squares[2, 7] = Pieces.Black | Pieces.Bishop;
            Squares[3, 7] = Pieces.Black | Pieces.King;
            Squares[4, 7] = Pieces.Black | Pieces.Queen;
            Squares[5, 7] = Pieces.Black | Pieces.Bishop;
            Squares[6, 7] = Pieces.Black | Pieces.Knight;
            Squares[7, 7] = Pieces.Black | Pieces.Rook;

            for (int i = 0; i < 8; i++)
            {
                Squares[i, 6] = Pieces.Black | Pieces.Pawn;
            }
        }

        private void InitFlags()
        {
            Turn = Turn.White;
            Castling = Castling.BlackKingSide
                    | Castling.BlackQueenSide
                    | Castling.WhiteKingSide
                    | Castling.WhiteQueenSide;
            EnPassantTarget = (-1, -1);
            HalfmoveClock = 0;
            FullmoveNumber = 0;
        }

        public void FenToBoard(string fen)
        {
            Array.Clear(Squares, 0, Squares.Length);
            int fileCount = 7;
            int rankCount = 7;
            foreach (var c in fen)
            {
                if (fileCount < 0)
                {
                    rankCount--;
                    fileCount = 7;
                    if (rankCount < 0)
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
                        fileCount -= emptyCount;
                        break;
                    case 'p':
                        Squares[fileCount, rankCount] = Pieces.Black | Pieces.Pawn;
                        fileCount--;
                        break;
                    case 'n':
                        Squares[fileCount, rankCount] = Pieces.Black | Pieces.Knight;
                        fileCount--;
                        break;
                    case 'b':
                        Squares[fileCount, rankCount] = Pieces.Black | Pieces.Bishop;
                        fileCount--;
                        break;
                    case 'r':
                        Squares[fileCount, rankCount] = Pieces.Black | Pieces.Rook;
                        fileCount--;
                        break;
                    case 'q':
                        Squares[fileCount, rankCount] = Pieces.Black | Pieces.Queen;
                        fileCount--;
                        break;
                    case 'k':
                        Squares[fileCount, rankCount] = Pieces.Black | Pieces.King;
                        fileCount--;
                        break;
                    case 'P':
                        Squares[fileCount, rankCount] = Pieces.White | Pieces.Pawn;
                        fileCount--;
                        break;
                    case 'N':
                        Squares[fileCount, rankCount] = Pieces.White | Pieces.Knight;
                        fileCount--;
                        break;
                    case 'B':
                        Squares[fileCount, rankCount] = Pieces.White | Pieces.Bishop;
                        fileCount--;
                        break;
                    case 'R':
                        Squares[fileCount, rankCount] = Pieces.White | Pieces.Rook;
                        fileCount--;
                        break;
                    case 'Q':
                        Squares[fileCount, rankCount] = Pieces.White | Pieces.Queen;
                        fileCount--;
                        break;
                    case 'K':
                        Squares[fileCount, rankCount] = Pieces.White | Pieces.King;
                        fileCount--;
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
                EnPassantTarget = (-1, -1);
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

        private (int, int) AlgebircToBoardIndex(string algebircNotation)
        {
            int file = algebircNotation[0] - 97;
            int rank = (int)char.GetNumericValue(algebircNotation[1]) - 1;
            return (file, rank);
        }

        #endregion

        /// <summary>
        /// Returns true if a move was made on the board, false otherwhise.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public bool Move((int file, int rank) from, (int file, int rank) to, int Promote)
        {
            if (from.file < 0 || from.rank < 0
                || from.file > 7 || from.rank > 7
                || to.file < 0|| to.rank < 0
                || to.file > 7 || to.rank > 7
                ) return false;
            return false;
        }

        public void CalculatePseudoLegalMoves()
        {
            for (int file = 0; file < 8; file++)
            {
                for (int rank = 0; rank < 8; rank++)
                {
                    switch (Squares[file, rank])
                    {
                        case Pieces.Pawn:
                            if (Squares[file, rank].IsWhite())
                            {

                            }
                            else if (Squares[file, rank].IsBlack())
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
