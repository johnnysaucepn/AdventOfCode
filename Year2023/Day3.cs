using Utilities;

namespace Year2023;

public class Day3
{
    public static int Part1(List<string> lines)
    {
        var height = lines.Count;
        var width = lines[0].Length;

        bool[,] include = new bool[width, height];
        //int[,] numbers = new int[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var entry = lines[y][x];

                if (!char.IsDigit(entry) && entry != '.')
                {
                    var left = (x < 0) ? 0 : x - 1;
                    var right = (x >= width - 1) ? width - 1 : x + 1;
                    var up = (y < 0) ? 0 : y - 1;
                    var down = (y >= height - 1) ? height - 1 : y + 1;
                    include[left, up] = true;
                    include[x, up] = true;
                    include[right, up] = true;
                    include[left, y] = true;
                    include[right, y] = true;
                    include[left, down] = true;
                    include[x, down] = true;
                    include[right, down] = true;
                }
            }
        }

        var includeMap = Format.DumpBooleanArray(include);
        Console.WriteLine(includeMap);
        //var numberMap = Format.DumpArray(numbers);
        //Console.WriteLine(numberMap);

        var total = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var entry = lines[y][x];
                var adjacent = false;
                if (char.IsDigit(entry))
                {
                    var extent = 0;
                    while ((x + extent <= width - 1) && char.IsDigit(lines[y][x + extent]))
                    {
                        if (include[x + extent, y] == true)
                        {
                            adjacent = true;
                        }
                        extent++;
                    }
                    var partNumberString = lines[y].Substring(x, extent);
                    var partNumber = int.Parse(partNumberString);
                    if (adjacent)
                    {
                        total += partNumber;
                    }
                    x += extent;
                }

            }
        }
        return total;
    }

    public static int Part2(List<string> lines)
    {

        return 0;
    }



    [Fact]
    public void Day3_Part1_Example1()
    {
        Assert.Equal(4361, Part1(Input.Strings(@"day3example1.txt")));
    }

    [Fact]
    public void Day3_Part2_Example2()
    {
        Assert.Fail();
        //Assert.Equal(0, Part2(Input.Strings(@"dayXexample2.txt")));
    }

}