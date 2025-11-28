namespace Utilities;

public record Coord
{
    public int X;
    public int Y;

    public Coord(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Coord Up() => new(X, Y - 1);
    public Coord UpRight() => new(X + 1, Y - 1);
    public Coord Right() => new(X + 1, Y);
    public Coord DownRight() => new(X + 1, Y + 1);
    public Coord Down() => new(X, Y + 1);
    public Coord DownLeft() => new(X - 1, Y + 1);
    public Coord Left() => new(X - 1, Y);
    public Coord UpLeft() => new(X - 1, Y - 1);
}
