using Utilities;
using Xunit;

namespace Year2025;

public class Day7
{
    public long Part1(List<string> lines)
    {
        var manifold = GridFactory.FromLinesAlpha(lines);

        var entry = lines[0].IndexOf('S');
        manifold.SetAt(entry, 0, '|');
        var splitCount = 0;

        for (var y = 0; y < manifold.Height - 1; y++)
        {
            for (var x = 0; x < manifold.Width; x++)
            {
                var here = new Coord(x, y);
                var down = here.Down();
                var splitLeft = here.DownLeft();
                var splitRight = here.DownRight();
                if (manifold.GetAt(here) == '|')
                {
                    if (manifold.GetAt(down) == '^')
                    {
                        splitCount++;
                        if (manifold.GetAt(splitLeft) == '.')
                        {
                            manifold.SetAt(splitLeft, '|');
                        }

                        if (manifold.GetAt(splitRight) == '.')
                        {
                            manifold.SetAt(splitRight, '|');
                        }
                    }
                    else
                    {
                        manifold.SetAt(down, '|');
                    }
                }
            }
            Console.WriteLine(Format.DumpGrid(manifold));
            Console.WriteLine(splitCount);
        }
        return splitCount;
    }

    public long Part2(List<string> lines)
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Day7_Part1_Example()
    {
        Assert.Equal(21, Part1(Input.Strings(@"day7example.txt")));
    }

    [Fact]
    public void Day7_Part2_Example()
    {
        Assert.Equal(0, Part2(Input.Strings(@"day7example.txt")));
    }

}