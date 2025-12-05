using Utilities;
using Xunit;

namespace Year2025;

public class Day5
{
    public record Range
    {
        public readonly long Start;
        public readonly long End;

        public Range(long start, long end)
        {
            Start = start;
            End = end;
        }
    }

    public long Part1(List<List<string>> sets)
    {
        var ranges = ParseRanges(sets[0]);
        var ingredients = ParseIngredients(sets[1]);

        var fresh = ingredients.Where(i => IsFresh(i, ranges)).ToList();

        return fresh.Count;
    }

    public long Part2(List<List<string>> sets)
    {
        var ranges = ParseRanges(sets[0]);

        throw new NotImplementedException();
    }

    private bool IsFresh(long ingredient, List<Range> ranges)
    {
        foreach (var range in ranges)
        {
            if (ingredient >= range.Start && ingredient <= range.End)
            {
                return true;
            }
        }
        return false;
    }

    private static List<long> ParseIngredients(List<string> lines)
    {
        return lines.Select(long.Parse).ToList();
    }

    private List<Range> ParseRanges(List<string> lines)
    {
        return lines.
            Select(l =>
            {
                var parts = l.Split('-');
                return new Range(long.Parse(parts[0]), long.Parse(parts[1]));
            }).ToList();
    }

    [Fact]
    public void Day5_ParseIngredients()
    {
        var sets = Input.StringSets(@"day5example.txt");
        var ranges = ParseRanges(sets[0]);
        var ingredients = ParseIngredients(sets[1]);

        Assert.Equal(4, ranges.Count);
        Assert.Equal(6, ingredients.Count);

        Assert.Equal(3, ranges[0].Start);
        Assert.Equal(14, ranges[1].End);
        Assert.Equal(12, ranges[3].Start);
        Assert.Equal(18, ranges[3].End);
    }

    [Fact]
    public void Day5_Part1_Example()
    {
        Assert.Equal(3, Part1(Input.StringSets(@"day5example.txt")));
    }

    [Fact]
    public void Day5_Part2_Example()
    {
        Assert.Equal(14, Part2(Input.StringSets(@"day5example.txt")));
    }

}