using Utilities;

namespace Year2023
{
    public class Day1
    {
        public static int Part1(List<string> lines)
        {
            var answer = lines
                .Select(ExtractDigits)
                .Select(FirstAndLastDigits)
                .Sum();

            return answer;
        }

        public static int Part2(List<string> lines)
        {
            var answer = lines
                .Select(ExtractAlphas)
                .Select(FirstAndLastDigits)
                .Sum();

            return answer;
        }

        private static List<int> ExtractDigits(string line)
        {
            List<int> values = [];
            for (var i = 0; i < line.Length; i++)
            {
                if (int.TryParse(line[i].ToString(), out int parsed))
                {
                    values.Add(parsed);
                }
            }
            return values;
        }

        private static List<int> ExtractAlphas(string line)
        {
            var swaps = new Dictionary<string, int>
            {
                ["one"] = 1,
                ["two"] = 2,
                ["three"] = 3,
                ["four"] = 4,
                ["five"] = 5,
                ["six"] = 6,
                ["seven"] = 7,
                ["eight"] = 8,
                ["nine"] = 9
            };

            List<int> values = [];
            for (var i = 0; i < line.Length; i++)
            {
                foreach (var key in swaps.Keys)
                {
                    if (line.IndexOf(key, i) == i)
                    {
                        values.Add(swaps[key]);
                    }
                }
                if (int.TryParse(line[i].ToString(), out int parsed))
                {
                    values.Add(parsed);
                }
            }
            Console.WriteLine(string.Join(", ", values));
            return values;

        }

        private static int FirstAndLastDigits(List<int> numbers)
        {
            var first = numbers.First();
            var last = numbers.Last();

            return (first * 10) + last;
        }

        [Fact]
        public void Day1_Part1_Example1()
        {
            Assert.Equal(142, Day1.Part1(Input.Strings(@"day1example1.txt")));
        }

        [Fact]
        public void Day1_Part2_Example2()
        {
            Assert.Equal(281, Day1.Part2(Input.Strings(@"day1example2.txt")));
        }
    }
}
