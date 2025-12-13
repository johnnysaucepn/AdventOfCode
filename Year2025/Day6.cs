using Utilities;
using Xunit;

namespace Year2025;

public class Day6
{
    public long Part1(List<string> lines)
    {
        var questionCount = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Count();

        Grid<long> operands = ParseOperands(lines[..^1], questionCount);
        List<string> operators = ParseOperators(lines[^1], questionCount);

        long total = 0;
        for (int i = 0; i < questionCount; i++)
        {
            var action = operators[i];
            var answer = operands.GetAt(i, 0);
            for (int o = 1; o < operands.Height; o++)
            {
                var operand = operands.GetAt(i, o);
                if (action == "+")
                {
                    answer += operand;
                }
                else if (action == "*")
                {
                    answer *= operand;
                }
            }
            total += answer;
        }
        return total;
    }

    public long Part2(List<string> lines)
    {
        // Assume that test data is padded with spaces

        var grid = GridFactory.FromLinesAlpha(lines);
        var operatorRow = grid.Height - 1;
        var width = grid.Width;
        var startingX = grid.Width - 1;

        long total = 0;


        List<long> operands = [];
        for (long x = startingX; x >= 0; x--)
        {
            long operand = 0;
            for (int y = 0; y < operatorRow; y++)
            {
                var symbol = grid.GetAt(x, y);
                if (char.IsDigit(symbol))
                {
                    operand *= 10;
                    operand += long.Parse(string.Concat(symbol));
                }
            }
            operands.Add(operand);

            long answer = 0;

            var operatorSymbol = grid.GetAt(x, operatorRow);
            if (operatorSymbol == ' ')
            {
                continue;
            }

            if (operatorSymbol == '*')
            {
                answer = operands.Aggregate(1, (long a, long b) => a * b);
            }
            if (operatorSymbol == '+')
            {
                answer = operands.Sum();
            }
            total += answer;
            operands = [];
            x--; // skip the next column, will never have values
        }
        return total;

    }


    private List<string> ParseOperators(string list, int questions)
    {
        return list.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    private Grid<long> ParseOperands(List<string> list, int questions)
    {
        var operands = new Grid<long>(questions, list.Count);
        for (var y = 0; y < list.Count; y++)
        {
            var line = list[y].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            for (var x = 0; x < questions; x++)
            {
                operands.SetAt(x, y, int.Parse(line[x]));
            }
        }
        return operands;
    }




    [Fact]
    public void Day6_ParseOperands_Example()
    {
        var example = Input.Strings(@"day6example.txt");
        var questions = example[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Count();
        var operands = ParseOperands(example[..^1], questions);
        Assert.Equal(4, operands.Width);
        Assert.Equal(3, operands.Height);
    }

    [Fact]
    public void Day6_Part1_Example()
    {
        Assert.Equal(4277556, Part1(Input.Strings(@"day6example.txt")));
    }

    [Fact]
    public void Day6_Part2_Example()
    {
        Assert.Equal(3263827, Part2(Input.Strings(@"day6example.txt")));
    }

}
