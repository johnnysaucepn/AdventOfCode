using Utilities;
using Xunit;

namespace Year2025;

public class Day2
{
    public long Part1(List<string> lines)
    {
        IEnumerable<(long start, long end)> ranges = ParseRanges(lines);
        var invalids = ranges
            .SelectMany(p => FindInvalidDouble(p.start, p.end));

        return invalids.Sum();
    }

    public long Part2(List<string> lines)
    {
        IEnumerable<(long start, long end)> ranges = ParseRanges(lines);
        var invalids = ranges
            .SelectMany(p => FindInvalidTuples(p.start, p.end));

        return invalids.Distinct().Sum();
    }

    private IEnumerable<long> FindInvalidTuples(long start, long end)
    {
        for (var i = start; i <= end; i++)
        {
            for (var n = 2; n < 8; n++)
            {
                if (IsInvalidTuple(i, n))
                {
                    yield return i;
                }
            }
        }
    }

    private IEnumerable<(long start, long end)> ParseRanges(List<string> lines)
    {
        return lines
            .SelectMany(l => l.Split(','))
            .Select(r =>
            {
                var pair = r.Split('-');
                return (start: long.Parse(pair[0]), end: long.Parse(pair[1]));
            });
    }


    private IEnumerable<long> FindInvalidDouble(long start, long end)
    {
        for (var i = start; i <= end; i++)
        {
            if (IsInvalidDouble(i)) yield return i;
        }
    }

    private bool IsInvalidDouble(long candidate)
    {
        var candidateString = candidate.ToString();
        if (candidateString.Length % 2 != 0) return false; // odd length cannot have two pairs;
        var half = candidateString.Length / 2;
        if (candidateString[..half] == candidateString[half..]) return true;
        return false;
    }

    private bool IsInvalidTuple(long candidate, int repeatLength)
    {
        var candidateString = candidate.ToString();
        if (candidateString.Length % repeatLength != 0) return false; // odd length cannot have two pairs;
        var fraction = candidateString.Length / repeatLength;
        var pieces = candidateString.Chunk(fraction).Select(c => string.Concat(c));
        return pieces.All(p => p.Equals(pieces.First()));
    }

    [Theory]
    [InlineData(11, true)]
    [InlineData(22, true)]
    [InlineData(1010, true)]
    [InlineData(1188511885, true)]
    [InlineData(1, false)]
    [InlineData(101, false)]
    [InlineData(446447, false)]
    public void Day2_IsInvalidDouble(long candidate, bool expected)
    {
        Assert.Equal(expected, IsInvalidDouble(candidate));
    }

    [Theory]
    [InlineData(11, true)]
    [InlineData(22, true)]
    [InlineData(1010, true)]
    [InlineData(1188511885, true)]
    [InlineData(1, false)]
    [InlineData(101, false)]
    [InlineData(446447, false)]
    public void Day2_IsInvalidTuple_2(long candidate, bool expected)
    {
        Assert.Equal(expected, IsInvalidTuple(candidate, 2));
    }

    [Theory]
    [InlineData(111, true)]
    [InlineData(222, true)]
    [InlineData(101010, true)]
    [InlineData(118851188511885, true)]
    [InlineData(1, false)]
    [InlineData(101, false)]
    [InlineData(1010, false)]
    [InlineData(446447, false)]
    public void Day2_IsInvalidTuple_3(long candidate, bool expected)
    {
        Assert.Equal(expected, IsInvalidTuple(candidate, 3));
    }

    [Theory]
    [InlineData(11, 22, new long[] { 11, 22 })]
    public void Day2_FindInvalidDouble(long start, long end, long[] expected)
    {
        Assert.Equal(expected, FindInvalidDouble(start, end));
    }

    [Fact]
    public void Day2_Part1_Example()
    {
        Assert.Equal(1227775554, Part1(Input.Strings(@"day2example.txt")));
    }

    [Fact]
    public void Day2_Part2_Example()
    {
        Assert.Equal(4174379265, Part2(Input.Strings(@"day2example.txt")));
    }
}
