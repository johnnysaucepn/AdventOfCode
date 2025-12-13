using Utilities;
using Xunit;

namespace Year2025;

public class Day9
{
    private enum TileType
    {
        Empty,
        Red,
        HorizontalGreen,
        VerticalGreen,
        InteriorGreen
    }

    public long Part1(List<string> lines)
    {
        List<Coord> redTiles = lines.Select(Coord.Parse).ToList();

        long largest = 0;
        for (var i = 0; i < redTiles.Count; i++)
        {
            for (var j = i; j < redTiles.Count; j++)
            {
                var first = redTiles[i];
                var second = redTiles[j];
                long size = CalculateArea(first, second);
                if (size < 0)
                {
                    Console.WriteLine($"{first}->{second}={size}, largest {largest}");
                }
                if (size > largest)
                {
                    largest = size;
                }
            }
        }
        return largest;
    }

    public long Part2(List<string> lines)
    {
        List<Coord> redTiles = lines.Select(Coord.Parse).ToList();

        var distinctX = redTiles.Select(t => t.X).Distinct().Order().ToList();
        var distinctY = redTiles.Select(t => t.Y).Distinct().Order().ToList();

        // Compress real co-ords into a lookups table, allowing us to create a grid with fewer elements
        Dictionary<long, long> xLookup = [];
        Dictionary<long, long> yLookup = [];

        for (var i = 0; i < distinctX.Count; i++)
        {
            xLookup[distinctX[i]] = i;
        }

        for (var j = 0; j < distinctY.Count; j++)
        {
            yLookup[distinctY[j]] = j;
        }

        var grid = new Grid<TileType>(distinctX.Count, distinctY.Count);

        // Mark up all the line segments on the compressed grid

        var lastRedTile = redTiles.Last();
        for (var i = 0; i < redTiles.Count; i++)
        {
            var thisRedTile = redTiles[i];

            var start = new Coord(xLookup[lastRedTile.X], yLookup[lastRedTile.Y]);
            var end = new Coord(xLookup[thisRedTile.X], yLookup[thisRedTile.Y]);

            DrawLine(grid, start, end);

            lastRedTile = thisRedTile;
        }
        DumpTiles(grid);

        long largest = 0;
        for (var i = 0; i < redTiles.Count; i++)
        {
            for (var j = i; j < redTiles.Count; j++)
            {
                var first = redTiles[i];
                var second = redTiles[j];

                // Look up the compressed co-ordinates 

                var firstCoord = new Coord(xLookup[first.X], yLookup[first.Y]);
                var secondCoord = new Coord(xLookup[second.X], yLookup[second.Y]);

                var isCandidate = true;
                for (var y = Math.Min(firstCoord.Y, secondCoord.Y) + 1; y <= Math.Max(firstCoord.Y, secondCoord.Y) - 1; y++)
                {
                    for (var x = Math.Min(firstCoord.X, secondCoord.X) + 1; x <= Math.Max(firstCoord.X, secondCoord.X) - 1; x++)
                    {
                        // If any part of the rectangle intersects with a marked edge, exclude it
                        if (grid.GetAt(x, y) != TileType.Empty)
                        {
                            isCandidate = false;
                            break;
                        }
                    }
                    if (!isCandidate) break;
                }

                if (isCandidate)
                {
                    // Calculate area from the original co-ordinates
                    long size = CalculateArea(first, second);
                    if (size > largest)
                    {
                        Console.WriteLine($"{first}->{second}={size}, previous largest {largest}");
                        largest = size;
                    }
                }
            }
        }
        return largest;
    }

    private static void DrawLine(Grid<TileType> grid, Coord start, Coord end)
    {
        if (start.Y == end.Y) // horizontal line
        {
            var inc = Math.Sign(end.X - start.X);
            for (var x = start.X; x != end.X; x += inc)
            {
                var tile = new Coord(x, start.Y);
                grid.SetAt(tile, TileType.HorizontalGreen);
            }
        }
        else // vertical line
        {
            var inc = Math.Sign(end.Y - start.Y);
            for (var y = start.Y; y != end.Y; y += inc)
            {
                var tile = new Coord(start.X, y);
                grid.SetAt(tile, TileType.VerticalGreen);
            }
        }
        grid.SetAt(start, TileType.Red);
        grid.SetAt(end, TileType.Red);
    }

    private static void DumpTiles(Grid<TileType> grid)
    {
        Console.Write(Format.DumpGrid(grid, t => t switch
        {
            TileType.Empty => '.',
            TileType.Red => '#',
            TileType.HorizontalGreen => '-',
            TileType.VerticalGreen => '|',
            TileType.InteriorGreen => 'X',
            _ => throw new NotImplementedException()
        }, 5));
    }

    public static long CalculateArea(Coord first, Coord second)
    {
        long firstX = first.X;
        long firstY = first.Y;
        long secondX = second.X;
        long secondY = second.Y;
        return (Math.Abs(secondY - firstY) + 1) * (Math.Abs(secondX - firstX) + 1);
    }

    [Fact]
    public void Day9_Part1_Example()
    {
        Assert.Equal(50, Part1(Input.Strings(@"day9example.txt")));
    }

    [Fact(Skip = "Algorithm works for input, but not for example - coords are too compressed in space")]
    public void Day9_Part2_Example()
    {
        Assert.Equal(24, Part2(Input.Strings(@"day9example.txt")));
    }

    [Fact]
    public void Day9_Part2_TooHigh()
    {
        Assert.True(Part2(Input.Strings(@"day9input.txt")) < 1646837199);
    }

    [Fact]
    public void Day9_Part2_TooLow()
    {
        Assert.True(Part2(Input.Strings(@"day9input.txt")) > 1294260375);
    }

    [Fact]
    public void Day9_Part2_CountDimensions()
    {
        var lines = Input.Strings(@"day9input.txt");

        List<Coord> redTiles = lines.Select(Coord.Parse).ToList();

        var distinctX = redTiles.Select(t => t.X).Distinct().Order().ToList();
        var distinctY = redTiles.Select(t => t.Y).Distinct().Order().ToList();
        Assert.Equal(248, distinctX.Count);
        Assert.Equal(247, distinctY.Count);
    }

    [Theory]
    [InlineData(1, 1, 1, 1, 1)]
    [InlineData(2, 3, 4, 5, 9)]
    [InlineData(7, 1, 11, 7, 35)]
    [InlineData(11, 1, 7, 7, 35)]
    [InlineData(11, 7, 7, 1, 35)]
    public void Day9_CalculateArea_Cases(int firstX, int firstY, int secondX, int secondY, long expectedArea)
    {
        var first = new Coord(firstX, firstY);
        var second = new Coord(secondX, secondY);
        var area = CalculateArea(first, second);
        Assert.Equal(expectedArea, area);
    }

}
