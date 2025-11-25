using ChessGame.Types;
using ChessGame.Board;

namespace ChessGame.Pieces;

class Rook(Color color) : Piece(color)
{
  public override char Symbol { get; } = 'r';
  public override PieceType Type { get; } = PieceType.Rook;

  public override List<Square> GetMoves(Square square, ChessBoard board)
  {
    List<int[]> directions = [[-1, 0], [1, 0], [0, -1], [0, 1]];
		return GetSlidingMoves(square, board, directions);
  }
}