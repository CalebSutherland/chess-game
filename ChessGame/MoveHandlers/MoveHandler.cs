using ChessGame.Board;

namespace ChessGame.MoveHandlers;

public interface IMoveHandler
{
  IMoveHandler SetNext(IMoveHandler next);
  bool HandleMove(Move move, ChessBoard board);
}

public abstract class MoveHandler : IMoveHandler
{
  private IMoveHandler? _next;

  public IMoveHandler SetNext(IMoveHandler next)
  {
    _next = next;
    return next;
  }

  public virtual bool HandleMove(Move move, ChessBoard board)
  {
    if (_next != null)
      return _next.HandleMove(move, board);

    return false;
  }
}