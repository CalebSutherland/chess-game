using ChessGame.Pieces;
using ChessGame.Types;

namespace ChessGame.Board;

public class MoveValidator(ChessBoard board)
{
  private readonly ChessBoard _board = board;
  
  private static Square FindKing(Color color, ChessBoard board)
  {
    for (int row = 0; row < board.Size; row++)
    {
      for (int col = 0; col < board.Size; col++)
      {
        Piece? piece = board.GetPiece(new Square(row, col));
        if (piece != null && piece.Symbol == 'k' && piece.Color == color)
        {
          return new Square(row, col);
        }
      }
    }
    throw new ArgumentException($"No {color} king found", nameof(color));
  }

  public static List<Square> GetAllSquaresUnderAttack(Color color , ChessBoard board)
  {
    List<Square> moves = [];
    for (int row = 0; row < board.Size; row++)
    {
      for (int col = 0; col < board.Size; col++)
      {
        Piece? piece = board.GetPiece(new Square(row, col));
        if (piece != null && piece.Color == color)
        {
          List<Square> current = piece.GetMoves(new Square(row, col), board);
          moves.AddRange(current);
        }
      }
    }
    return moves;
  }

  public static bool IsKingInCheck(Color color, ChessBoard board)
  {
    Square king = FindKing(color, board);
    Color attacker = color.Opposite();

    List<Square> underAttack = GetAllSquaresUnderAttack(attacker, board);

    if (underAttack.Contains(king))
    {
      return true;
    }
    return false;
  }

  public bool IsLegalMove(Move move)
  {
    if (!_board.IsValidMove(move)) return false;

    ChessBoard copy = _board.Copy();
    Piece? piece = copy.GetPiece(move.Start);

    if (piece == null || piece.Color != copy.Turn) return false;
    if (!piece.GetMoves(move.Start, copy).Contains(move.End)) return false;

    copy.MovePiece(move);
    if (IsKingInCheck(copy.Turn, copy)) return false;

    return true;
  }
}