public class Item
{
    public string Label = Guid.NewGuid().ToString()[..8];

    public long Worry;
}