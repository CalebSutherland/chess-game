using ChessGame.Board;
using ChessGame.Notation;
using ChessGame.Pieces;
using ChessGame.Types;
namespace ChessGame.MoveHandlers;

public class CastlingHandler : MoveHandler
{
  public override bool HandleMove(Move move, ChessBoard board, SANBuilder sanBuilder)
  {
    Piece? piece = board.GetPiece(move.Start);
    if (piece == null)
    {
      Console.WriteLine("Illegal move - Invalid piece");
      return false;
    }

    int startRow = piece.Color == Color.White ? 7 : 0;
    bool isKingSide = move == new Move(new Square(startRow, 4), new Square(startRow, 6));
    bool isQueenSide = move == new Move(new Square(startRow, 4), new Square(startRow, 2));

    if (piece.Type == PieceType.King && (isKingSide || isQueenSide))
    {
      // Cant castle while in check
      if (MoveValidator.IsKingInCheck(piece.Color, board))
      {
        Console.WriteLine("Illegal castle - Can't castle while king is in check");
        return false;
      } 

      if (piece.Color == Color.White)
      {
        if (isQueenSide && board.Castling.QueensideW)
        {
          if (!CanCastleQueenside(board, startRow))
            return false;
          
          PerformCastle(board, move, new Move(new Square(startRow, 0), new Square(startRow, 3)));
          sanBuilder.Queenside = true;
          return true;
        }
        if (isKingSide && board.Castling.KingsideW)
        {
          if (!CanCastleKingside(board, startRow))
            return false;
          
          PerformCastle(board, move, new Move(new Square(startRow, 7), new Square(startRow, 5)));
          sanBuilder.Kingside = true;
          return true;
        }
      }
      else
      {
        if (isQueenSide && board.Castling.QueensideB)
        {
          if (!CanCastleQueenside(board, startRow))
            return false;
          
          PerformCastle(board, move, new Move(new Square(startRow, 0), new Square(startRow, 3)));
          sanBuilder.Queenside = true;
          return true;
        }
        if (isKingSide && board.Castling.KingsideB)
        {
          if (!CanCastleKingside(board, startRow))
            return false;
          
          PerformCastle(board, move, new Move(new Square(startRow, 7), new Square(startRow, 5)));
          sanBuilder.Kingside = true;
          return true;
        }
      }
    }
    return base.HandleMove(move, board, sanBuilder);
  }

  private static bool CanCastleKingside(ChessBoard board, int row)
  {
    if (board.GetPiece(new Square(row, 5)) != null || 
      board.GetPiece(new Square(row, 6)) != null)
    {
      Console.WriteLine("Illegal castle - Spaces between king and rook must be empty");
      return false;
    }

    Color kingColor = board.Turn;
    Color enemyColor = kingColor.Opposite();

     List<Square> attackedSquares = MoveValidator.GetAllSquaresUnderAttack(enemyColor, board);

     if (attackedSquares.Contains(new Square(row, 5)) || 
        attackedSquares.Contains(new Square(row, 6)))
    {
      Console.WriteLine("Illegal castle - King can't castle through attacking peice");
      return false;
    }

    return true;
  }

  private static bool CanCastleQueenside(ChessBoard board, int row)
  {
    if (board.GetPiece(new Square(row, 1)) != null || 
        board.GetPiece(new Square(row, 2)) != null || 
        board.GetPiece(new Square(row, 3)) != null)
    {
      Console.WriteLine("Illegal castle - Spaces between king and rook must be empty");
      return false;
    }

    Color kingColor = board.Turn;
    Color enemyColor = kingColor.Opposite();
    List<Square> attackedSquares = MoveValidator.GetAllSquaresUnderAttack(enemyColor, board);

    if (attackedSquares.Contains(new Square(row, 2)) || 
        attackedSquares.Contains(new Square(row, 3)))
    {
      Console.WriteLine("Illegal castle - King can't castle through attacking peice");
      return false;
    }

    return true;
  }

  private static void PerformCastle(ChessBoard board, Move kingMove, Move rookMove)
  {   
    CastlingUpdater.UpdateCastlingRights(kingMove, board);
    board.EnPassant = null;
    board.MovePiece(kingMove);
    board.MovePiece(rookMove); 
  }
}