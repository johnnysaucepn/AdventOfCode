using Utilities;
using Xunit;

namespace Year2021;

public class Day8
{
    public int Part1(List<string> lines)
    {
        var answer = lines
            .Select(ParseNote)
            .Select(FindUniqueDigits)
            .Sum();

        return answer;
    }

    private Note ParseNote(string noteString)
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

    public int Part2(List<string> lines)
    {
        var answer = lines
            .Select(ParseNote)
            .Select(n =>
            {
                var code = BreakTheCode(n);

                var normalised = n.Outputs.Select(o => string.Concat(o.ToCharArray().Order()));

                var outputString = string.Concat(normalised.Select(o => code.IndexOf(o)));

                return int.Parse(outputString);
            }).Sum();
        return answer;
    }

    private static List<string> BreakTheCode(Note note)
    {
        var one = note.Patterns.First(o => o.Length == 2).ToCharArray();
        var seven = note.Patterns.First(o => o.Length == 3).ToCharArray();
        var four = note.Patterns.First(o => o.Length == 4).ToCharArray();
        var eight = note.Patterns.First(o => o.Length == 7).ToCharArray(); // ignore this, doesn't tell us anything
        var twoOrThreeOrFive = note.Patterns.Where(o => o.Length == 5).Select(o => o.ToCharArray()).ToList();
        var zeroOrSixOrNine = note.Patterns.Where(o => o.Length == 6).Select(o => o.ToCharArray()).ToList();

        //var segmentsCF = one;
        //var segmentsACF = seven;
        //var segmentsBCDF = four;
        //var segmentsABCDEFG = eight;

        var segmentsA = seven.Except(one);
        Assert.Single(segmentsA);

        var segmentsBD = four.Except(one);
        Assert.Equal(2, segmentsBD.Count());

        var segmentsABCDF = segmentsA.Union(one).Union(segmentsBD); // must be 9
        Assert.Equal(5, segmentsABCDF.Count());

        var nine = zeroOrSixOrNine.First(n => n.Union(segmentsABCDF).Count() == 6);
        Assert.Equal(6, nine.Count());
        zeroOrSixOrNine.Remove(nine);

        var zero = zeroOrSixOrNine.First(n => n.Intersect(one).Count() == 2); // six doesn't contain both segments of one
        Assert.Equal(6, zero.Count());
        zeroOrSixOrNine.Remove(zero);

        var six = zeroOrSixOrNine.First();
        Assert.Equal(6, six.Count());
        zeroOrSixOrNine.Remove(six);

        var segmentsD = eight.Except(zero);
        Assert.Single(segmentsD);
        var segmentsC = eight.Except(six);
        Assert.Single(segmentsC);
        var segmentsE = eight.Except(nine);
        Assert.Single(segmentsE);
        var segmentsB = segmentsBD.Except(segmentsD);
        Assert.Single(segmentsB);
        var segmentsG = nine.Except(four).Except(segmentsA);
        Assert.Single(segmentsG);
        var segmentsF = one.Except(segmentsC);

        var two = eight.Except(segmentsB).Except(segmentsF);
        Assert.Equal(5, two.Count());

        var five = six.Except(segmentsE);
        Assert.Equal(5, two.Count());

        var three = nine.Except(segmentsB);
        Assert.Equal(5, two.Count());

        return [
            new string(zero.Order().ToArray()),
            new string(one.Order().ToArray()),
            new string(two.Order().ToArray()),
            new string(three.Order().ToArray()),
            new string(four.Order().ToArray()),
            new string(five.Order().ToArray()),
            new string(six.Order().ToArray()),
            new string(seven.Order().ToArray()),
            new string(eight.Order().ToArray()),
            new string(nine.Order().ToArray())
            ];
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

    [Theory]
    [InlineData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf")]
    public void Day8_Part2_Example1(string note)
    {
        Assert.Equal(5353, Part2([note]));
    }

    [Fact]
    public void Day8_Part2_Example2()
    {
        Assert.Equal(61229, Part2(Input.Strings(@"day8example2.txt")));
    }

    class Note
    {
        public List<string> Patterns { get; private set; }
        public List<string> Outputs { get; private set; }

        public Note(List<string> patterns, List<string> outputs)
        {
            Patterns = patterns;
            Outputs = outputs;
        }
    }
}

