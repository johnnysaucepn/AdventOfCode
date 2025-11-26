var lines = File.ReadAllLines("input.txt");
var elves = new List<int>();
var total = 0;
var maxCalories = -1;
var maxElf = -1;
foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        if (total > maxCalories)
        {
            maxCalories = total;
            maxElf = elves.Count;
        }
        elves.Add(total);
        total = 0;
    }
    else
    {
        var calories = int.Parse(line);
        total+= calories;
    }
}

if (total > 0)
{
    elves.Add(total);
}

// Part 1
Console.WriteLine(maxCalories);

// Part 2
var sorted = elves.OrderByDescending(e=>e);
Console.WriteLine(sorted.Take(3).Sum(x=>x));
