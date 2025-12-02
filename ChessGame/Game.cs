using ChessGame.Board;
using ChessGame.MoveHandlers;

namespace ChessGame;

public class Game(ChessBoard board)
{
  public ChessBoard Board { get; } = board;
  private readonly IMoveHandler MoveHandler = new NormalMove();

  public bool MakeMove(Move move)
  {
    if (MoveHandler.HandleMove(move, Board))
    {
      return true;
    }
    else
    {
      return false;
    }
  }
}