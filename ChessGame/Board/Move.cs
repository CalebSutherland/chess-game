namespace ChessGame.Board;

class Move(Square start, Square end)
{
  public Square Start {get;} = start;
  public Square End {get;} = end;
}