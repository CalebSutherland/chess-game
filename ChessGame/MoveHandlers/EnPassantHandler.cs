using ChessGame.Board;
using ChessGame.Pieces;
using ChessGame.Types;

namespace ChessGame.MoveHandlers;

public class EnPassantHandler : MoveHandler
{
  public override bool HandleMove(Move move, ChessBoard board)
  {
    Piece? piece = board.GetPiece(move.Start);
    if (piece == null)
    {
      Console.WriteLine("Illegal move - invalid piece");
      return false;
    }

    if (board.EnPassant != null && piece.Type == PieceType.Pawn)
    {
      if (move.Start.Row == board.EnPassant.Row && Math.Abs(move.Start.Col - board.EnPassant.Col) == 1)
      {
        int enPassantRow = piece.Color == Color.White ? board.EnPassant.Row - 1 : board.EnPassant.Row + 1;
        Square enPassantCapture =  new(enPassantRow, board.EnPassant.Col);

        MoveValidator validator = new(board);
        if (!validator.IsLegalMove(move))
        {
          return false;
        }

        if (move.End == enPassantCapture)
        {
          // Should not have to update castling rights after en passant
          board.MovePiece(move);
          board.SetPiece(board.EnPassant, null);
          board.EnPassant = null;
          return true;
        }
      }
    }
    return base.HandleMove(move, board);
  }
}