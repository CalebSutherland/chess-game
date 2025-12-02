using ChessGame.Board;
using ChessGame.MoveHandlers;
using ChessGame.Notation;
using ChessGame.Pieces;
using ChessGame.Types;

namespace ChessGame;

public class Game
{
  public ChessBoard Board { get; }
  private readonly IMoveHandler _handler;

  public Game(ChessBoard board)
  {
    Board = board;
    _handler = new CastlingHandler();
    _handler.SetNext(new EnPassantHandler())
      .SetNext(new PromotionHandler())
      .SetNext(new NormalMoveHandler());
  }

  public bool MakeMove(Move move)
  {
    if (_handler.HandleMove(move, Board))
    {
      Board.Turn = Board.Turn.Opposite();
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

    List<char> promotionPieces = ['q', 'r', 'b', 'n'];
    Piece? promotionPiece = null;

    if (moveString.Length == 5 && promotionPieces.Contains(moveString[4]))
    {
      promotionPiece = PieceFactory.CreatePiece(moveString[4], Board.Turn);
    }

    Move move = new(startSquare, endSquare, promotionPiece);
    return MakeMove(move);
  }

  public void PreformMoves(List<string> moves)
  {
    foreach (string move in moves)
    {
      MakeMove(move);
      Console.WriteLine(Board.Serialize());
      Console.WriteLine("Move: " + move);
      Board.DisplayBoard();
    }
  }
}