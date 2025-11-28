namespace Utilities;

public class Grid<T>
{
    public int Width;
    public int Height;

    private readonly T[,] _data;

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
        _data = new T[width, height];
    }

    public T GetAt(int x, int y)
    {
        return _data[x, y];
    }

    public T GetAt(Coord coord)
    {
        return _data[coord.X, coord.Y];
    }

    public void SetAt(int x, int y, T val)
    {
        _data[x, y] = val;
    }

    public void SetAt(Coord coord, T val)
    {
        _data[coord.X, coord.Y] = val;
    }

    public bool InBounds(Coord here)
    {
        if (here.X < 0) return false;
        if (here.X >= Width) return false;

        if (here.Y < 0) return false;
        if (here.Y >= Height) return false;

        return true;
    }

    public static Grid<char> FromLinesAlpha(List<string> lines)
    {
        var grid = new Grid<char>(lines[0].Length, lines.Count);
        for (var y = 0; y < grid.Height; y++)
        {
            for (var x = 0; x < grid.Width; x++)
            {
                grid.SetAt(x, y, lines[y][x]);
            }
        }

        return grid;
    }

    public static Grid<int> FromLinesNumeric(List<string> lines)
    {
        var grid = new Grid<int>(lines[0].Length, lines.Count);
        for (var y = 0; y < grid.Height; y++)
        {
            for (var x = 0; x < grid.Width; x++)
            {
                var val = (int)char.GetNumericValue(lines[y][x]);
                grid.SetAt(x, y, val);
            }
        }

        return grid;
    }

    public IEnumerable<T> GetSequence(int x, int y, int extent)
    {
        for (var i = 0; i < extent; i++)
        {
            yield return _data[x + i, y];
        }
    }
}
