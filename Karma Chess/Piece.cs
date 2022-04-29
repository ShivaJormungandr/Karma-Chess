﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma_Chess
{
    [Flags]
    public enum Piece
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
