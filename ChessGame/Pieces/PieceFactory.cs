using ChessGame.Types;

namespace ChessGame.Pieces;

public static class PieceFactory
{
  public static Piece? CreatePiece(char piece, Color color)
  {
    if (piece == 'p')
    {
      return new Pawn(color);
    }
    else if (piece == 'r')
    {
      return new Rook(color);
    }
    else if (piece == 'n')
    {
      return new Knight(color);
    }
    else if (piece == 'b')
    {
      return new Bishop(color);
    }
    else if (piece == 'q')
    {
      return new Queen(color);
    }
    else if (piece == 'k')
    {
      return new King(color);
    }
    else
    {
      return null;
    }
  }
}