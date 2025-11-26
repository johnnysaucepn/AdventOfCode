public class Monkey
{
    public MonkeyGame Game;
    public int Number;
    public Queue<Item> Items = new Queue<Item>();
    public long Inspections = 0;
    public Func<long, (long, string)> Operation = w => (w, "NA");
    public int Divisor = 0;
    public int TrueMonkey = -1;
    public int FalseMonkey = -1;

    public Monkey(
        MonkeyGame game,
        int number,
        IEnumerable<long> items,
        Func<long, (long, string)> operation,
        int divisor,
        int trueMonkey,
        int falseMonkey
    )
    {
        Game = game;
        Number = number;
        Items = new Queue<Item>(items.Select(x => new Item { Worry = x }));
        Operation = operation;
        Divisor = divisor;
        TrueMonkey = trueMonkey;
        FalseMonkey = falseMonkey;
    }

    public bool HasItems() => Items.Count() > 0;

    public void TakeTurn()
    {
        Console.WriteLine($"Monkey {Number}:");
        while (Items.Any())
        {
            InspectNextItem();
        }
    }

    public void InspectNextItem()
    {
        var item = Items.Dequeue();
        if (!Game.Quiet) Console.WriteLine($"  Monkey inspects an item with a worry level of {item.Worry}.");
        (item.Worry, var inspectMsg) = Operation(item.Worry);
        if (!Game.Quiet) Console.WriteLine($"    Worry level {inspectMsg} to {item.Worry}.");
        if (Game.WithRelief)
        {
            item.Worry = GetBored(item.Worry);
            if (!Game.Quiet) Console.WriteLine($"    Monkey gets bored with item. Worry level is divided by 3 to {item.Worry}.");
        }
        else
        {
            item.Worry = item.Worry % Game.MaxWorry;
        }
        var isTrue = item.Worry % Divisor == 0;
        if (!Game.Quiet) Console.WriteLine($"    Current worry level is {(isTrue ? "" : "not ")}divisible by {Divisor}.");
        var targetMonkey = isTrue ? TrueMonkey : FalseMonkey;
        ThrowItem(item, targetMonkey);
        if (!Game.Quiet) Console.WriteLine($"    Item with worry level {item.Worry} is thrown to monkey {targetMonkey}.");

        Inspections++;
    }

    public long GetBored(long original)
    {
        return (long)Math.Floor(original / 3.0);
    }

    public void ThrowItem(Item item, int targetMonkeyNumber)
    {
        var monkey = Game.Monkeys[targetMonkeyNumber];
        monkey.Items.Enqueue(item);
    }

    public void ShowStartingItems()
    {
        var itemList = string.Join(", ", Items);
        Console.WriteLine($"Starting items: {itemList}");
    }


}
