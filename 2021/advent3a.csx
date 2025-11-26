var lines = (await File.ReadAllLinesAsync(@"input3.txt")).Where(x=>!string.IsNullOrWhiteSpace(x)).ToList();
var numbers = lines.Select(x=>Convert.ToInt32(x,2)).ToList();

var gamma = 0;
var epsilon = 0;
var lineCount = lines.Count;
var len = lines.First().Length;
for (var i=0; i<len; i++)
{
    var unit = (int)Math.Pow(2, i);
    var ones = numbers.Count(x=>(x & unit) > 0);
    var zeros = lineCount - ones;
    
    if (ones >zeros)
    {
        gamma += unit;
    }
    if (ones < zeros)
    {
        epsilon += unit;
    }
    Console.WriteLine($"{i} {unit} {zeros} {ones}");
        
    
}

Console.WriteLine(gamma);
Console.WriteLine(epsilon);
Console.WriteLine(gamma * epsilon);
