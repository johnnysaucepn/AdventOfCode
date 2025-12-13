namespace Utilities;

public record Coord
{
    public long X;
    public long Y;

    public Coord(long x, long y)
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

    public static Coord Parse(string s)
    {
        var parts = s.Split(',');
        var x = long.Parse(parts[0]);
        var y = long.Parse(parts[1]);
        return new Coord(x, y);
    }
}
