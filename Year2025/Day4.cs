using Utilities;
using Xunit;

namespace Year2025;

public class Day4
{
    public long Part1(List<string> lines)
    {
        var grid = GridFactory.FromLinesAlpha(lines);
        int accessible = CountAccessibleRolls(grid);
        return accessible;
    }

    public long Part2(List<string> lines)
    {
        var totalRemoved = 0;
        var grid = GridFactory.FromLinesAlpha(lines);
        var accessible = 0;
        do
        {
            accessible = RemoveAccessibleRolls(grid, out var nextGrid);
            totalRemoved += accessible;
            grid = nextGrid;

        } while (accessible > 0);

        return totalRemoved;
    }

    private int CountAccessibleRolls(Grid<char> grid)
    {
        var output = new Grid<char>(grid.Width, grid.Height);
        var accessible = 0;
        grid.ForEach(c =>
        {
            output.SetAt(c, grid.GetAt(c));

            if (grid.GetAt(c) == '@')
            {
                var adjacentRolls = CountAdjacentRolls(grid, c);
                if (adjacentRolls < 4)
                {
                    output.SetAt(c, adjacentRolls.ToString()[0]);
                    accessible++;
                }
            }
        });
        Console.WriteLine(Format.DumpGrid(output));
        return accessible;
    }

    private int RemoveAccessibleRolls(Grid<char> grid, out Grid<char> nextGrid)
    {
        var outputGrid = new Grid<char>(grid.Width, grid.Height);
        var removed = 0;

        grid.ForEach(c =>
        {
            outputGrid.SetAt(c, grid.GetAt(c));

            if (grid.GetAt(c) == '@')
            {
                var adjacentRolls = CountAdjacentRolls(grid, c);
                if (adjacentRolls < 4)
                {
                    outputGrid.SetAt(c, '.');
                    removed++;
                }
            }
        });
        for (var y = 0; y < grid.Height; y++)
        {
            for (var x = 0; x < grid.Width; x++)
            {

            }
        }
        Console.WriteLine(Format.DumpGrid(outputGrid));

        nextGrid = outputGrid;
        return removed;
    }

    private int CountAdjacentRolls(Grid<char> grid, Coord here)
    {
        var adjacentTotal = 0;

        List<Coord> adjacents = [
            here.UpLeft(),
            here.Up(),
            here.UpRight(),
            here.Right(),
            here.DownRight(),
            here.Down(),
            here.DownLeft(),
            here.Left()
            ];

        foreach (var adjacent in adjacents)
        {
            if (grid.InBounds(adjacent) && grid.GetAt(adjacent) == '@')
            {
                adjacentTotal++;
            }
        }
        return adjacentTotal;
    }


    [Fact]
    public void Day4_Part1_Example()
    {
        Assert.Equal(13, Part1(Input.Strings(@"day4example.txt")));
    }

    [Fact]
    public void Day4_Part2_Example()
    {
        Assert.Equal(43, Part2(Input.Strings(@"day4example.txt")));
    }
}
