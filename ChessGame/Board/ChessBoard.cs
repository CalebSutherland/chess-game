using ChessGame.Types;
using ChessGame.Pieces;

namespace ChessGame.Board;

class ChessBoard
{
  public int Size { get; } = 8;
  public Piece?[,] Grid { get; }

  public ChessBoard()
  {
    Piece?[,] grid = new Piece?[8, 8];
    for (int i = 0; i < Size; i++)
    {
      grid[0, i] = new Rook(Color.Black);
      grid[7, i] = new Rook(Color.White);
    }
      grid[1, 0] = new Queen(Color.Black);
    Grid = grid;
  }

  public bool IsValidSquare(Square square)
  {
    if (square.Row < 0 || square.Row >= Size || square.Col < 0 || square.Col >= Size)
    {
      return false;
    }
    return true;
  }

  public Piece? GetPiece(Square square)
  {
    if (IsValidSquare(square))
    {
      return Grid[square.Row, square.Col];
    }
    return null;
  }

  public void SetPiece(Square square, Piece piece)
  {
    if (IsValidSquare(square))
    {
      Grid[square.Row, square.Col] = piece;
    }
  }

  public void DisplayBoard()
  {
    Console.WriteLine("  | a b c d e f g h |");
    Console.WriteLine("-----------------------");
    for (int i = 0; i < 8; i++)
    {
      Console.Write(8 - i);
      Console.Write(" | ");
      for (int j = 0; j < 8; j++)
      {
        Piece? piece = Grid[i, j];
        if (piece == null)
        {
          Console.Write(". ");
          continue;
        }
        char symbol = piece.Symbol;
        if (piece.Color == Color.White)
        {
          Console.Write(char.ToUpper(symbol) + " ");
        }
        else
        {
          Console.Write(symbol + " ");
        }
      }
      Console.Write("| ");
      Console.Write(8 - i);
      Console.Write("\n");
    }
    Console.WriteLine("-----------------------");
    Console.WriteLine("  | a b c d e f g h |");
  }
}
