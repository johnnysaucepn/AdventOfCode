using Utilities;
using Xunit;

namespace Year2021;

public class Day8
{
    public static int Part1(List<string> lines)
    {
        var answer = lines
            .Select(ParseNote)
            .Select(FindUniqueDigits)
            .Sum();

        return answer;
    }

    private static Note ParseNote(string noteString)
    {
        var parts = noteString.Split('|');
        var patterns = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        var outputs = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        return new Note(patterns, outputs);

    }

    private static int FindUniqueDigits(Note note)
    {
        var count = note.Outputs.Where(o =>
            o.Length == 2 // 1
            || o.Length == 3 // 7
            || o.Length == 4 // 4
            || o.Length == 7 // 8
            ).Count();
        return count;
    }

    public static int Part2(List<string> lines)
    {

        return 0;
    }

    [Theory]
    [InlineData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf")]
    public void Day8_Part1_Example1(string note)
    {
        Assert.Equal(0, Part1([note]));
    }

    [Fact]
    public void Day8_Part1_Example2()
    {
        Assert.Equal(26, Part1(Input.Strings(@"day8example2.txt")));
    }

    [Fact]
    public void Day8_Part2_Example()
    {
        Assert.Equal(0, Part2(Input.Strings(@"day8example3.txt")));
    }

}

internal class Note
{
    public List<string> Patterns { get; private set; }
    public List<string> Outputs { get; private set; }

    public Note(List<string> patterns, List<string> outputs)
    {
        Patterns = patterns;
        Outputs = outputs;
    }
}