using ChessGame.Types;
using ChessGame.Pieces;
using ChessGame.Notation;

namespace ChessGame.Board;

public class ChessBoard
{
  public int Size { get; } = 8;
  public Piece?[,] Grid;
  public Color Turn { get; set; }
  public CastlingRights Castling { get; }
  public Square? EnPassant { get; set; }
  public int Halfmove { get; }
  public int Fullmove { get; }

  public ChessBoard(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
  {
    string[] fenParts = fen.Split(" ");
    Grid = BoardParser.Deserialize(fenParts[0]);
    Turn = fenParts[1] == "b" ? Color.Black : Color.White;
    Castling = CastlingParser.Deserialize(fenParts[2]);
    EnPassant = fenParts[3] == "-" ? null : SquareParser.Deserialize(fenParts[3]);
    Halfmove = int.Parse(fenParts[4]);
    Fullmove = int.Parse(fenParts[5]);
  }

  public bool IsValidSquare(Square square)
  {
    return square.Row >= 0 && square.Row < Size && square.Col >= 0 && square.Col < Size;
  }

  public bool IsValidMove(Move move)
  {
    return IsValidSquare(move.Start) && IsValidSquare(move.End);
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

  public void MovePiece(Move move)
  {
    if (IsValidMove(move))
    {
      Piece? piece = GetPiece(move.Start);
      if (piece != null)
      {
        SetPiece(move.End, piece);
        SetPiece(move.Start, null);
      }
    }
  }

  public ChessBoard Copy()
  {
    return new ChessBoard(Serialize());
  }

  public string Serialize()
{
    string board = BoardParser.Serialize(Grid);
    string turn = Turn == Color.White ? "w" : "b";
    string castling = CastlingParser.Serialize(Castling);
    string enPassant = EnPassant == null ? "-" : SquareParser.Serialize(EnPassant);
    string half = Halfmove.ToString();
    string full = Fullmove.ToString();

    return $"{board} {turn} {castling} {enPassant} {half} {full}";
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
    Console.Write("\n");
  }
}
