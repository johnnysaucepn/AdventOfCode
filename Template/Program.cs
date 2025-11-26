Part1("example.txt");
Part2("example.txt");

void Part1(string selectedInput)
{
    Console.WriteLine("----Part 1----");
    var lines = File.ReadAllLines(selectedInput).ToList();

    Console.WriteLine("----Part 1 Answer----");
}

void Part2(string selectedInput)
{
    Console.WriteLine("----Part 2----");
    var lines = File.ReadAllLines(selectedInput).ToList();

    Console.WriteLine("----Part 2 Answer----");
}