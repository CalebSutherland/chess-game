using ChessGame;
using ChessGame.Board;

Game game = new(new ChessBoard());
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

Square start = new(7, 1);
Square end = new(5, 2);

Move move = new(start, end);
Console.WriteLine(game.MakeMove(move));
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

start = new(0, 1);
end = new(2, 2);
move = new(start, end);
Console.WriteLine(game.MakeMove(move));
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

start = new(6, 4);
end = new(4, 4);
move = new(start, end);
Console.WriteLine(game.MakeMove(move));
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

start = new(1, 4);
end = new(3, 4);
move = new(start, end);
Console.WriteLine(game.MakeMove(move));
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

start = new(6, 5);
end = new(5, 5);
move = new(start, end);
Console.WriteLine(game.MakeMove(move));
Console.WriteLine(game.Board.Serialize());
game.Board.DisplayBoard();

