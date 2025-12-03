namespace ChessGame.Notation;

public class SANBuilder
{
  public string SAN { get; set; } = "";
  public bool Kingside { get; set; } = false;
  public bool Queenside { get; set; } = false;
  public string Piece { get; set; } = "";
  public bool Capture {get; set; } = false;
  public string Square { get; set; } = "";
  public string Promotion { get; set; } = "";
  public bool Check { get; set; } = false;
  public bool Checkmate { get; set; } = false;

  public string Build()
  {
    if (Kingside) return "0-0";
    if (Queenside) return "0-0-0";

    SAN += Piece;
    if (Capture) SAN += "x";
    SAN += Square;
    SAN += Promotion;

    if (Checkmate) SAN += "#";
    else if (Check) SAN += "+";

    return SAN;
  }
}