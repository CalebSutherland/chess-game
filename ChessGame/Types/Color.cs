namespace ChessGame.Types;

public enum Color
{
  White,
  Black,
}

public static class ColorExtensions
{
  public static Color Opposite(this Color color)
  {
    return color == Color.White ? Color.Black : Color.White;
  }
}