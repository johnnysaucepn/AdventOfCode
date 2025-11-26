
Part1();
Part2();

void Part1()
{
    var stream = File.ReadAllText("input.txt");
    Console.WriteLine(ScanForMarker(stream, 4));
}

void Part2()
{
    var stream = File.ReadAllText("input.txt");
    Console.WriteLine(ScanForMarker(stream, 14));  
}

int ScanForMarker(string stream, int length)
{
    // Init with dummy values
    var buffer = new char[length];
    for (var i=0; i<stream.Length; i++)
    {
        buffer[i % length] = stream[i];
        if (IsMarker(buffer, length))
        {
            return i+1;
        }
    }
    return -1;
}

bool IsMarker(IEnumerable<char> buffer, int length)
{
    //Console.WriteLine(new string(buffer.ToArray()));
    var unique = buffer.Where(c => c != default).Distinct().Count();
    
    return (unique == length);
}