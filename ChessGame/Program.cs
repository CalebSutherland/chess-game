using ChessGame.Board;
using ChessGame.Parsers;
using ChessGame.Pieces;

ChessBoard board = new();
board.DisplayBoard();
string fen = BoardParser.Serialize(board.Grid);
Console.WriteLine(fen);

ChessBoard copy = board.Copy();

Square start = new(1, 4);
Square end = new(3, 4);

Piece? piece = copy.GetPiece(start);
copy.SetPiece(end, piece);
copy.SetPiece(start, null);

copy.DisplayBoard();
fen = BoardParser.Serialize(copy.Grid);
Console.WriteLine(fen);

board.DisplayBoard();
fen = BoardParser.Serialize(board.Grid);
Console.WriteLine(fen);