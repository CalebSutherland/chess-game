using ChessGame.Board;
using ChessGame.Pieces;
using ChessGame.Types;
namespace ChessGame.MoveHandlers;

public class CastlingHandler : MoveHandler
{
  public override bool HandleMove(Move move, ChessBoard board)
  {
    Piece? piece = board.GetPiece(move.Start);
    if (piece == null) return base.HandleMove(move, board);

    int startRow = piece.Color == Color.White ? 7 : 0;
    bool isKingSide = move == new Move(new Square(startRow, 4), new Square(startRow, 6));
    bool isQueenSide = move == new Move(new Square(startRow, 4), new Square(startRow, 2));

    if (piece.Type == PieceType.King && (isKingSide || isQueenSide))
    {
      // Cant castle while in check
      if (MoveValidator.IsKingInCheck(piece.Color, board)) return false;

      if (piece.Color == Color.White)
      {
        if (isQueenSide && board.Castling.QueensideW)
        {
          if (!CanCastleQueenside(board, startRow))
            return false;
          
          PerformCastle(board, move, new Move(new Square(startRow, 0), new Square(startRow, 3)));
          return true;
        }
        if (isKingSide && board.Castling.KingsideW)
        {
          if (!CanCastleKingside(board, startRow))
            return false;
          
          PerformCastle(board, move, new Move(new Square(startRow, 7), new Square(startRow, 5)));
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
          return true;
        }
        if (isKingSide && board.Castling.KingsideB)
        {
          if (!CanCastleKingside(board, startRow))
            return false;
          
          PerformCastle(board, move, new Move(new Square(startRow, 7), new Square(startRow, 5)));
          return true;
        }
      }
    }
    return base.HandleMove(move, board);
  }

  private static bool CanCastleKingside(ChessBoard board, int row)
  {
    if (board.GetPiece(new Square(row, 5)) != null || 
      board.GetPiece(new Square(row, 6)) != null)
    {
      return false;
    }

    Color kingColor = board.Turn;
    Color enemyColor = kingColor.Opposite();

     List<Square> attackedSquares = MoveValidator.GetAllMoves(enemyColor, board);

     if (attackedSquares.Contains(new Square(row, 5)) || 
        attackedSquares.Contains(new Square(row, 6)))
    {
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
      return false;
    }

    Color kingColor = board.Turn;
    Color enemyColor = kingColor.Opposite();
    List<Square> attackedSquares = MoveValidator.GetAllMoves(enemyColor, board);

    if (attackedSquares.Contains(new Square(row, 2)) || 
        attackedSquares.Contains(new Square(row, 3)))
    {
      return false;
    }

    return true;
  }

  private static void PerformCastle(ChessBoard board, Move kingMove, Move rookMove)
  {   
    board.EnPassant = null;
    CastlingUpdater.UpdateCastlingRights(kingMove, board);
    board.Turn = board.Turn.Opposite();
    board.MovePiece(kingMove);
    board.MovePiece(rookMove); 
  }
}