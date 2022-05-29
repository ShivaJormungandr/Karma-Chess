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
            if (maximizingColor == Pieces.White)
            {
                return board.WhiteScore - board.BlackScore;
            }
            else
            {
                return board.BlackScore - board.WhiteScore;
            }
        }

        public int MinMaxFunc(Board board, int depth,int alpha, int beta, bool maximizingPlayer, Pieces maximizingColor)
        {
            count++;
            if (depth == 0 || board.LegalMoves.Count == 0 || board.CheckMate)
            {
                return Evaluate(board, maximizingColor);
            }

            ((int file, int rank) from, (int file, int rank) to, int Special) bestMove = board.LegalMoves[0];

            if (maximizingPlayer)
            {
                int value = int.MinValue;
                foreach (var move in board.LegalMoves)
                {
                    var boardClone = board.CopyObject<Board>();
                    _ = boardClone.Move(move.from, move.to, move.Special);
                    boardClone.CalculateLegalMoves();

                    int minmaxResult = MinMaxFunc(boardClone, depth - 1, alpha, beta, !maximizingPlayer, maximizingColor);
                    if (value > minmaxResult)
                    {
                        bestMove = move;
                    }
                    value = Math.Max(value, minmaxResult);
                    alpha = Math.Max(alpha, minmaxResult);
                    if (beta <= alpha) break;
                }
                bestMoveMinMix = bestMove;
                return value;
            }
            else
            {
                int value = int.MaxValue;
                foreach (var move in board.LegalMoves)
                {
                    var boardClone = board.CopyObject<Board>();
                    _ = boardClone.Move(move.from, move.to, move.Special);
                    boardClone.CalculateLegalMoves();

                    int minmaxResult = MinMaxFunc(boardClone, depth - 1, alpha, beta, maximizingPlayer, maximizingColor);
                    if (value < minmaxResult)
                    {
                        bestMove = move;
                    }
                    value = Math.Min(value, minmaxResult);
                    beta = Math.Min(beta, minmaxResult);
                    if(beta <= alpha) break;
                }
                bestMoveMinMix = bestMove;
                return value;
            }

        }

    }
}
