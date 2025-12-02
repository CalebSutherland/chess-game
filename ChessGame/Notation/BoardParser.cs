using ChessGame.Pieces;
using ChessGame.Types;

namespace ChessGame.Notation;

public static class BoardParser
{
  public static Piece?[,] Deserialize(string fen)
  {
    Piece?[,] grid = new Piece?[8, 8];
    string[] parts = fen.Split("/");

    for (int row = 0; row < grid.GetLength(0); row++)
    {
      int col = 0;

      foreach (char c in parts[row])
      {
        if (char.IsDigit(c))
        {
          col += (int)char.GetNumericValue(c);
        }
        else
        {
          Color color = char.IsUpper(c) ? Color.White : Color.Black;
          char symbol = char.ToLower(c);

          Piece? piece = PieceFactory.CreatePiece(symbol, color);
          grid[row, col] = piece;
          col += 1;
        }
      }
    }
    return grid;
  }

  public static string Serialize(Piece?[,] grid)
  {
    List<string> fen = [];

    for (int row = 0; row < grid.GetLength(0); row++)
    {
      int empty = 0;
      string part = "";
      for (int col = 0; col < grid.GetLength(1); col++)
      {
        Piece? piece = grid[row, col];
        if (piece != null)
        {
          if (empty > 0)
          {
            part += empty.ToString();
          }
          char symbol = piece.Color == Color.White ? char.ToUpper(piece.Symbol) : piece.Symbol;
          part += symbol;
          empty = 0;
        }
        else
        {
          empty++;
        }
      }

      if (empty > 0)
      {
        part += empty.ToString();
      }
      fen.Add(part);
    }
    string result = string.Join("/", fen);
    return result;
  }
}