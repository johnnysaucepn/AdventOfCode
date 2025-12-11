using Utilities;
using Xunit;

namespace Year2025;

public class Day8
{
    public class Junction
    {
        public Coord3d Coord;
        public int Circuit;

        public Junction(Coord3d coord, int circuit)
        {
            Coord = coord;
            Circuit = circuit;
        }
    }

    public struct JunctionPair
    {
        public Junction First;
        public Junction Second;
        public double Distance;

        public JunctionPair(Junction first, Junction second, double distance)
        {
            First = first;
            Second = second;
            Distance = distance;
        }
    }

    public long Part1(List<string> lines, int iterations)
    {
        List<Junction> junctions = ParseJunctions(lines);

        var connections = GetAllConnections(junctions);

        foreach (var pair in connections)
        {
            Console.WriteLine($"{pair.First.Coord} => {pair.Second.Coord} = {pair.Distance}");
        }

        for (var i = 0; i < iterations; i++)
        {
            MakeConnection(junctions, connections, i);
        }

        var groups = junctions.GroupBy(j => j.Circuit).OrderByDescending(g => g.Count());

        foreach (var group in groups)
        {
            Console.WriteLine($"circuit {group.Key} = {group.Count()} junctions");
        }

        var total = groups.Take(3).Select(g => g.Count()).Aggregate((a, b) => a * b);

        return total;

    }

    public long Part2(List<string> lines)
    {
        List<Junction> junctions = ParseJunctions(lines);

        var connections = GetAllConnections(junctions);

        foreach (var pair in connections)
        {
            Console.WriteLine($"{pair.First.Coord} => {pair.Second.Coord} = {pair.Distance}");
        }

        var i = 0;
        JunctionPair? lastPair = null;
        while (lastPair is null)
        {
            MakeConnection(junctions, connections, i);

            var remainingCircuits = junctions.Select(j => j.Circuit).Distinct();
            if (remainingCircuits.Count() == 1)
            {
                lastPair = connections[i];
            }
            i++;
        }

        return lastPair.Value.First.Coord.X * lastPair.Value.Second.Coord.X;

    }


    private static void MakeConnection(List<Junction> junctions, List<JunctionPair> connections, int i)
    {
        var pair = connections[i];
        if (pair.First.Circuit != pair.Second.Circuit)
        {
            // Find all junctions with the same circuit as Second, and attach them all to First
            foreach (var affected in junctions.Where(j => j.Circuit == pair.Second.Circuit).ToList())
            {
                affected.Circuit = pair.First.Circuit;
                Console.WriteLine($"Connected {affected.Coord} to {affected.Circuit}");
            }
        }
    }

    private static List<Junction> ParseJunctions(List<string> lines)
    {
        var nextCircuit = 1;
        var junctions = lines
            .Select(Coord3d.Parse)
            .Select(c => new Junction(c, nextCircuit++))
            .ToList();
        return junctions;
    }


    public static List<JunctionPair> GetAllConnections(List<Junction> junctions)
    {
        List<JunctionPair> pairs = [];

        for (var i = 0; i < junctions.Count; i++)
        {
            var first = junctions[i];
            for (var j = i + 1; j < junctions.Count; j++)
            {
                var second = junctions[j];
                pairs.Add(new JunctionPair(first, second, first.Coord.DistanceTo(second.Coord)));
            }
        }
        return pairs.OrderBy(p => p.Distance).ToList();
    }

    [Fact]
    public void Day8_Part1_Example()
    {
        Assert.Equal(40, Part1(Input.Strings(@"day8example.txt"), 10));
    }

    [Fact]
    public void Day8_Part2_Example()
    {
        Assert.Equal(25272, Part2(Input.Strings(@"day8example.txt")));
    }

}