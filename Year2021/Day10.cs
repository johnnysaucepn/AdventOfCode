using Utilities;
using Xunit;

namespace Year2021;

public class Day10
{
    public record struct ChunkAnalysis
    {
        public List<char> StillOpen;
        public List<char> LeftOver;

        public ChunkAnalysis(List<char> stillOpen, List<char> leftOver)
        {
            StillOpen = stillOpen;
            LeftOver = leftOver;
        }
    }

    public readonly Dictionary<char, char> ChunkPatternPairs = new()
    {
        ['('] = ')',
        ['['] = ']',
        ['{'] = '}',
        ['<'] = '>'
    };

    public long Part1(List<string> lines)
    {
        var total = lines
            .Select(ParseLine)
            .Where(IsCorrupted)
            .Select(CalculateCorruptionScore)
            .Sum();

        return total;
    }

    public long Part2(List<string> lines)
    {
        var scores = lines
            .Select(ParseLine)
            .Where(IsIncomplete)
            .Select(CalculateIncompleteScore)
            .Order()
            .ToList();

        foreach (var item in scores) Console.WriteLine(item);

        var length = scores.Count;
        Assert.True(length % 2 == 1);// Ensure that we have an odd number of results

        var midPoint = (length - 1) / 2;
        return scores[midPoint];
    }

    private ChunkAnalysis ParseLine(string line)
    {
        var chunks = new Stack<char>();
        var remaining = new Stack<char>(line.Reverse());

        while (remaining.Count > 0)
        {
            var candidate = remaining.Pop();
            if (ChunkPatternPairs.ContainsKey(candidate))
            {
                chunks.Push(candidate);
            }
            else // is an end
            {
                var lastChunk = chunks.Pop();
                var expectedMatch = ChunkPatternPairs[lastChunk];

                if (expectedMatch != candidate)
                {
                    // Push it back
                    chunks.Push(lastChunk);
                    remaining.Push(candidate);
                    return new ChunkAnalysis(chunks.ToList(), remaining.ToList());
                }
            }
        }
        return new ChunkAnalysis(chunks.ToList(), remaining.ToList());
    }

    private bool IsCorrupted(ChunkAnalysis chunkState)
    {
        return chunkState.StillOpen.Count > 0 && chunkState.LeftOver.Count > 0;
    }

    private bool IsIncomplete(ChunkAnalysis chunkState)
    {
        return chunkState.StillOpen.Count > 0 && chunkState.LeftOver.Count == 0;
    }

    public bool IsComplete(ChunkAnalysis chunkState)
    {
        return chunkState.StillOpen.Count == 0 && chunkState.LeftOver.Count == 0;
    }

    private long CalculateCorruptionScore(ChunkAnalysis chunkState)
    {
        Assert.True(IsCorrupted(chunkState));

        var firstCorrupted = chunkState.LeftOver.First();
        // corruption found, look up the score for what we found
        if (firstCorrupted == ')') return 3;
        if (firstCorrupted == ']') return 57;
        if (firstCorrupted == '}') return 1197;
        if (firstCorrupted == '>') return 25137;
        return 0;
    }

    private long CalculateIncompleteScore(ChunkAnalysis chunkState)
    {
        Assert.True(IsIncomplete(chunkState));

        long total = 0;
        foreach (char ch in chunkState.StillOpen)
        {
            total *= 5;
            if (ch == '(') total += 1;
            if (ch == '[') total += 2;
            if (ch == '{') total += 3;
            if (ch == '<') total += 4;

            Assert.True(total > 0);
        }
        return total;

    }

    [Theory]
    [InlineData("[({(<(())[]>[[{[]{<()<>>", false)]
    [InlineData("[(()[<>])]({[<{<<[]>>(", false)]
    [InlineData("{([(<{}[<>[]}>{[]{[(<()>", true)]
    [InlineData("(((({<>}<{<{<>}{[]{[]{}", false)]
    [InlineData("[[<[([]))<([[{}[[()]]]", true)]
    [InlineData("[{[{({}]{}}([{[{{{}}([]", true)]
    [InlineData("{<[[]]>}<{[{[{[]{()[[[]", false)]
    [InlineData("[<(<(<(<{}))><([]([]()", true)]
    [InlineData("<{([([[(<>()){}]>(<<{{", true)]
    [InlineData("<{([{{}}[<[[[<>{}]]]>[]]", false)]
    public void IsCorrupted_Cases(string line, bool expectedCorruption)
    {
        var result = ParseLine(line);
        Assert.Equal(expectedCorruption, IsCorrupted(result));
    }

    [Theory]
    [InlineData("{([(<{}[<>[]}>{[]{[(<()>", 1197)]
    [InlineData("[[<[([]))<([[{}[[()]]]", 3)]
    [InlineData("[{[{({}]{}}([{[{{{}}([]", 57)]
    [InlineData("[<(<(<(<{}))><([]([]()", 3)]
    [InlineData("<{([([[(<>()){}]>(<<{{", 25137)]
    public void CheckForCorruption_Cases(string line, int expectedScore)
    {
        var result = ParseLine(line);
        Assert.Equal(expectedScore, CalculateCorruptionScore(result));
    }

    [Theory]
    [InlineData("[({(<(())[]>[[{[]{<()<>>", true)]
    [InlineData("[(()[<>])]({[<{<<[]>>(", true)]
    [InlineData("{([(<{}[<>[]}>{[]{[(<()>", false)]
    [InlineData("(((({<>}<{<{<>}{[]{[]{}", true)]
    [InlineData("[[<[([]))<([[{}[[()]]]", false)]
    [InlineData("[{[{({}]{}}([{[{{{}}([]", false)]
    [InlineData("{<[[]]>}<{[{[{[]{()[[[]", true)]
    [InlineData("[<(<(<(<{}))><([]([]()", false)]
    [InlineData("<{([([[(<>()){}]>(<<{{", false)]
    [InlineData("<{([{{}}[<[[[<>{}]]]>[]]", true)]
    public void IsIncomplete_Cases(string line, bool expectedIncomplete)
    {
        var result = ParseLine(line);
        Assert.Equal(expectedIncomplete, IsIncomplete(result));
    }

    [Theory]
    [InlineData("[({(<(())[]>[[{[]{<()<>>", 288957)]
    [InlineData("[(()[<>])]({[<{<<[]>>(", 5566)]
    [InlineData("(((({<>}<{<{<>}{[]{[]{}", 1480781)]
    [InlineData("{<[[]]>}<{[{[{[]{()[[[]", 995444)]
    [InlineData("<{([{{}}[<[[[<>{}]]]>[]]", 294)]
    public void ScoreAutoComplete_Cases(string line, int expectedScore)
    {
        var result = ParseLine(line);
        Assert.Equal(expectedScore, CalculateIncompleteScore(result));
    }

    [Fact]
    public void Part1_Example1()
    {
        Assert.Equal(26397, Part1(Input.Strings(@"day10example.txt")));
    }

    [Fact]
    public void Part2_Example2()
    {
        Assert.Equal(288957, Part2(Input.Strings(@"day10example.txt")));
    }

}

