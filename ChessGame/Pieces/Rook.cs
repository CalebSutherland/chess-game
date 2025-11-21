using ChessGame.Common;
using ChessGame.Board;

namespace ChessGame.Pieces;

class Rook(Color color) : Piece(color)
{
    public override char Symbol { get; } = 'r';

	public override Square[] GetMoves()
	{
		return [];
	}

}