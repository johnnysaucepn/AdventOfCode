public record struct Point
{
    public int X;
    public int Y;
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Segment
{
    public Point Start;
    public Point End;

    public int IncrementX = 0;
    public int IncrementY = 0;

    public Segment(int startX, int startY, int endX, int endY)
    {
        Start = new Point(startX, startY);
        End = new Point(endX, endY);

        if (Start.X > End.X) IncrementX = -1;
        if (Start.X < End.X) IncrementX = +1;
        if (Start.Y > End.Y) IncrementY = -1;
        if (Start.Y < End.Y) IncrementY = +1;
    }

    public static IEnumerable<Segment> Parse(string line)
    {
        var points = line.Split(" -> ");
        for (var i = 1; i < points.Count(); i++)
        {
            var from = points[i - 1].Split(',').Select(x => Convert.ToInt32(x)).ToList();
            var to = points[i].Split(',').Select(x => Convert.ToInt32(x)).ToList();

            yield return new Segment(from[0], from[1], to[0], to[1]);
        }

    }

}