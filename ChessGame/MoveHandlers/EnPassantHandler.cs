using ChessGame.Board;
using ChessGame.Notation;
using ChessGame.Pieces;
using ChessGame.Types;

namespace ChessGame.MoveHandlers;

public class EnPassantHandler : MoveHandler
{
  public override bool HandleMove(Move move, ChessBoard board, SANBuilder sanBuilder)
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

        if (!MoveValidator.IsLegalMove(move, board))
        {
          return false;
        }

        if (move.End == enPassantCapture)
        {
          string start = SquareParser.Serialize(move.Start);
          sanBuilder.Piece = start[0].ToString();
          sanBuilder.Capture = true;
          sanBuilder.Square = SquareParser.Serialize(move.End);

          // Should not have to update castling rights after en passant
          board.MovePiece(move);
          board.SetPiece(board.EnPassant, null);
          board.EnPassant = null;
          return true;
        }
      }
    }
    return base.HandleMove(move, board, sanBuilder);
  }
}