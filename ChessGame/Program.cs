using ChessGame;
using ChessGame.Board;

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

List<string> enPassant = [
  "d2d4",
  "e7e5",
  "d4d5",
  "c7c5",
  "d5c6",
  "e5e4",
  "f2f4",
  "e4f3"
];

List<string> promotion = [
  "a2a4",
  "b7b5",
  "a4b5",
  "a7a5",
  "b5b6",
  "a5a4",
  "b6b7",
  "a4a3",
  "b7c8q",
  "a3b2",
  "c2c4",
  "b2c1r",
  "d1a4",
];

List<string> checkMate = [
  "e2e4",
  "e7e5",
  "f1c4",
  "a7a6",
  "d1f3",
  "a6a5",
  "f3f7"
];

List<string> pawnCapture = [
  "d2d4",
  "e7e5",
  "d4e5",
  "f7f6",
  "e5f6",
  "g7f6",
];

List<string> staleMate = [
  "g6h6"
];

List<string> twoAttackers = [
  "f1d2"
];

// Game game = new(new ChessBoard());
// Console.WriteLine(game.Board.Serialize());
// game.Board.DisplayBoard();

// game.PreformMoves(checkMate);

// Game stale = new(new ChessBoard("7k/5Q2/6K1/8/8/8/8/8 w - - 0 1"));
// Console.WriteLine(stale.Board.Serialize());
// stale.Board.DisplayBoard();

// stale.PreformMoves(staleMate);

Game two = new(new ChessBoard("4k3/pppppppp/8/8/B3P3/3P1N2/PPP2PPP/R1BQKN1R w - - 0 1"));
Console.WriteLine(two.Board.Serialize());
two.Board.DisplayBoard();

two.PreformMoves(twoAttackers);

