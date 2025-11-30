using ChessGame.Board;

namespace ChessGame.Parsers;

public static class CastlingParser
{
    public static CastlingRights Deserialize(string rights)
  {
    CastlingRights castling = new();
    foreach(char c in rights)
    {
      switch (c)
      {
        case 'K': castling.KingsideW = true; break;
        case 'Q': castling.QueensideW = true; break;
        case 'k': castling.KingsideB = true; break;
        case 'q': castling.QueensideB = true; break;
      }
    }
    return castling;
  }

  public static string Serialize(CastlingRights castling)
  {
    string rights = "";
    
    if (castling.KingsideW == true) rights += "K";
    if (castling.QueensideW == true) rights += "Q";
    if (castling.KingsideB == true) rights += "k";
    if (castling.QueensideB == true) rights += "q";

    if (rights == "")
    {
      return "-";
    }

    return rights;
  }
}