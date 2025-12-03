using Utilities;
using Xunit;

namespace Year2025
{
    public class Day2
    {
        public long Part1(List<string> lines)
        {
            var ranges = lines
                .SelectMany(l => l.Split(','))
                .Select(r =>
                {
                    var pair = r.Split('-');
                    return (start: long.Parse(pair[0]), end: long.Parse(pair[1]));
                });
            var invalids = ranges
                .SelectMany(p => FindInvalid(p.start, p.end, IsInvalidDouble));

            return invalids.Sum();
        }

        public int Part2(List<string> lines)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<long>? FindInvalid(long start, long end, Func<long, bool> checkFunction)
        {
            for (var i = start; i <= end; i++)
            {
                if (checkFunction(i)) yield return i;
            }
        }

        private bool IsInvalidDouble(long candidate)
        {
            var candidateString = candidate.ToString();
            if (candidateString.Length % 2 == 1) return false; // odd length cannot have two pairs;
            var half = candidateString.Length / 2;
            if (candidateString[..half] == candidateString[half..]) return true;
            return false;
        }

        private bool IsInvalidTuple(long candidate)
        {
            var candidateString = candidate.ToString();

            return false;
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
        [InlineData(11, 22, new long[] { 11, 22 })]
        public void Day2_FindInvalid_Double(long start, long end, long[] expected)
        {
            Assert.Equal(expected, FindInvalid(start, end, IsInvalidDouble));
        }

        [Fact]
        public void Day2_Part1_Example()
        {
            Assert.Equal(1227775554, Part1(Input.Strings(@"day2example.txt")));
        }

        [Fact]
        public void Day2_Part2_Example()
        {
            Assert.Equal(0, Part2(Input.Strings(@"day2example.txt")));
        }
    }
}
