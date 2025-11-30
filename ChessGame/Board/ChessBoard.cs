using ChessGame.Types;
using ChessGame.Pieces;
using ChessGame.Parsers;

namespace ChessGame.Board;

public class ChessBoard
{
  public int Size { get; } = 8;
  public string Fen { get; set; }
  public Piece?[,] Grid;
  public Color Turn { get; }
  public CastlingRights Castling { get; }
  public Square? EnPassant { get; }
  public int Halfmove { get; }
  public int Fullmove { get; }

  public ChessBoard(string fen = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1")
  {
    string[] fenParts = fen.Split(" ");
    Fen = fen;
    Grid = BoardParser.Deserialize(fenParts[0]);
    Turn = fenParts[1] == "b" ? Color.Black : Color.White;
    Castling = CastlingParser.Deserialize(fenParts[2]);
    EnPassant = SquareParser.Deserialize(fenParts[3]);
    Halfmove = int.Parse(fenParts[4]);
    Fullmove = int.Parse(fenParts[5]);
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

  public void SetPiece(Square square, Piece? piece)
  {
    if (IsValidSquare(square))
    {
      Grid[square.Row, square.Col] = piece;
    }
  }

  public ChessBoard Copy()
  {
    return new ChessBoard(Fen);
  }

  public void DisplayBoard()
  {
    Console.WriteLine("  | a b c d e f g h |");
    Console.WriteLine("-----------------------");
    for (int i = 0; i < Size; i++)
    {
      Console.Write(8 - i);
      Console.Write(" | ");
      for (int j = 0; j < Size; j++)
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
