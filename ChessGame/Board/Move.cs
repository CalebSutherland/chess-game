namespace ChessGame.Board;

public class Move(Square start, Square end)
{
  public Square Start { get; } = start;
  public Square End { get; } = end;
}