namespace Utilities;

public class Grid<T>
{
    public int Width;
    public int Height;

    internal readonly T[,] Data;

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
        Data = new T[width, height];
    }

    public T GetAt(int x, int y)
    {
        return Data[x, y];
    }

    public T GetAt(Coord coord)
    {
        return Data[coord.X, coord.Y];
    }

    public void SetAt(int x, int y, T val)
    {
        Data[x, y] = val;
    }

    public void SetAt(Coord coord, T val)
    {
        Data[coord.X, coord.Y] = val;
    }

    public bool InBounds(Coord here)
    {
        if (here.X < 0) return false;
        if (here.X >= Width) return false;

        if (here.Y < 0) return false;
        if (here.Y >= Height) return false;

        return true;
    }

    public IEnumerable<T> GetSequence(int x, int y, int extent)
    {
        for (var i = 0; i < extent; i++)
        {
            yield return Data[x + i, y];
        }
    }

    public void ForEach(Action<Coord> action)
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                action(new Coord(x, y));
            }
        }
    }
}
