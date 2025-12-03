using ChessGame.Board;
using ChessGame.Notation;
using ChessGame.Pieces;
using ChessGame.Types;

namespace ChessGame.MoveHandlers;

class NormalMoveHandler : MoveHandler
{
  public override bool HandleMove(Move move, ChessBoard board, SANBuilder sanBuilder)
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

    // Handle new en passant target
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
    
    sanBuilder.Capture = board.GetPiece(move.End) != null;
    if (piece.Type == PieceType.Pawn && sanBuilder.Capture)
    {
      string start = SquareParser.Serialize(move.Start);
      sanBuilder.Piece = start[0].ToString();
    }
    else if (piece.Type != PieceType.Pawn)
    {
      sanBuilder.Piece = piece.Symbol.ToString().ToUpper();
    }
    sanBuilder.Square = SquareParser.Serialize(move.End);

    CastlingUpdater.UpdateCastlingRights(move, board);
    board.MovePiece(move);
    return true;
  }
}