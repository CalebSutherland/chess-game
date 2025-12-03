using ChessGame.Board;
using ChessGame.Pieces;

namespace ChessGame.GameState;

public class GameStatusChecker(ChessBoard board)
{
  private readonly ChessBoard _board = board;
  private readonly MoveValidator _validator = new(board);

  public bool IsCheck()
  {
    return MoveValidator.IsKingInCheck(_board.Turn, _board);
  }

  public bool IsCheckMate()
  {
    return IsCheck() && GetAllLegalMoves().Count == 0;
  }

  public bool IsStaleMate()
  {
    return !IsCheck() && GetAllLegalMoves().Count == 0;
  }

  public string DetermineGameState()
  {
    if (IsCheckMate()) return "Checkmate";
    if (IsStaleMate()) return "Stalemate";
    return "Ongoing";
  }

  public List<Move> GetAllLegalMoves()
  {
    List<Move> legalMoves = [];

    for (int row = 0; row < _board.Size; row++)
    {
      for (int col = 0; col < _board.Size; col++)
      {
        Piece? piece = _board.GetPiece(new Square(row, col));
        if (piece != null && piece.Color == _board.Turn)
        {
          Square start = new(row, col);
          List<Square> current = piece.GetMoves(start, _board);
          foreach (Square end in current)
          {
            Move move = new(start, end);
            if (_validator.IsLegalMove(move))
            {
              legalMoves.Add(move);
            }
          }
        }
      }
    }
    return legalMoves;
  }
}