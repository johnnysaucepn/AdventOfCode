using Utilities;
using Xunit;

public class Template
{
    public long Part1(List<string> lines)
    {
        throw new NotImplementedException();
    }

    public long Part2(List<string> lines)
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void DayX_Part1_Example1()
    {
        Assert.Equal(0, Part1(Input.Strings(@"dayXexample1.txt")));
    }

    [Fact]
    public void DayX_Part2_Example2()
    {
        Assert.Equal(0, Part2(Input.Strings(@"dayXexample2.txt")));
    }

}
