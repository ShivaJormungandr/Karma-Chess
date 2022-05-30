using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma_Chess
{
    class MinMax
    {
        public ((int file, int rank) from, (int file, int rank) to, int Special) bestMoveMinMix;
        private int count = 0;
        private int Evaluate(Board board, Pieces maximizingColor)
        {
            var whiteScore = 0;
            var blackScore = 0;
            foreach (var sqare in board.Squares)
            {
                if (sqare != Pieces.None)
                {
                    var pieceColor = sqare & Pieces.ColorMask;
                    var piece = sqare & Pieces.PieceMask;

                    switch (pieceColor)
                    {
                        case Pieces.White:
                            whiteScore += GetPieceWorth(piece);
                            break;
                        case Pieces.Black:
                            blackScore += GetPieceWorth(piece);
                            break;
                    }
                }
            }

            if (board.IsInCkeck)
            {
                switch (board.Turn)
                {
                    case Pieces.White:
                        whiteScore -= GetPieceWorth(Pieces.King);
                        break;
                    case Pieces.Black:
                        blackScore -= GetPieceWorth(Pieces.King);
                        break;
                }
            }
            if (maximizingColor.IsWhite())
                return whiteScore - blackScore;
            else
                return blackScore - whiteScore;
        }
        private int GetPieceWorth(Pieces piece)
        {
            //TODO: Get rid of the magic numbers
            switch (piece)
            {
                case Pieces.Pawn:
                    return 10;
                case Pieces.Knight:
                    return 30;
                case Pieces.Bishop:
                    return 30;
                case Pieces.Rook:
                    return 50;
                case Pieces.Queen:
                    return 90;
                case Pieces.King:
                    return 900;
            }

            return 0;
        }

        public int MinMaxFunc(Board board, int depth, int alpha, int beta, bool maximizingPlayer, Pieces maximizingColor)
        {
            count++;
            if (depth == 0 || board.LegalMoves.Count == 0 || board.CheckMate)
            {
                return Evaluate(board, maximizingColor);
            }

            ((int file, int rank) from, (int file, int rank) to, int Special) bestMove = board.LegalMoves[0];

            if (maximizingPlayer)
            {
                int maxEval = int.MinValue;
                //foreach (var move in board.LegalMoves)
                Parallel.ForEach(board.LegalMoves, (move, state) =>
                {
                    var boardClone = board.CopyObject<Board>();
                    _ = boardClone.Move(move.from, move.to, move.Special);
                    boardClone.CalculateLegalMoves();

                    int eval = MinMaxFunc(boardClone, depth - 1, alpha, beta, !maximizingPlayer, maximizingColor);
                    if (eval > maxEval)
                    {
                        bestMove = move;
                    }
                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha) state.Break();
                });
                bestMoveMinMix = bestMove;
                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                //foreach (var move in board.LegalMoves)
                Parallel.ForEach(board.LegalMoves, (move, state) =>
                {
                    var boardClone = board.CopyObject<Board>();
                    _ = boardClone.Move(move.from, move.to, move.Special);
                    boardClone.CalculateLegalMoves();

                    int eval = MinMaxFunc(boardClone, depth - 1, alpha, beta, maximizingPlayer, maximizingColor);
                    if (eval < minEval)
                    {
                        bestMove = move;
                    }
                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha) state.Break();
                });
                bestMoveMinMix = bestMove;
                return minEval;
            }

        }

    }
}
