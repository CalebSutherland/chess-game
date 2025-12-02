using ChessGame.Pieces;
using ChessGame.Types;

namespace ChessGame.Board;

public static class CastlingUpdater
{
  public static void UpdateCastlingRights(Move move, ChessBoard board)
  {
    Piece? piece = board.GetPiece(move.Start);
    Piece? captured = board.GetPiece(move.End);

    if (piece == null)
    {
      return;
    }

    // Handle king moving
    if (piece.Type == PieceType.King)
    {
      if (piece.Color == Color.White)
      {
        board.Castling.KingsideW = false;
        board.Castling.QueensideW = false;
      } 
      else
      {
        board.Castling.KingsideB = false;
        board.Castling.QueensideB = false;
      }
    }

    // Handle rook moving
    if (piece.Type == PieceType.Rook)
    {
      bool isQueenside = move.Start.Col == 0;
      bool isKingside = move.Start.Col == 7;
      if (piece.Color == Color.White)
      {
        if (isQueenside) board.Castling.QueensideW = false;
        if (isKingside) board.Castling.KingsideW = false;
      }
      else 
      {
        if (isQueenside) board.Castling.QueensideB = false;
        if (isKingside) board.Castling.KingsideB = false;
      }
    }

    // Handle rook being captured
    if (captured != null && captured.Type == PieceType.Rook)
    {
      bool isQueenside = move.End.Col == 0;
      bool isKingside = move.End.Col == 7;

      if (captured.Color == Color.White)
      {
        if (isQueenside) board.Castling.QueensideW = false;
        if (isKingside) board.Castling.KingsideW = false;
      } else
      {
        if (isQueenside) board.Castling.QueensideB = false;
        if (isKingside) board.Castling.KingsideB = false;
      }
    }
  }
}