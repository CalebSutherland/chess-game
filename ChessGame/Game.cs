using ChessGame.Board;
using ChessGame.MoveHandlers;
using ChessGame.Notation;

namespace ChessGame;

public class Game
{
  public ChessBoard Board { get; }
  private readonly IMoveHandler Handler = new CastlingHandler();

  public Game(ChessBoard board)
  {
    Board = board;
    Handler.SetNext(new NormalMoveHandler());
  }

  public bool MakeMove(Move move)
  {
    if (Handler.HandleMove(move, Board))
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  public bool MakeMove(string moveString)
  {
    string start = moveString[..2];
    string end = moveString.Substring(2, 2);

    Square startSquare = SquareParser.Deserialize(start);
    Square endSquare = SquareParser.Deserialize(end);

    Move move = new(startSquare, endSquare);
    return MakeMove(move);
  }
}