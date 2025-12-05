namespace Utilities;

public static class GridFactory
{
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
}