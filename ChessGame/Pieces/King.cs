using ChessGame.Types;
using ChessGame.Board;

namespace ChessGame.Pieces;

class King(Color color) : Piece(color)
{
  public override char Symbol { get; } = 'k';
  public override PieceType Type { get; } = PieceType.King;

  public override List<Square> GetMoves(Square square, ChessBoard board)
  {
    List<Square> moves = [];
    List<int[]> offsets = [
      [1, 0], [0, 1], [-1, 0], [0, -1], 
      [1, 1], [-1, -1], [1, -1], [-1, 1]
    ];

    for (int i = 0; i < offsets.Count; i++)
    {
      int row = square.Row + offsets[i][0];
      int col = square.Col + offsets[i][1];

      Square move = new(row, col);

      if (board.IsValidSquare(move))
      {
        Piece? piece = board.Grid[row, col];
        if (piece == null || piece.Color != Color)
        {
          moves.Add(move);
        }
      }
    }
    return moves;
  }
}