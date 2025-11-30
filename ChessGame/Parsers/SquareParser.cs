using ChessGame.Board;

namespace ChessGame.Parsers;

public static class SquareParser
{
  public static Square? Deserialize(string san)
  {
    if (san == "-") return null;

    if (san.Length != 2)
    {
      throw new ArgumentException("Invalid square notation.", nameof(san));
    }

    char file = san[0];
    char rank = san[1];

    int col = file - 'a';
    int row = 7 - (rank - '1');

    return new Square(row, col);
    
  }

  public static string Serialize(Square square)
  {
    char file = (char)('a' + square.Col);
    char rank = (char)('1' + (7 - square.Row));
    return $"{file}{rank}"; 
  }
}
