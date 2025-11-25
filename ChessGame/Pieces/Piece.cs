using ChessGame.Types;
using ChessGame.Board;

namespace ChessGame.Pieces;

abstract class Piece(Color color)
{
  public Color Color { get; } = color;
  public abstract PieceType Type { get; }
  public abstract char Symbol { get; }

  public abstract List<Square> GetMoves(Square square, ChessBoard board);

  protected List<Square> GetSlidingMoves(Square square, ChessBoard board, List<int[]> directions)
  {
    List<Square> moves = [];
    for (int i = 0; i < directions.Count; i++)
    {
      int row = square.Row;
      int col = square.Col;

      int dRow = directions[i][0];
      int dCol = directions[i][1];

      row += dRow;
      col += dCol;

      while (0 <= row && row < board.Size && 0 <= col && col < board.Size)
      {
        Square move = new(row, col);
        Piece? piece = board.GetPiece(move);

        if (piece == null)
        {
          moves.Add(move);
        }
        else if (piece.Color != Color) 
        {
          moves.Add(move);
          break;
        }

        row += dRow;
        col += dCol;
      }
    }
      return moves;
  }
}