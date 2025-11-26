Part1("input.txt");
Part2("input.txt");

void Part1(string selectedInput)
{
    Console.WriteLine("----Part 1----");
    // Exclude any accidental whitespace
    var lines = File.ReadAllLines(selectedInput).Where(x=>!string.IsNullOrWhiteSpace(x)).ToList();

    var (width, height) = GetDimensions(lines);
    var trees = BuildTreeMatrix(lines, width, height);

    DumpMatrix(trees, width, height);

    var visibility = CalculateVisibility(trees, width, height);

    Console.WriteLine("----Part 1 Answer----");
    Console.WriteLine(visibility);
}

void Part2(string selectedInput)
{
    Console.WriteLine("----Part 2----");
    // Exclude any accidental whitespace
    var lines = File.ReadAllLines(selectedInput).Where(x=>!string.IsNullOrWhiteSpace(x)).ToList();

    var (width, height) = GetDimensions(lines);
    var trees = BuildTreeMatrix(lines, width, height);

    DumpMatrix(trees, width, height);

    var maxScenicIndex = CalculateScenicIndices(trees, width, height);

    Console.WriteLine("----Part 2 Answer----");
    Console.WriteLine(maxScenicIndex);
}

void DumpMatrix(int[,] matrix, int width, int height)
{
    for (var y=0; y < height; y++)
    {
        for (var x=0; x < width; x++)
        {
            Console.Write(matrix[x,y]);
        }
        Console.WriteLine();
    }
}


(int width, int height) GetDimensions(IList<string> lines)
{
    var width = lines[0].Length;
    var height = lines.Count;
    return (width, height);
}

int[,] BuildTreeMatrix(IList<string> lines, int width, int height)
{
    var grid = new int[width, height];

    for (var x=0; x < width; x++)
    {
        for (var y=0; y < height; y++)
        {
            // Convert.ToInt32 on the char itself gives an ASCII code!
            grid[x,y] = int.Parse(lines[y][x].ToString());
        }
    }
    return grid;
}

int CalculateVisibility(int[,] matrix, int width, int height)
{
    int total = 0;
    for (var y=0; y < height; y++)
    {
        for (var x=0; x < width; x++)
        {
            if (BlockedFromLeft(matrix, width, height, x, y)
            && BlockedFromRight(matrix, width, height, x, y)
            && BlockedFromTop(matrix, width, height, x, y)
            && BlockedFromBottom(matrix, width, height, x, y))
            {
                Console.Write("X");
            }
            else
            {
                total++;
                Console.Write(".");
            }
        }
        Console.WriteLine();
    }
    return total;
}

int CalculateScenicIndices(int[,] matrix, int width, int height)
{
    int highest = 0;
    for (var y=0; y < height; y++)
    {
        for (var x=0; x < width; x++)
        {
            var visibleLeft = VisibleLeft(matrix, width, height, x, y);
            var visibleUp = VisibleUp(matrix, width, height, x, y);
            var visibleRight = VisibleRight(matrix, width, height, x, y);
            var visibleDown = VisibleDown(matrix, width, height, x, y);

            var scenicIndex = visibleLeft * visibleUp * visibleRight * visibleDown;
            if (scenicIndex > highest) highest = scenicIndex;
            Console.Write(scenicIndex);
        }
        Console.WriteLine();
    }
    return highest;
}

bool BlockedFromLeft(int[,] matrix, int width, int height, int x, int y)
{
    var thisTree = matrix[x, y];
    for (var i=0; i < x; i++)
    {
        if (matrix[i, y] >= thisTree)
        {
            return true;
        }
    }
    return false;
}

bool BlockedFromRight(int[,] matrix, int width, int height, int x, int y)
{
    var thisTree = matrix[x, y];
    for (var i = x + 1; i < width; i++)
    {
        if (matrix[i, y] >= thisTree)
        {
            return true;
        }
    }
    return false;
}

bool BlockedFromTop(int[,] matrix, int width, int height, int x, int y)
{
    var thisTree = matrix[x, y];
    for (var j = 0; j < y; j++)
    {
        if (matrix[x, j] >= thisTree)
        {
            return true;
        }
    }
    return false;
}
bool BlockedFromBottom(int[,] matrix, int width, int height, int x, int y)
{
    var thisTree = matrix[x, y];
    for (var j = y + 1; j < width; j++)
    {
        if (matrix[x, j] >= thisTree)
        {
            return true;
        }
    }
    return false;
}

int VisibleLeft(int[,] matrix, int width, int height, int x, int y)
{
    var thisTree = matrix[x, y];
    var count = 0;
    for (var i = x - 1; i >= 0; i--)
    {
        count++;
        if (matrix[i, y] >= thisTree)
        {
            break;
        }
    }
    return count;
}

int VisibleRight(int[,] matrix, int width, int height, int x, int y)
{
    var thisTree = matrix[x, y];
    var count = 0;
    for (var i = x + 1; i < width; i++)
    {
        count++;
        if (matrix[i, y] >= thisTree)
        {
            break;
        }
    }
    return count;
}

int VisibleUp(int[,] matrix, int width, int height, int x, int y)
{
    var thisTree = matrix[x, y];
    var count = 0;
    for (var j = y - 1; j >= 0; j--)
    {
        count++;
        if (matrix[x, j] >= thisTree)
        {
            break;
        }
    }
    return count;
}

int VisibleDown(int[,] matrix, int width, int height, int x, int y)
{
    var thisTree = matrix[x, y];
    var count = 0;
    for (var j = y + 1; j < height; j++)
    {
        count++;
        if (matrix[x, j] >= thisTree)
        {
            break;
        }
    }
    return count;
}
