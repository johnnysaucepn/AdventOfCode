Part1("input.txt");
Part2("input.txt");

void Part1(string selectedInput)
{
    Console.WriteLine("----Part 1----");
    var lines = File.ReadAllLines(selectedInput).ToList();

    var monkeyGame = new MonkeyGame(lines);
    monkeyGame.WithRelief = true;

    for (var i = 0; i < 20; i++)
    {
        monkeyGame.PlayRound(i + 1);
    }

    monkeyGame.DumpMonkeyActivity();

    var activeMonkeys = monkeyGame.Monkeys.OrderByDescending(m => m.Inspections).Take(2).ToList();
    var monkeyBusiness = activeMonkeys[0].Inspections * activeMonkeys[1].Inspections;

    Console.WriteLine("----Part 1 Answer----");
    Console.WriteLine(monkeyBusiness);
}

void Part2(string selectedInput)
{
    Console.WriteLine("----Part 2----");
    var lines = File.ReadAllLines(selectedInput).ToList();

    var monkeyGame = new MonkeyGame(lines);
    monkeyGame.Quiet = true;
    monkeyGame.WithRelief = false;

    for (var i = 0; i < 10000; i++)
    {
        monkeyGame.PlayRound(i + 1);
    }

    monkeyGame.DumpMonkeyActivity();

    var activeMonkeys = monkeyGame.Monkeys.OrderByDescending(m => m.Inspections).Take(2).ToList();
    var monkeyBusiness = activeMonkeys[0].Inspections * activeMonkeys[1].Inspections;

    Console.WriteLine("----Part 2 Answer----");
    Console.WriteLine(monkeyBusiness);
}






