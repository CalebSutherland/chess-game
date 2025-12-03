using ChessGame.Board;
using ChessGame.GameState;
using ChessGame.MoveHandlers;
using ChessGame.Notation;
using ChessGame.Pieces;
using ChessGame.Types;

namespace ChessGame;

public class Game
{
  public ChessBoard Board { get; }
  private readonly IMoveHandler _handler;
  public string SAN { get; set; }

  public Game(ChessBoard board)
  {
    Board = board;
    SAN = "";
    _handler = new CastlingHandler();
    _handler.SetNext(new EnPassantHandler())
      .SetNext(new PromotionHandler())
      .SetNext(new NormalMoveHandler());
  }

  public bool MakeMove(Move move)
  {
    SANBuilder sanBuilder = new();
    if (_handler.HandleMove(move, Board, sanBuilder))
    {
      Board.Turn = Board.Turn.Opposite();
      GameStatusChecker stateChecker = new(Board);

      if (stateChecker.IsCheckMate())
      {
        sanBuilder.Checkmate = true;
      }
      else if (stateChecker.IsCheck())
      {
        sanBuilder.Check = true;
      }

      SAN = sanBuilder.Build();

      return true;
    }
    else
    {
      SAN = "";
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
      GameStatusChecker stateChecker = new(Board);
      Console.WriteLine(stateChecker.DetermineGameState());
      Console.WriteLine(Board.Serialize());
      Console.WriteLine("UCI: " + move + "    SAN: " + SAN);
      Board.DisplayBoard();
    }
  }
}