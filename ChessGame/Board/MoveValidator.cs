using ChessGame.Pieces;
using ChessGame.Types;

namespace ChessGame.Board;

public static class MoveValidator
{
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

  public static bool IsLegalMove(Move move, ChessBoard board)
  {
    if (!board.IsValidMove(move)) return false;

    ChessBoard copy = board.Copy();
    Piece? piece = copy.GetPiece(move.Start);

    if (piece == null || piece.Color != copy.Turn) return false;
    if (!piece.GetMoves(move.Start, copy).Contains(move.End)) return false;

    copy.MovePiece(move);
    if (IsKingInCheck(copy.Turn, copy)) return false;

    return true;
  }

  public static List<Move> GetAllLegalMoves(ChessBoard board)
  {
    List<Move> legalMoves = [];

    for (int row = 0; row < board.Size; row++)
    {
      for (int col = 0; col < board.Size; col++)
      {
        Piece? piece = board.GetPiece(new Square(row, col));
        if (piece != null && piece.Color == board.Turn)
        {
          Square start = new(row, col);
          List<Square> current = piece.GetMoves(start, board);
          foreach (Square end in current)
          {
            Move move = new(start, end);
            if (IsLegalMove(move, board))
            {
              legalMoves.Add(move);
            }
          }
        }
      }
    }
    return legalMoves;
  }

  // Get multiple attackers of same piece type - used building SAN string
  public static Square? GetMultipleAttackers(Move move, PieceType pieceType, ChessBoard board)
  {
    for (int row = 0; row < board.Size; row++)
    {
      for (int col = 0; col < board.Size; col++)
      {
        Square square = new(row, col);
        Piece? piece = board.GetPiece(square);
        if (piece != null && piece.Type == pieceType && square != move.Start)
        {
          List<Square> current = piece.GetMoves(square, board);
          if (current.Contains(move.End))
          {
            return square;
          }
        }
      }
    }
    return null;
  }
}