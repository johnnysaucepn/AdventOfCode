using System.Text.Json;

public class Item
{
    public List<Item>? Children;
    public int? Value;

    public bool Divider;

    public bool IsSingle => Value.HasValue;
    public bool IsList => !Value.HasValue;

    public Item(int value, bool divider = false)
    {
        Value = value;
        Children = null;
        Divider = divider;
    }

    public Item(List<Item> children, bool divider = false)
    {
        Value = null;
        Children = children;
        Divider = divider;
    }

    public override string ToString()
    {
        if (IsList)
        {
            var childrenString = string.Join(',', Children.Select(x => x.ToString()));
            return $"[{childrenString}]";
        }
        else
        {
            return Value.Value.ToString();
        }
    }

    public static Item ParseLine(string line)
    {
        var root = JsonDocument.Parse(line).RootElement;
        return ParseSegment(root);
    }

    public static Item ParseSegment(JsonElement element, int depth = 0)
    {
        Console.WriteLine($"{Space(depth)}{element.ToString()}");
        if (element.ValueKind == JsonValueKind.Array)
        {
            var children = element.EnumerateArray().Select(x => ParseSegment(x, depth + 1)).ToList();
            return new Item(children);
        }
        else
        {
            return new Item(element.GetInt32());
        }
    }

    private static string Space(int depth) => new string(Enumerable.Repeat(' ', depth).ToArray());

}

