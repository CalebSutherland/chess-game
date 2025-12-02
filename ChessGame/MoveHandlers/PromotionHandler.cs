using ChessGame.Board;
using ChessGame.Pieces;
using ChessGame.Types;

namespace ChessGame.MoveHandlers;

public class PromotionHandler : MoveHandler
{
  public override bool HandleMove(Move move, ChessBoard board)
  {
    Piece? piece = board.GetPiece(move.Start);

    if (piece == null)
    {
      Console.WriteLine("Illegal move - Invalid piece");
      return false;
    }

    MoveValidator validator = new(board);
    if (!validator.IsLegalMove(move))
    {
      return false;
    }

    if (piece.Type == PieceType.Pawn)
    {
      int promotionRow = piece.Color == Color.White ? 0 : 7;
      int direction = piece.Color == Color.White ? -1 : 1;

      Move promotionMove = new(new Square(promotionRow - direction, move.Start.Col), new Square(promotionRow, move.End.Col), move.Promotion);
      if (move == promotionMove)
      {
        // Default to queen if promotion piece is null
        Piece? promotionPiece;
        if (move.Promotion == null)
        {
          promotionPiece = PieceFactory.CreatePiece('q', piece.Color);
        }
        else
        {
          promotionPiece = move.Promotion;
        }

        CastlingUpdater.UpdateCastlingRights(move, board);
        board.EnPassant = null;
        board.MovePiece(move);
        board.SetPiece(move.End, promotionPiece);
        return true;
      }
    }
    
    return base.HandleMove(move, board);
  }
}