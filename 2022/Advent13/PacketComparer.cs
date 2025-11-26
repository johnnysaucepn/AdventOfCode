public class PacketComparer : IComparer<Item>
{
    public bool? CustomCompare(Item left, Item right, int depth = 0)
    {
        if (left.IsSingle && right.IsSingle)
        {
            Console.WriteLine($"{Space(depth)}- Compare {left.Value.Value} vs {right.Value.Value}");
            if (left.Value.Value < right.Value.Value)
            {
                Console.WriteLine($"{Space(depth + 1)}- Left side is smaller, so inputs are in the right order");
                return true;
            }

            if (left.Value.Value > right.Value.Value)
            {
                Console.WriteLine($"{Space(depth + 1)}- Right side is smaller, so inputs are not in the right order");
                return false;
            }

            return null;
        }
        if (left.IsList && right.IsList)
        {
            var leftLength = left.Children.Count;
            var rightLength = right.Children.Count;

            Console.WriteLine($"{Space(depth)}- Compare {left} vs {right}");
            var smallest = Math.Min(leftLength, rightLength);
            for (var i = 0; i < smallest; i++)
            {
                var comparison = CustomCompare(left.Children[i], right.Children[i], depth + 1);
                if (comparison.HasValue) return comparison.Value;
            }
            if (leftLength < rightLength)
            {
                Console.WriteLine($"{Space(depth)}- Left side is smaller, so inputs are in the right order");
                return true;
            }
            if (leftLength > rightLength)
            {
                Console.WriteLine($"{Space(depth)}- Right side is smaller, so inputs are not in the right order");
                return false;
            }

            return null;
        }
        if (left.IsList && right.IsSingle)
        {
            Console.WriteLine($"{Space(depth)}- Mixed types; convert right to [{right.Value.Value}] and retry comparison");
            var rightList = new Item(new List<Item> { right });
            return CustomCompare(left, rightList, depth);
        }

        if (left.IsSingle && right.IsList)
        {
            Console.WriteLine($"{Space(depth)}- Mixed types; convert left to [{left.Value.Value}] and retry comparison");
            var leftList = new Item(new List<Item> { left });
            return CustomCompare(leftList, right, depth);
        }
        return null;
    }

    public int Compare(Item? x, Item? y)
    {
        return (CustomCompare(x, y) ?? false) ? -1 : +1;
    }

    private static string Space(int depth) => new string(Enumerable.Repeat(' ', depth).ToArray());
}