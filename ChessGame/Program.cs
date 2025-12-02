using ChessGame;
using ChessGame.Board;

Game game = new(new ChessBoard());
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

game.MakeMove("d2d4");
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

game.MakeMove("d7d5");
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

game.MakeMove("c1f4");
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

game.MakeMove("c8f5");
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();


game.MakeMove("b1c3");
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

game.MakeMove("b8c6");
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

game.MakeMove("d1d2");
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

game.MakeMove("d8d7");
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

game.MakeMove("e1c1");
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

game.MakeMove("e8c8");
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();


// Kingside Castling
// game.MakeMove("e2e4");
// Console.WriteLine(game.Board.Serialize());
// game.Board.DisplayBoard();

// game.MakeMove("e7e5");
// Console.WriteLine(game.Board.Serialize());
// game.Board.DisplayBoard();

// game.MakeMove("f1d3");
// Console.WriteLine(game.Board.Serialize());
// game.Board.DisplayBoard();

// game.MakeMove("f8d6");
// Console.WriteLine(game.Board.Serialize());
// game.Board.DisplayBoard();

// game.MakeMove("g1f3");
// Console.WriteLine(game.Board.Serialize());
// game.Board.DisplayBoard();

// game.MakeMove("g8f6");
// Console.WriteLine(game.Board.Serialize());
// game.Board.DisplayBoard();

// game.MakeMove("e1g1");
// Console.WriteLine(game.Board.Serialize());
// game.Board.DisplayBoard();

// Console.WriteLine(game.MakeMove("e8g8"));
// Console.WriteLine(game.Board.Serialize());
// game.Board.DisplayBoard();
