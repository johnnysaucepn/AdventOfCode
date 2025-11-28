using Utilities;

namespace Year2023;

public class Day3
{
    public record Part
    {
        public char Symbol;
        public Coord Location;

        public Part(char symbol, Coord location)
        {
            Symbol = symbol;
            Location = location;
        }
    }

    public record PartNumber
    {
        public int Value;
        public List<Part> LinkedParts = [];
    }

    public static int Part1(List<string> lines)
    {
        var grid = Grid<char>.FromLines(lines);

        List<PartNumber> partsFound = FindParts(lines, grid);
        foreach (var part in partsFound)
        {
            Console.WriteLine($"{part.Value}, {part.LinkedParts.Count}");
        }
        var total = partsFound
            .Where(p => p.LinkedParts.Count > 0)
            .Sum(p => p.Value);

        return total;
    }

    public static int Part2(List<string> lines)
    {
        var grid = Grid<char>.FromLines(lines);

        List<PartNumber> partsFound = FindParts(lines, grid);

        Dictionary<Part, List<int>> partCount = [];

        foreach (var partNumber in partsFound)
        {
            foreach (var part in partNumber.LinkedParts)
            {
                if (!partCount.ContainsKey(part)) { partCount[part] = []; }
                partCount[part].Add(partNumber.Value);
            }
        }

        var total = partCount
            .Where(c => c.Key.Symbol == '*' && c.Value.Count == 2)
            .Select(c =>
            {
                var product = 1;
                foreach (var factor in c.Value)
                {
                    product *= factor;
                }
                return product;
            }).Sum();

        return total;
    }


    private static List<PartNumber> FindParts(List<string> lines, Grid<char> grid)
    {
        List<PartNumber> partsFound = [];

        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width;)
            {
                var entry = grid.GetAt(x, y);

                if (char.IsDigit(entry))
                {
                    var extent = 0;
                    List<Part> candidateParts = [];
                    var finished = false;
                    while (!finished)
                    {
                        var scanPoint = new Coord(x + extent, y);
                        if (!grid.InBounds(scanPoint) || !char.IsDigit(grid.GetAt(scanPoint)))
                        {
                            finished = true;
                        }
                        else
                        {
                            candidateParts.AddRange(FindConnectedParts(grid, scanPoint));
                            extent++;
                        }
                    }
                    var partNumberString = string.Concat(grid.GetSequence(x, y, extent));
                    var partNumber = int.Parse(partNumberString);

                    partsFound.Add(new PartNumber() { Value = partNumber, LinkedParts = candidateParts.Distinct().ToList() });

                    x += extent;
                }
                else
                {
                    x++;
                }

            }
        }

        return partsFound;
    }

    public static List<Part> FindConnectedParts(Grid<char> grid, Coord here)
    {
        List<Part> partsFound = [];

        var left = new Coord(here.X - 1, here.Y);
        var topLeft = new Coord(here.X - 1, here.Y - 1);
        var top = new Coord(here.X, here.Y - 1);
        var topRight = new Coord(here.X + 1, here.Y - 1);
        var right = new Coord(here.X + 1, here.Y);
        var bottomRight = new Coord(here.X + 1, here.Y + 1);
        var bottom = new Coord(here.X, here.Y + 1);
        var bottomLeft = new Coord(here.X - 1, here.Y + 1);

        List<Coord> coordList = [left, topLeft, top, topRight, right, bottomRight, bottom, bottomLeft];

        foreach (var coord in coordList)
        {
            if (grid.InBounds(coord))
            {
                var entry = grid.GetAt(coord);
                if (IsPartSymbol(entry))
                {
                    var part = new Part(entry, coord);
                    partsFound.Add(part);
                }
            }
        }
        return partsFound.Distinct().ToList();
    }

    public static bool IsPartSymbol(char test)
    {
        return test != '.' && !char.IsDigit(test);
    }

    [Fact]
    public void Day3_Part1_Example1()
    {
        Assert.Equal(4361, Part1(Input.Strings(@"day3example.txt")));
    }

    [Fact]
    public void Day3_Part2_Example2()
    {
        Assert.Equal(467835, Part2(Input.Strings(@"day3example.txt")));
    }

}