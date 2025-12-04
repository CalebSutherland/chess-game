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

    if (!MoveValidator.IsLegalMove(move, board))
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
    
    // Build the SAN string
    string start = SquareParser.Serialize(move.Start);
    sanBuilder.Capture = board.GetPiece(move.End) != null;

    if (piece.Type == PieceType.Pawn && sanBuilder.Capture)
    {
      sanBuilder.Piece = start[0].ToString();
    }
    else if (piece.Type != PieceType.Pawn)
    {
      sanBuilder.Piece = piece.Symbol.ToString().ToUpper();
      Square? secondAttacker = MoveValidator.GetMultipleAttackers(move, piece.Type, board);
      if (secondAttacker != null)
      {
        if (move.Start.Col == secondAttacker.Col)
        {
          sanBuilder.TwoAttackers = start[1].ToString();
        }
        else
        {
          sanBuilder.TwoAttackers = start[0].ToString();
        }
      }
    }
    sanBuilder.Square = SquareParser.Serialize(move.End);

    CastlingUpdater.UpdateCastlingRights(move, board);
    board.MovePiece(move);
    return true;
  }
}