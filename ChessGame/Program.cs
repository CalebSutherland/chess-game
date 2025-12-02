using ChessGame;
using ChessGame.Board;

static void PreformMoves(Game game, List<string> moves)
{
  foreach (string move in moves)
  {
    game.MakeMove(move);
    Console.WriteLine(game.Board.Serialize());
    game.Board.DisplayBoard();
  }
}

List<string> queenside = [
  "d2d4",
  "d7d5",
  "c1f4",
  "c8f5",
  "b1c3",
  "b8c6",
  "d1d2",
  "d8d7",
  "e1c1",
  "e8c8",
];

List<string> queenside2 = [
  "d2d4",
  "d7d5",
  "c1f4",
  "c8f5",
  "b1c3",
  "b8c6",
  "d1d2",
  "d8d7",
  "a1b1",
  "a8b8",
  "e1c1",
  "e8c8",
];

List<string> kingside = [
  "e2e4",
  "e7e5",
  "f1d3",
  "f8d6",
  "g1f3",
  "g8f6",
  "e1g1",
  "e8g8",
];

Game game = new(new ChessBoard());
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

PreformMoves(game, queenside2);
