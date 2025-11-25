using ChessGame.Types;
using ChessGame.Board;

namespace ChessGame.Pieces;

class Pawn(Color color) : Piece(color)
{
  public override char Symbol { get; } = 'p';
  public override PieceType Type { get; } = PieceType.Pawn;

  public override List<Square> GetMoves(Square square, ChessBoard board)
  {
    List<Square> moves = [];

    int direction = Color == Color.White ? -1 : 1;
    int startRow = Color == Color.White ? 6 : 1;
    int nextRow = startRow + direction;

    Square oneSquare = new(nextRow, square.Col);
    if (board.IsValidSquare(oneSquare) && board.GetPiece(oneSquare) == null)
    {
      moves.Add(oneSquare);

      if (square.Row == startRow)
      {
        Square twoSquare = new(nextRow + direction, square.Col);
        if (board.IsValidSquare(twoSquare) && board.GetPiece(twoSquare) == null)
        {
          moves.Add(twoSquare);
        }
      }
    }

    int[] offsets = [1, -1];
    foreach (int offset in offsets)
    {
      Square diagnol = new(square.Row, square.Col + offset);
      if (board.IsValidSquare(diagnol))
      {
        Piece? piece = board.GetPiece(diagnol);
        if(piece != null && piece.Color != Color)
        {
          moves.Add(diagnol);
        }
      }
    }
    return moves;
  }
}