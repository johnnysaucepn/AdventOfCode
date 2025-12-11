namespace Utilities;

public struct Coord3d
{
    public int X;
    public int Y;
    public int Z;

    public Coord3d(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public double DistanceTo(Coord3d other)
    {
        return Math.Sqrt(
            Math.Pow(other.X - this.X, 2)
            + Math.Pow(other.Y - this.Y, 2)
            + Math.Pow(other.Z - this.Z, 2)
            );
    }

    public static double DistanceBetween(Coord3d a, Coord3d b)
    {
        return a.DistanceTo(b);
    }

    public static Coord3d Parse(string s)
    {
        var parts = s.Split(',');
        var x = int.Parse(parts[0]);
        var y = int.Parse(parts[1]);
        var z = int.Parse(parts[2]);
        return new Coord3d(x, y, z);
    }

    public override string ToString()
    {
        return $"{X},{Y},{Z}";
    }
}