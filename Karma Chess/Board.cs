using System.Linq;

namespace Karma_Chess
{
    public class Board
    {
        public Pieces[,] Squares;
        public Pieces Turn;
        public Castling Castling;
        //(-1,-1) For no target or the indexes for the EnPasant Sqare
        public (int, int) EnPassantTarget;
        public int HalfmoveClock;
        public int FullmoveNumber;
        //Special holds Promotions if Pawn or Casttling if King
        //Special defaul is 0, positive is promotion, negative is castling
        public List<((int file, int rank) from, (int file, int rank) to, int Special)> LegalMoves;

        public Board()
        {
            Squares = new Pieces[8, 8];
            LegalMoves = new List<((int file, int rank) from, (int file, int rank) to, int Special)>();
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
            Squares[3, 0] = Pieces.White | Pieces.Queen;
            Squares[4, 0] = Pieces.White | Pieces.King;
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
            Squares[3, 7] = Pieces.Black | Pieces.Queen;
            Squares[4, 7] = Pieces.Black | Pieces.King;
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
            Turn = Pieces.White;
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
            int fileCount = 0;
            int rankCount = 7;
            foreach (var c in fen)
            {
                if (fileCount > 7)
                {
                    rankCount--;
                    fileCount = 0;
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
                        fileCount += emptyCount;
                        break;
                    case 'p':
                        Squares[fileCount, rankCount] = Pieces.Black | Pieces.Pawn;
                        fileCount++;
                        break;
                    case 'n':
                        Squares[fileCount, rankCount] = Pieces.Black | Pieces.Knight;
                        fileCount++;
                        break;
                    case 'b':
                        Squares[fileCount, rankCount] = Pieces.Black | Pieces.Bishop;
                        fileCount++;
                        break;
                    case 'r':
                        Squares[fileCount, rankCount] = Pieces.Black | Pieces.Rook;
                        fileCount++;
                        break;
                    case 'q':
                        Squares[fileCount, rankCount] = Pieces.Black | Pieces.Queen;
                        fileCount++;
                        break;
                    case 'k':
                        Squares[fileCount, rankCount] = Pieces.Black | Pieces.King;
                        fileCount++;
                        break;
                    case 'P':
                        Squares[fileCount, rankCount] = Pieces.White | Pieces.Pawn;
                        fileCount++;
                        break;
                    case 'N':
                        Squares[fileCount, rankCount] = Pieces.White | Pieces.Knight;
                        fileCount++;
                        break;
                    case 'B':
                        Squares[fileCount, rankCount] = Pieces.White | Pieces.Bishop;
                        fileCount++;
                        break;
                    case 'R':
                        Squares[fileCount, rankCount] = Pieces.White | Pieces.Rook;
                        fileCount++;
                        break;
                    case 'Q':
                        Squares[fileCount, rankCount] = Pieces.White | Pieces.Queen;
                        fileCount++;
                        break;
                    case 'K':
                        Squares[fileCount, rankCount] = Pieces.White | Pieces.King;
                        fileCount++;
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
                    Turn = Pieces.White;
                    break;
                case "b":
                    Turn = Pieces.Black;
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
            try
            {
                HalfmoveClock = int.Parse(flags[3]);
                FullmoveNumber = int.Parse(flags[4]);
            }
            catch (Exception ex)
            {
                //TODO: Log here
            }
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
        public bool Move((int file, int rank) from, (int file, int rank) to, int Special)
        {
            if (from.file < 0 || from.rank < 0
                || from.file > 7 || from.rank > 7
                || to.file < 0 || to.rank < 0
                || to.file > 7 || to.rank > 7
                ) return false;
            return false;
        }

        private void CalculatePseudoLegalMoves()
        {
            for (int file = 0; file < 8; file++)
            {
                for (int rank = 0; rank < 8; rank++)
                {
                    if ((Squares[file, rank] & Pieces.ColorMask).Equals(Turn))
                        switch (Squares[file, rank] & Pieces.PieceMask)
                        {
                            case Pieces.Pawn:
                                if (Squares[file, rank].IsWhite())
                                {
                                    #region White Pawn Moves
                                    //Moves from the 2nd Rank (2 ranks up)
                                    if (rank == 1)
                                    {
                                        if (Squares[file, rank + 1].IsEmpty() && Squares[file, rank + 2].IsEmpty())
                                        {
                                            LegalMoves.Add(((file, rank), (file, rank + 2), 0));
                                        }
                                    }
                                    //Normal Moves (1 rank up)
                                    if (Squares[file, rank + 1].IsEmpty())
                                    {
                                        //if next rank is the last one add promotions to the board too
                                        if (rank + 1 == 7)
                                        {
                                            //Pawn to Knight
                                            LegalMoves.Add(((file, rank), (file, rank + 1), 1));
                                            //Pawn to Bishop
                                            LegalMoves.Add(((file, rank), (file, rank + 1), 2));
                                            //Pawn to Rook
                                            LegalMoves.Add(((file, rank), (file, rank + 1), 3));
                                            //Pawn to Queen
                                            LegalMoves.Add(((file, rank), (file, rank + 1), 4));
                                        }
                                        else
                                        {
                                            LegalMoves.Add(((file, rank), (file, rank + 1), 0));
                                        }
                                    }
                                    #endregion

                                    #region White Pawn Captures
                                    //Captures (1 rank diagonal and EnPassant)
                                    if (file + 1 <= 7)
                                    {
                                        if ((Squares[file + 1, rank + 1].IsBlack()
                                            || (file + 1, rank + 1).Equals(EnPassantTarget))
                                            && !Squares[file + 1, rank + 1].IsKing())
                                        {
                                            //if next rank is the last one add promotions to the board too
                                            if (rank + 1 == 7)
                                            {
                                                //Pawn to Knight
                                                LegalMoves.Add(((file, rank), (file + 1, rank + 1), 1));
                                                //Pawn to Bishop
                                                LegalMoves.Add(((file, rank), (file + 1, rank + 1), 2));
                                                //Pawn to Rook
                                                LegalMoves.Add(((file, rank), (file + 1, rank + 1), 3));
                                                //Pawn to Queen
                                                LegalMoves.Add(((file, rank), (file + 1, rank + 1), 4));
                                            }
                                            else
                                            {
                                                LegalMoves.Add(((file, rank), (file + 1, rank + 1), 0));
                                            }
                                        }
                                    }

                                    if (file - 1 >= 0)
                                    {
                                        if ((Squares[file - 1, rank + 1].IsBlack()
                                            || (file - 1, rank + 1).Equals(EnPassantTarget))
                                            && !Squares[file - 1, rank + 1].IsKing())
                                        {
                                            //if next rank is the last one add promotions to the board too
                                            if (rank + 1 == 7)
                                            {
                                                //Pawn to Knight
                                                LegalMoves.Add(((file, rank), (file - 1, rank + 1), 1));
                                                //Pawn to Bishop
                                                LegalMoves.Add(((file, rank), (file - 1, rank + 1), 2));
                                                //Pawn to Rook
                                                LegalMoves.Add(((file, rank), (file - 1, rank + 1), 3));
                                                //Pawn to Queen
                                                LegalMoves.Add(((file, rank), (file - 1, rank + 1), 4));
                                            }
                                            else
                                            {
                                                //Pawn to Knight
                                                LegalMoves.Add(((file, rank), (file - 1, rank + 1), 1));
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                else if (Squares[file, rank].IsBlack())
                                {
                                    #region Black Pawn Moves
                                    //Moves from the 2nd Rank (2 ranks up)
                                    if (rank == 6)
                                    {
                                        if (Squares[file, rank - 1].IsEmpty() && Squares[file, rank - 2].IsEmpty())
                                        {
                                            LegalMoves.Add(((file, rank), (file, rank - 2), 0));
                                        }
                                    }
                                    //Normal Moves (1 rank up)
                                    if (Squares[file, rank - 1].IsEmpty())
                                    {
                                        //if next rank is the last one add promotions to the board too
                                        if (rank - 1 == 0)
                                        {
                                            //Pawn to Knight
                                            LegalMoves.Add(((file, rank), (file, rank - 1), 1));
                                            //Pawn to Bishop
                                            LegalMoves.Add(((file, rank), (file, rank - 1), 2));
                                            //Pawn to Rook
                                            LegalMoves.Add(((file, rank), (file, rank - 1), 3));
                                            //Pawn to Queen
                                            LegalMoves.Add(((file, rank), (file, rank - 1), 4));
                                        }
                                        else
                                        {
                                            LegalMoves.Add(((file, rank), (file, rank - 1), 0));
                                        }
                                    }
                                    #endregion

                                    #region Black Pawn Captures
                                    //Captures (1 rank diagonal and EnPassant)
                                    if (file + 1 <= 7)
                                    {
                                        if ((Squares[file + 1, rank - 1].IsBlack()
                                            || (file + 1, rank - 1).Equals(EnPassantTarget))
                                            && !Squares[file + 1, rank - 1].IsKing())
                                        {
                                            //if next rank is the last one add promotions to the board too
                                            if (rank + 1 == 7)
                                            {
                                                //Pawn to Knight
                                                LegalMoves.Add(((file, rank), (file + 1, rank - 1), 1));
                                                //Pawn to Bishop
                                                LegalMoves.Add(((file, rank), (file + 1, rank - 1), 2));
                                                //Pawn to Rook
                                                LegalMoves.Add(((file, rank), (file + 1, rank - 1), 3));
                                                //Pawn to Queen
                                                LegalMoves.Add(((file, rank), (file + 1, rank - 1), 4));
                                            }
                                            else
                                            {
                                                LegalMoves.Add(((file, rank), (file + 1, rank - 1), 0));
                                            }
                                        }
                                    }

                                    if (file - 1 >= 0)
                                    {
                                        if ((Squares[file - 1, rank - 1].IsBlack()
                                            || (file - 1, rank - 1).Equals(EnPassantTarget))
                                            && !Squares[file - 1, rank - 1].IsKing())
                                        {
                                            //if next rank is the last one add promotions to the board too
                                            if (rank + 1 == 7)
                                            {
                                                //Pawn to Knight
                                                LegalMoves.Add(((file, rank), (file - 1, rank - 1), 1));
                                                //Pawn to Bishop
                                                LegalMoves.Add(((file, rank), (file - 1, rank - 1), 2));
                                                //Pawn to Rook
                                                LegalMoves.Add(((file, rank), (file - 1, rank - 1), 3));
                                                //Pawn to Queen
                                                LegalMoves.Add(((file, rank), (file - 1, rank - 1), 4));
                                            }
                                            else
                                            {
                                                //Pawn to Knight
                                                LegalMoves.Add(((file, rank), (file - 1, rank - 1), 1));
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                break;
                            case Pieces.Knight:
                                #region Knight Moves
                                if (file - 2 >= 0 && rank + 1 <= 7)
                                {
                                    if (Squares[file - 2, rank + 1].IsEmpty()
                                        || !(Squares[file - 2, rank + 1]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file - 2, rank + 1), 0));
                                    }
                                }
                                if (file - 1 >= 0 && rank + 2 <= 7)
                                {
                                    if (Squares[file - 1, rank + 2].IsEmpty()
                                        || !(Squares[file - 1, rank + 2]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file - 1, rank + 2), 0));
                                    }
                                }
                                if (file + 1 <= 7 && rank + 2 <= 7)
                                {
                                    if (Squares[file + 1, rank + 2].IsEmpty()
                                        || !(Squares[file + 1, rank + 2]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file + 1, rank + 2), 0));
                                    }
                                }
                                if (file + 2 <= 7 && rank + 1 <= 7)
                                {
                                    if (Squares[file + 2, rank + 1].IsEmpty()
                                        || !(Squares[file + 2, rank + 1]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file + 2, rank + 1), 0));
                                    }

                                }
                                if (file + 2 <= 7 && rank - 1 >= 0)
                                {
                                    if (Squares[file + 2, rank - 1].IsEmpty()
                                        || !(Squares[file + 2, rank - 1]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file + 2, rank - 1), 0));
                                    }
                                }
                                if (file + 1 <= 7 && rank - 2 >= 0)
                                {
                                    if (Squares[file + 1, rank - 2].IsEmpty()
                                        || !(Squares[file + 1, rank - 2]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file + 1, rank - 2), 0));
                                    }
                                }
                                if (file - 1 >= 0 && rank - 2 >= 0)
                                {
                                    if (Squares[file - 1, rank - 2].IsEmpty()
                                        || !(Squares[file - 1, rank - 2]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file - 1, rank - 2), 0));
                                    }
                                }
                                if (file - 2 >= 0 && rank - 1 >= 0)
                                {
                                    if (Squares[file - 2, rank - 1].IsEmpty()
                                        || !(Squares[file - 2, rank - 1]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file - 2, rank - 1), 0));
                                    }
                                }
                                #endregion
                                break;
                            case Pieces.Bishop:
                                #region Bishop Moves
                                //NW
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (file - distance < 0 || rank + distance > 7) break;
                                    if (Squares[file - distance, rank + distance].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file - distance, rank + distance), 0));
                                    }
                                    else if (!(Squares[file - distance, rank + distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file - distance, rank + distance), 0));
                                        break;
                                    }
                                    else if ((Squares[file - distance, rank + distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                //NE
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (file + distance > 7 || rank + distance > 7) break;
                                    if (Squares[file + distance, rank + distance].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file + distance, rank + distance), 0));
                                    }
                                    else if (!(Squares[file + distance, rank + distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file + distance, rank + distance), 0));
                                        break;
                                    }
                                    else if ((Squares[file + distance, rank + distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                //SE
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (file + distance > 7 || rank - distance < 0) break;
                                    if (Squares[file + distance, rank - distance].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file + distance, rank - distance), 0));
                                    }
                                    else if (!(Squares[file + distance, rank - distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file + distance, rank - distance), 0));
                                        break;
                                    }
                                    else if ((Squares[file + distance, rank - distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                //SW
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (file - distance < 0 || rank - distance < 0) break;
                                    if (Squares[file - distance, rank - distance].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file - distance, rank - distance), 0));
                                    }
                                    else if (!(Squares[file - distance, rank - distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file - distance, rank - distance), 0));
                                        break;
                                    }
                                    else if (!(Squares[file - distance, rank - distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                #endregion
                                break;
                            case Pieces.Rook:
                                #region Rook Moves
                                //N
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (rank + distance > 7) break;
                                    if (Squares[file, rank + distance].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file, rank + distance), 0));
                                    }
                                    else if (!(Squares[file, rank + distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file, rank + distance), 0));
                                        break;
                                    }
                                    else if ((Squares[file, rank + distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                //E
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (file + distance > 7) break;
                                    if (Squares[file + distance, rank].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file + distance, rank), 0));
                                    }
                                    else if (!(Squares[file + distance, rank]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file + distance, rank), 0));
                                        break;
                                    }
                                    else if ((Squares[file + distance, rank]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                //S
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (rank - distance < 0) break;
                                    if (Squares[file, rank - distance].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file, rank - distance), 0));
                                    }
                                    else if (!(Squares[file, rank - distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file, rank - distance), 0));
                                        break;
                                    }
                                    else if ((Squares[file, rank - distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                //W
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (file - distance < 0) break;
                                    if (Squares[file - distance, rank].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file - distance, rank), 0));
                                    }
                                    else if (!(Squares[file - distance, rank]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file - distance, rank), 0));
                                        break;
                                    }
                                    else if ((Squares[file - distance, rank]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                #endregion
                                break;
                            case Pieces.Queen:
                                #region Queen Moves
                                //NW
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (file - distance < 0 || rank + distance > 7) break;
                                    if (Squares[file - distance, rank + distance].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file - distance, rank + distance), 0));
                                    }
                                    else if (!(Squares[file - distance, rank + distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file - distance, rank + distance), 0));
                                        break;
                                    }
                                    else if ((Squares[file - distance, rank + distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                //NE
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (file + distance > 7 || rank + distance > 7) break;
                                    if (Squares[file + distance, rank + distance].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file + distance, rank + distance), 0));
                                    }
                                    else if (!(Squares[file + distance, rank + distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file + distance, rank + distance), 0));
                                        break;
                                    }
                                    else if ((Squares[file + distance, rank + distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                //SE
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (file + distance > 7 || rank - distance < 0) break;
                                    if (Squares[file + distance, rank - distance].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file + distance, rank - distance), 0));
                                    }
                                    else if (!(Squares[file + distance, rank - distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file + distance, rank - distance), 0));
                                        break;
                                    }
                                    else if ((Squares[file + distance, rank - distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                //SW
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (file - distance < 0 || rank - distance < 0) break;
                                    if (Squares[file - distance, rank - distance].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file - distance, rank - distance), 0));
                                    }
                                    else if (!(Squares[file - distance, rank - distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file - distance, rank - distance), 0));
                                        break;
                                    }
                                    else if (!(Squares[file - distance, rank - distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                //N
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (rank + distance > 7) break;
                                    if (Squares[file, rank + distance].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file, rank + distance), 0));
                                    }
                                    else if (!(Squares[file, rank + distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file, rank + distance), 0));
                                        break;
                                    }
                                    else if ((Squares[file, rank + distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                //E
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (file + distance > 7) break;
                                    if (Squares[file + distance, rank].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file + distance, rank), 0));
                                    }
                                    else if (!(Squares[file + distance, rank]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file + distance, rank), 0));
                                        break;
                                    }
                                    else if ((Squares[file + distance, rank]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                //S
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (rank - distance < 0) break;
                                    if (Squares[file, rank - distance].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file, rank - distance), 0));
                                    }
                                    else if (!(Squares[file, rank - distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file, rank - distance), 0));
                                        break;
                                    }
                                    else if ((Squares[file, rank - distance]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                //W
                                for (int distance = 1; distance < 8; distance++)
                                {
                                    if (file - distance < 0) break;
                                    if (Squares[file - distance, rank].IsEmpty())
                                    {
                                        LegalMoves.Add(((file, rank), (file - distance, rank), 0));
                                    }
                                    else if (!(Squares[file - distance, rank]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file - distance, rank), 0));
                                        break;
                                    }
                                    else if ((Squares[file - distance, rank]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        break;
                                    }
                                }
                                #endregion
                                break;
                            case Pieces.King:
                                #region King Moves
                                //NW
                                if (file - 1 >= 0 && rank + 1 <= 7)
                                {
                                    if (Squares[file - 1, rank + 1].IsEmpty()
                                        || !(Squares[file - 1, rank + 1]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file - 1, rank + 1), 0));
                                    }
                                }
                                //N
                                if (rank + 1 <= 7)
                                {
                                    if (Squares[file, rank + 1].IsEmpty()
                                        || !(Squares[file, rank + 1]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file, rank + 1), 0));
                                    }
                                }
                                //NE
                                if (file + 1 <= 7 && rank + 1 <= 7)
                                {
                                    if (Squares[file + 1, rank + 1].IsEmpty()
                                        || !(Squares[file + 1, rank + 1]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file + 1, rank + 1), 0));
                                    }
                                }
                                //E
                                if (file + 1 <= 7)
                                {
                                    if (Squares[file + 1, rank].IsEmpty()
                                        || !(Squares[file + 1, rank]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file + 1, rank), 0));
                                    }
                                }
                                //SE
                                if (file + 1 <= 7 && rank - 1 >= 0)
                                {
                                    if (Squares[file + 1, rank - 1].IsEmpty()
                                        || !(Squares[file + 1, rank - 1]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file + 1, rank - 1), 0));
                                    }
                                }
                                //S
                                if (rank - 1 >= 0)
                                {
                                    if (Squares[file, rank - 1].IsEmpty()
                                        || !(Squares[file, rank - 1]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file, rank - 1), 0));
                                    }
                                }
                                //SW
                                if (file - 1 >= 0 && rank - 1 >= 0)
                                {
                                    if (Squares[file - 1, rank - 1].IsEmpty()
                                        || !(Squares[file - 1, rank - 1]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file - 1, rank - 1), 0));
                                    }
                                }
                                //W
                                if (file - 1 >= 0)
                                {
                                    if (Squares[file - 1, rank].IsEmpty()
                                        || !(Squares[file - 1, rank]
                                            & Pieces.ColorMask).Equals(Turn))
                                    {
                                        LegalMoves.Add(((file, rank), (file - 1, rank), 0));
                                    }
                                }
                                //Castling
                                if (Squares[file, rank].IsWhite())
                                {
                                    if ((Castling & Castling.WhiteKingSide) == Castling.WhiteKingSide
                                        && Squares[file + 1, rank].IsEmpty()
                                        && Squares[file + 2, rank].IsEmpty()
                                        && Squares[file + 3, rank].IsRook())
                                    {
                                        LegalMoves.Add(((file, rank), (file + 2, rank), -1));
                                    }
                                    if ((Castling & Castling.WhiteQueenSide) == Castling.WhiteQueenSide
                                        && Squares[file - 1, rank].IsEmpty()
                                        && Squares[file - 2, rank].IsEmpty()
                                        && Squares[file - 3, rank].IsEmpty()
                                        && Squares[file - 4, rank].IsRook())
                                    {
                                        LegalMoves.Add(((file, rank), (file - 2, rank), -1));
                                    }
                                }
                                else if (Squares[file, rank].IsBlack())
                                {
                                    if ((Castling & Castling.BlackKingSide) == Castling.BlackKingSide
                                        && Squares[file + 1, rank].IsEmpty()
                                        && Squares[file + 2, rank].IsEmpty()
                                        && Squares[file + 3, rank].IsRook())
                                    {
                                        LegalMoves.Add(((file, rank), (file + 2, rank), -1));
                                    }
                                    if ((Castling & Castling.BlackQueenSide) == Castling.BlackQueenSide
                                        && Squares[file - 1, rank].IsEmpty()
                                        && Squares[file - 2, rank].IsEmpty()
                                        && Squares[file - 3, rank].IsEmpty()
                                        && Squares[file - 4, rank].IsRook())
                                    {
                                        LegalMoves.Add(((file, rank), (file - 2, rank), -1));
                                    }
                                }
                                #endregion
                                break;
                            case Pieces.None:
                            default:
                                break;
                        }
                }
            }
        }

        public void CalculateLegalMoves()
        {
            CalculatePseudoLegalMoves();
            foreach (var move in LegalMoves)
            {
                Pieces[,] tempSqares = Squares.Clone() as Pieces[,];
                tempSqares[move.to.file, move.to.rank] = tempSqares[move.from.file, move.from.rank];
                tempSqares[move.from.file, move.from.rank] = Pieces.None;
                //TODO: Find king of turn color, check if in check, if yes remove from legal moves

            }
        }
    }
}
