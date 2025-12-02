using ChessGame.Board;
using ChessGame.Pieces;
using ChessGame.Types;

namespace ChessGame.MoveHandlers;

class NormalMoveHandler : MoveHandler
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

    int startingRow = piece.Color == Color.White ? 6 : 1;
    if (piece.Type == PieceType.Pawn && move.Start.Row == startingRow 
      && Math.Abs(move.End.Row - move.Start.Row) > 1)
    {
      board.EnPassant = move.End;
    }
    else
    {
      board.EnPassant = null;
    }
    
    CastlingUpdater.UpdateCastlingRights(move, board);
    board.MovePiece(move);
    return true;
  }
}