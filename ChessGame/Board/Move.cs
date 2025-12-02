using ChessGame.Pieces;

namespace ChessGame.Board;

public record Move(Square Start, Square End, Piece? Promotion = null);