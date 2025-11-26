using System.Text.Json;

//Part1("input.txt");
Part2("input.txt");

void Part1(string selectedInput)
{
    Console.WriteLine("----Part 1----");
    var lines = File.ReadAllLines(selectedInput).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

    var sum = 0;

    for (var i = 0; i < lines.Count / 3; i += 1)
    {
        Console.WriteLine($"Pair {i + 1}: {lines[i * 3]} and {lines[i * 3 + 1]}");

        var left = Item.ParseLine(lines[i * 3]);
        var right = Item.ParseLine(lines[i * 3 + 1]);
        var comparer = new PacketComparer();
        var correct = comparer.CustomCompare(left, right) ?? true;
        Console.WriteLine(correct);

        if (correct)
        {
            Console.WriteLine(i + 1);
            sum += i + 1;
        }
    }

    Console.WriteLine("----Part 1 Answer----");
    Console.WriteLine(sum);
}

void Part2(string selectedInput)
{
    Console.WriteLine("----Part 2----");
    var lines = File.ReadAllLines(selectedInput).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

    var packets = lines.Select(l => Item.ParseLine(l)).ToList();

    packets.Add(new Item(new List<Item>{new Item(2)}, true));
    packets.Add(new Item(new List<Item>{new Item(6)}, true));

    var comparer = new PacketComparer();

    packets.Sort(comparer);

    foreach (var packet in packets)
    {
        Console.WriteLine(packet.ToString());
    }

    var decoderKey = 1;
    for (var i=0; i<packets.Count; i++)
    {
        if (packets[i].Divider)
        {
            decoderKey *= (i+1);
        }
    }

    Console.WriteLine("----Part 2 Answer----");
    Console.WriteLine(decoderKey);
}

