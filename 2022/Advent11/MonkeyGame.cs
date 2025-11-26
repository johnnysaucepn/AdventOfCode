
using System.Text.RegularExpressions;
public class MonkeyGame
{
    public IList<Monkey> Monkeys = new List<Monkey>();

    public int MaxWorry = 0;

    public bool WithRelief = true;
    public bool Quiet = false;

    public MonkeyGame(IList<string> lines)
    {
        Monkeys = ParseMonkeys(lines).ToList();
        var maxWorry = 1;
        foreach (var monkey in Monkeys)
        {
            maxWorry *= monkey.Divisor;
        }
        MaxWorry = maxWorry;
    }

    public void PlayRound(int round)
    {
        foreach (var monkey in Monkeys)
        {
            monkey.TakeTurn();
        }
        Console.WriteLine($"After round {round}, the monkeys are holding items with these worry levels:");
        DumpMonkeys();
    }

    public IEnumerable<Monkey> ParseMonkeys(IList<string> lines)
    {
        var monkeyRegex = new Regex("^Monkey ([0-9]+):");
        var itemsRegex = new Regex("Starting items: (.+)$");
        var opRegex = new Regex(@"Operation: new = old ([\+\-\*]+) (\w+)");
        var divTestRegex = new Regex("Test: divisible by ([0-9]+)");

        var trueRegex = new Regex("If true: throw to monkey ([0-9]+)");
        var falseRegex = new Regex("If false: throw to monkey ([0-9]+)");

        for (var i = 0; i < lines.Count; i += 7)
        {
            var monkeyLine = lines[i];
            var itemsLine = lines[i + 1];
            var opLine = lines[i + 2];
            var testLine = lines[i + 3];
            var trueLine = lines[i + 4];
            var falseLine = lines[i + 5];

            // Plenty of formatting assumptions here!
            var monkeyNumber = Convert.ToInt32(monkeyRegex.Match(monkeyLine).Groups[1].Value);

            var itemsList = itemsRegex.Match(itemsLine).Groups[1].Value;
            var items = itemsList.Split(", ").Select(x => Convert.ToInt64(x));

            Func<long, (long, string)> op;
            var opMatch = opRegex.Match(opLine);
            if (opMatch.Success)
            {
                var operatorString = opMatch.Groups[1].Value;
                var operand = opMatch.Groups[2].Value;

                if (operatorString == "*")
                {
                    if (operand == "old")
                    {
                        op = w => (w * w, $"is multiplied by itself");
                    }
                    else
                    {
                        var multiplier = Convert.ToInt32(operand);
                        op = w => (w * multiplier, $"is multiplied by {multiplier}");
                    }

                }
                else if (operatorString == "+")
                {
                    var addor = Convert.ToInt32(operand);
                    op = w => (w + addor, $"increases by {addor}");
                }
                else
                {
                    throw new FormatException($"Unrecognised operation: {opLine}");
                }
            }
            else
            {
                throw new FormatException($"Unrecognised operation: {opLine}");
            }

            var divTestMatch = divTestRegex.Match(testLine);
            var divisor = Convert.ToInt32(divTestMatch.Groups[1].Value);

            var trueMatch = trueRegex.Match(trueLine);
            var ifTrue = Convert.ToInt32(trueMatch.Groups[1].Value);

            var falseMatch = falseRegex.Match(falseLine);
            var ifFalse = Convert.ToInt32(falseMatch.Groups[1].Value);

            yield return new Monkey(
                this,
                monkeyNumber,
                items,
                op,
                divisor,
                ifTrue,
                ifFalse
            );
        }
    }

    public void DumpMonkeys()
    {
        foreach (var monkey in Monkeys)
        {
            var itemList = string.Join(", ", monkey.Items.Select(x => x.Worry.ToString()));
            Console.WriteLine($"Monkey {monkey.Number}: {itemList}");
        }
    }

    public void DumpMonkeyActivity()
    {
        foreach (var monkey in Monkeys)
        {
            Console.WriteLine($"Monkey {monkey.Number} inspected items {monkey.Inspections} times.");
        }
    }
}