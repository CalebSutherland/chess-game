using ChessGame.Board;
using ChessGame.Notation;

namespace ChessGame.MoveHandlers;

public interface IMoveHandler
{
  IMoveHandler SetNext(IMoveHandler next);
  bool HandleMove(Move move, ChessBoard board, SANBuilder sanBuilder);
}

public abstract class MoveHandler : IMoveHandler
{
  private IMoveHandler? _next;

  public IMoveHandler SetNext(IMoveHandler next)
  {
    _next = next;
    return next;
  }

  public virtual bool HandleMove(Move move, ChessBoard board, SANBuilder sanBuilder)
  {
    if (_next != null)
      return _next.HandleMove(move, board, sanBuilder);

    return false;
  }
}