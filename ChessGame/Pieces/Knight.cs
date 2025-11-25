using ChessGame.Types;
using ChessGame.Board;

namespace ChessGame.Pieces;

class Knight(Color color) : Piece(color)
{
  public override char Symbol { get; } = 'n';
  public override PieceType Type { get; } = PieceType.Knight;

  public override List<Square> GetMoves(Square square, ChessBoard board)
  {
    List<Square> moves = [];
    List<int[]> offsets = [
      [-2, -1], [-2, 1], [-1, -2], [-1, 2], 
      [1, -2], [1, 2], [2, -1], [2, 1]
    ];

    for (int i = 0; i < offsets.Count; i++)
    {
      int row = square.Row + offsets[i][0];
      int col = square.Col + offsets[i][1];

      Square move = new(row, col);

      if (board.IsValidSquare(move))
      {
        Piece? piece = board.GetPiece(move);
        if (piece == null || piece.Color != Color)
        {
          moves.Add(move);
        }
      }
    }
    return moves;
  }
}