using Utilities;

namespace Year2023
{
    public class Tests
    {
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