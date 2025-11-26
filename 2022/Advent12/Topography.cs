public class Topography
{
    public int Width;
    public int Height;

    public int StartX = 0;
    public int StartY = 0;

    public int EndX = 0;
    public int EndY = 0;

    public int[,] Elevations;
    public Topography(IList<string> lines)
    {
        Width = lines[0].Length;
        Height = lines.Count;

        Elevations = new int[Width, Height];

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var code = lines[y][x];
                int elevation;
                if (code == 'S')
                {
                    StartX = x;
                    StartY = y;
                    elevation = 1;
                }
                else if (code == 'E')
                {
                    EndX = x;
                    EndY = y;
                    elevation = 26;
                }
                else
                {
                    elevation = code - 'a' + 1;
                }
                Elevations[x, y] = elevation;
            }
        }

    }

}

