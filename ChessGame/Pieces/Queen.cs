using ChessGame.Types;
using ChessGame.Board;

namespace ChessGame.Pieces;

public class Queen(Color color) : Piece(color)
{
  public override char Symbol { get; } = 'q';
  public override PieceType Type { get; } = PieceType.Queen;

  public override List<Square> GetMoves(Square square, ChessBoard board)
  {
    List<int[]> directions = [[-1, 0], [1, 0], [0, -1], [0, 1], [1, 1], [-1, -1], [1, -1], [-1, 1]];
		return GetSlidingMoves(square, board, directions);
  }
}