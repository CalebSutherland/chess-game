using ChessGame.Pieces;
using ChessGame.Types;

namespace ChessGame.Board;

public class MoveValidator(ChessBoard board)
{
  private readonly ChessBoard _board = board;
  
  private static Square FindKing(Color color, ChessBoard board)
  {
    Piece?[,] grid = board.Grid;

    for (int row = 0; row < grid.GetLength(0); row++)
    {
      for (int col = 0; col < grid.GetLength(1); col++)
      {
        Piece? piece = grid[row, col];
        if (piece != null && piece.Symbol == 'k' && piece.Color == color)
        {
          return new Square(row, col);
        }
      }
    }
    throw new ArgumentException($"No {color} king found", nameof(color));
  }

  public static List<Square> GetAllMoves(Color color , ChessBoard board)
  {
    Piece?[,] grid = board.Grid;
    List<Square> moves = [];
    for (int row = 0; row < grid.GetLength(0); row++)
    {
      for (int col = 0; col < grid.GetLength(1); col++)
      {
        Piece? piece = grid[row, col];
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

    List<Square> underAttack = GetAllMoves(attacker, board);

    if (underAttack.Contains(king))
    {
      return true;
    }
    return false;
  }

  public bool IsLegalMove(Move move)
  {
    if (!_board.IsValidMove(move))
    {
      Console.WriteLine("Illegal move - Move out of board range");
      return false;
    } 

    ChessBoard copy = _board.Copy();
    Piece? piece = copy.GetPiece(move.Start);

    if (piece == null || piece.Color != copy.Turn)
    {
      Console.WriteLine("Illegal move - Invalid peice");
      return false;
    }

    if (!piece.GetMoves(move.Start, copy).Contains(move.End))
    {
      Console.WriteLine("Illegal move - Invalid move for peice");
      return false;
    }

    copy.MovePiece(move);
    if (IsKingInCheck(copy.Turn, copy))
    {
      Console.WriteLine("Illegal move - Move would leave king in check");
      return false;
    }

    return true;
  }
}