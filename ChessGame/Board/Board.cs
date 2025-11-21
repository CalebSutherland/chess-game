using ChessGame.Common;
using ChessGame.Pieces;

namespace ChessGame.Board;

class Board
{
    public Piece?[,] Grid { get; }

    public Board()
    {
        Piece?[,] grid = new Piece?[8, 8];
        for (int i = 0; i < 8; i++)
        {
            grid[0, i] = new Rook(Color.Black);
            grid[7, i] = new Rook(Color.White);
        }
        Grid = grid;
    }

    public void DisplayBoard()
    {
        Console.WriteLine("  | a b c d e f g h |");
        Console.WriteLine("-----------------------");
        for (int i = 0; i < 8; i++)
        {
            Console.Write(8 - i);
            Console.Write(" | ");
            for (int j = 0; j < 8; j++)
            {
                Piece piece = Grid[i, j];
                if (piece == null)
                {
                    Console.Write(". ");
                    continue;
                }
                char symbol = piece.Symbol;
                if (piece.Color == Color.White)
                {
                    Console.Write(char.ToUpper(symbol) + " ");
                }
                else
                {
                    Console.Write(symbol + " ");
                }
            }
            Console.Write("| ");
            Console.Write(8 - i);
            Console.Write("\n");
        }
        Console.WriteLine("-----------------------");
        Console.WriteLine("  | a b c d e f g h |");
    }
}
