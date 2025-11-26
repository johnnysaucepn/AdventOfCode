public enum Material
{
    Air = 0,
    Sand,
    Rock,
    SandSpawner
}


public class Topography
{
    public Material[,] Space;

    public int Width;
    public int Height;

    public Point SandOrigin;

    public Topography(IEnumerable<Segment> segments, Point sandOrigin)
    {
        var minX = segments.Min(s => Math.Min(s.Start.X, s.End.X));
        var minY = segments.Min(s => Math.Min(s.Start.Y, s.End.Y));
        var maxX = segments.Max(s => Math.Max(s.Start.X, s.End.X));
        var maxY = segments.Max(s => Math.Max(s.Start.Y, s.End.Y));

        var tempWidth = (maxX - minX);
        var height = maxY + 3;
        var width = Math.Max(tempWidth, height) * 2;
        // Align the sand dropper to the centre of the space, as we know it will spread from here
        var horizOffset = (sandOrigin.X - (width / 2));

        Width = width;
        Height = height;
        Space = new Material[Width, Height];
        sandOrigin.X -= horizOffset;
        SandOrigin = sandOrigin;

        foreach (var segment in segments)
        {
            segment.Start.X -= horizOffset;
            segment.End.X -= horizOffset;
        }

        Space[sandOrigin.X, sandOrigin.Y] = Material.SandSpawner;

    }

    public void DrawRock(IEnumerable<Segment> segments)
    {
        foreach (var segment in segments)
        {
            var x = segment.Start.X;
            var y = segment.Start.Y;
            Space[x, y] = Material.Rock;
            do
            {
                x += segment.IncrementX;
                y += segment.IncrementY;
                Space[x, y] = Material.Rock;
            } while (x != segment.End.X || y != segment.End.Y);
        }
    }

    public void Dump()
    {
        var slice = 200;
        var centre = Width / 2;
        var left = centre - (slice/2);
        var right = left + slice;

        if (left < 0) left = 0;
        if (right >= Width) right = Width - 1;

        for (var y = 0; y < Height; y++)
        {
            for (var x = left; x < right; x++)
            {
                var cell = Space[x, y] switch
                {
                    Material.Air => '.',
                    Material.Rock => '#',
                    Material.Sand => 'o',
                    Material.SandSpawner => '+',
                    _ => ' '
                };
                Console.Write(cell);
            }
            Console.WriteLine();
        }
    }
}