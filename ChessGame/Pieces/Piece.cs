using ChessGame.Common;
using ChessGame.Board;

namespace ChessGame.Pieces;

abstract class Piece(Color color)
{
    public PieceType Type {get;}
    public Color Color { get; } = color;
    public abstract char Symbol {get;} 

    public abstract Square[] GetMoves();
}