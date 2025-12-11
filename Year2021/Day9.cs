using Utilities;
using Xunit;

namespace Year2021
{
    public class Day9
    {
        private Grid<int> grid;

        public int Part1(List<string> lines)
        {
            grid = GridFactory.FromLinesNumeric(lines);
            var lowest = FindLowest().ToList();
            return lowest.Sum(x => grid.GetAt(x) + 1);
        }

        public int Part2(List<string> lines)
        {
            grid = GridFactory.FromLinesNumeric(lines);
            var lowest = FindLowest().ToList();
            var basins = lowest
                .Select(l => FindBasin(l).Distinct()).ToList();

            var topThree = basins
                .Select(b => b.Count())
                .OrderDescending()
                .Take(3);

            return topThree.Aggregate((a, b) => a * b);
        }

        private List<Coord> FindBasin(Coord start)
        {
            var openStack = new Stack<Coord>();
            var visited = new Grid<bool>(grid.Width, grid.Height);
            var found = new List<Coord>();

            openStack.Push(start);
            while (openStack.Any())
            {
                var node = openStack.Pop();
                if (!visited.GetAt(node))
                {
                    visited.SetAt(node, true);
                    if (grid.GetAt(node) < 9)
                    {
                        found.Add(node);
                        if (grid.InBounds(node.Up())) openStack.Push(node.Up());
                        if (grid.InBounds(node.Right())) openStack.Push(node.Right());
                        if (grid.InBounds(node.Down())) openStack.Push(node.Down());
                        if (grid.InBounds(node.Left())) openStack.Push(node.Left());
                    }

                }
            }
            return found;
        }

        private IEnumerable<Coord> FindLowest()
        {
            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    var here = new Coord(x, y);
                    if (IsLowest(here, grid.GetAt(here))) yield return here;
                }
            }
        }

        private bool IsLowest(Coord here, int consideration)
        {
            if (grid.InBounds(here.Up()) && grid.GetAt(here.Up()) <= consideration) return false;
            if (grid.InBounds(here.Down()) && grid.GetAt(here.Down()) <= consideration) return false;
            if (grid.InBounds(here.Left()) && grid.GetAt(here.Left()) <= consideration) return false;
            if (grid.InBounds(here.Right()) && grid.GetAt(here.Right()) <= consideration) return false;
            return true;
        }


        [Fact]
        public void Day9_Part1_Example()
        {
            Assert.Equal(15, new Day9().Part1(Input.Strings(@"day9example.txt")));
        }

        [Fact]
        public void Day9_Part2_Example()
        {
            Assert.Equal(1134, new Day9().Part2(Input.Strings(@"day9example.txt")));
        }

    }
}
