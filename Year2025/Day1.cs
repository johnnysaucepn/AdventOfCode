using Utilities;
using Xunit;

namespace Year2025;

public class Day1
{
    public int Part1(List<string> lines)
    {
        var count = 0;
        var position = 50;
        var maxPosition = 100;
        foreach (var line in lines)
        {
            char dir = line[0];
            string stepString = line[1..];
            int step = int.Parse(stepString);
            if (dir == 'L')
            {
                position = ((position - step) + maxPosition) % maxPosition;
            }
            if (dir == 'R')
            {
                position = ((position + step) + maxPosition) % maxPosition;
            }
            if (position == 0)
            {
                count++;
            }
        }
        return count;
    }

    public int Part2(List<string> lines)
    {
        var count = 0;
        var position = 50;
        var maxPosition = 100;
        foreach (var line in lines)
        {
            char dir = line[0];
            string stepString = line[1..];
            int step = int.Parse(stepString);
            var direction = dir == 'L' ? -1 : 1;

            for (var i = 0; i < step; i++)
            {
                position = ((position + direction) + maxPosition) % maxPosition;
                if (position == 0)
                {
                    count++;
                }
            }

        }
        return count;
    }

    [Fact]
    public void Day1_Part1_Example()
    {
        Assert.Equal(3, Part1(Input.Strings(@"day1example.txt")));
    }

    [Fact]
    public void Day1_Part2_Example2()
    {
        Assert.Equal(6, Part2(Input.Strings(@"day1example.txt")));
    }

}
