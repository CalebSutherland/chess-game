using ChessGame.Board;
using ChessGame.Pieces;

ChessBoard board = new();
board.DisplayBoard();

Piece piece = board.Grid[1, 0];
List<Square> moves = piece.GetMoves(new Square(1, 0), board);
foreach (Square square in moves)
{
  Console.Write(square.Row + ",");
  Console.Write(square.Col);
  Console.Write("\n");
}
