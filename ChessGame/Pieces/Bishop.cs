using ChessGame.Types;
using ChessGame.Board;

namespace ChessGame.Pieces;

public class Bishop(Color color) : Piece(color)
{
  public override char Symbol { get; } = 'b';
  public override PieceType Type { get; } = PieceType.Bishop;

  public override List<Square> GetMoves(Square square, ChessBoard board)
  {
    List<int[]> directions = [[-1, -1], [1, 1], [-1, 1], [1, -1]];
		return GetSlidingMoves(square, board, directions);
  }
}