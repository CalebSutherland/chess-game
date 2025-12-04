using ChessGame.Board;
using ChessGame.Pieces;

namespace ChessGame.GameState;

public class GameStatusChecker(ChessBoard board)
{
  private readonly ChessBoard _board = board;

  public bool IsCheck()
  {
    return MoveValidator.IsKingInCheck(_board.Turn, _board);
  }

  public bool IsCheckMate()
  {
    return IsCheck() && MoveValidator.GetAllLegalMoves(_board).Count == 0;
  }

  public bool IsStaleMate()
  {
    return !IsCheck() && MoveValidator.GetAllLegalMoves(_board).Count == 0;
  }

  public string DetermineGameState()
  {
    if (IsCheckMate()) return "Checkmate";
    if (IsStaleMate()) return "Stalemate";
    return "Ongoing";
  }
}