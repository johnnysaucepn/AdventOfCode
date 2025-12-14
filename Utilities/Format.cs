using System.Text;

namespace Utilities;

public static class Format
{
    public static string DumpBooleanArray(bool[,] array, char falseChar = '.', char trueChar = 'O')
    {
        var sb = new StringBuilder();
        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                sb.Append(array[x, y] ? trueChar : falseChar);
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public static string DumpList<T>(IEnumerable<T> list)
    {
        return string.Join(",", list.Select(x => x.ToString()));
    }

    public static string DumpArray<T>(T[,] array, int downScaleFactor = 1)
    {
        var sb = new StringBuilder();
        for (int y = 0; y < array.GetLength(1); y += downScaleFactor)
        {
            for (int x = 0; x < array.GetLength(0); x += downScaleFactor)
            {
                sb.Append(array[x, y]);
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public static string DumpGrid<T>(Grid<T> grid, int downScaleFactor = 1)
    {
        return DumpArray(grid.Data, downScaleFactor);
    }

    public static string DumpGrid<T>(Grid<T> grid, Func<T, char> formatFunc, int downScaleFactor = 1)
    {
        var sb = new StringBuilder();
        for (int y = 0; y < grid.Data.GetLength(1); y += downScaleFactor)
        {
            for (int x = 0; x < grid.Data.GetLength(0); x += downScaleFactor)
            {
                sb.Append(formatFunc(grid.Data[x, y]));
            }
            sb.AppendLine();
        }
        return sb.ToString();

    }
}
