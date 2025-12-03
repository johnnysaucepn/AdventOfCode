using Utilities;
using Xunit;

namespace Year2025;

public class Day3
{
    public long Part1(List<string> lines)
    {
        return lines.Sum(line =>
        {
            var batteries = GetBatteries(line);
            var joltage = GetHighestJoltage(batteries);
            return joltage;
        });
    }

    public long Part2(List<string> lines)
    {
        return lines.Sum(line =>
        {
            var batteries = GetBatteries(line);
            var joltage = GetHighestJoltage(batteries, 12);
            return joltage;
        });
    }

    private List<int> GetBatteries(string batteryString)
    {
        return batteryString.Select(c => int.Parse(c.ToString())).ToList();
    }

    private int GetHighestJoltage(List<int> batteries)
    {
        var highestValue = batteries[..^1].Max();
        var firstHighestPosition = batteries.IndexOf(highestValue);
        var restOfBatteries = batteries[(firstHighestPosition + 1)..];
        var secondHighestValue = restOfBatteries.Max();

        return highestValue * 10 + secondHighestValue;
    }

    private long GetHighestJoltage(List<int> batteries, int digits)
    {
        long total = 0;
        var remainingBatteries = batteries;
        for (var i = digits - 1; i >= 0; i--)
        {
            var highestValue = remainingBatteries[..^i].Max();
            var firstHighestPosition = remainingBatteries.IndexOf(highestValue);
            remainingBatteries = remainingBatteries[(firstHighestPosition + 1)..];

            total *= 10;
            total += highestValue;
        }
        return total;
    }

    [Theory]
    [InlineData("987654321111111", 98)]
    [InlineData("811111111111119", 89)]
    [InlineData("234234234234278", 78)]
    [InlineData("818181911112111", 92)]
    public void GetHighestJoltage_Cases(string batteryString, int expectedJoltage)
    {
        var batteries = GetBatteries(batteryString);
        Assert.Equal(expectedJoltage, GetHighestJoltage(batteries));
    }

    [Theory]
    [InlineData("987654321111111", 2, 98)]
    [InlineData("811111111111119", 2, 89)]
    [InlineData("234234234234278", 2, 78)]
    [InlineData("818181911112111", 2, 92)]

    [InlineData("987654321111111", 3, 987)]
    [InlineData("811111111111119", 3, 819)]
    [InlineData("234234234234278", 3, 478)]
    [InlineData("818181911112111", 3, 921)]

    [InlineData("987654321111111", 4, 9876)]
    [InlineData("811111111111119", 4, 8119)]
    [InlineData("234234234234278", 4, 4478)]
    [InlineData("818181911112111", 4, 9211)]

    [InlineData("987654321111111", 5, 98765)]
    [InlineData("811111111111119", 5, 81119)]
    [InlineData("234234234234278", 5, 44478)]
    [InlineData("818181911112111", 5, 92111)]
    public void GetHighestJoltage_General_Cases(string batteryString, int count, int expectedJoltage)
    {
        var batteries = GetBatteries(batteryString);
        Assert.Equal(expectedJoltage, GetHighestJoltage(batteries, count));
    }

    [Fact]
    public void Day3_Part1_Example()
    {
        Assert.Equal(357, Part1(Input.Strings(@"day3example.txt")));
    }

    [Fact]
    public void Day3_Part2_Example()
    {
        Assert.Equal(3121910778619, Part2(Input.Strings(@"day3example.txt")));
    }

}
