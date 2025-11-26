var lines = (await File.ReadAllLinesAsync(@"input3.txt")).Where(x=>!string.IsNullOrWhiteSpace(x)).ToList();

var lineCount = lines.Count;
var len = lines.First().Length;

var oxygenStr = Whittle(lines, KeepTheMostCommon);
var co2Str = Whittle(lines, KeepTheLeastCommon);

var oxygen = Convert.ToInt32(oxygenStr, 2);
var co2 = Convert.ToInt32(co2Str, 2);
Console.WriteLine(oxygen);
Console.WriteLine(co2);
Console.WriteLine(oxygen * co2);


string Whittle(IEnumerable<string> source, Func<int, int, char> filter)
{
    for (var i=0; i<len; i++)
    {
        var ones = source.Count(x=>x[i] == '1');
        var zeroes = source.Count(x=>x[i] == '0');
        
        var keep = filter(ones, zeroes);
        //Console.WriteLine($"{ones} {zeroes} - keeping {keep}");
        
        source = source.Where(x=>x[i] == keep).ToList();
        
        if (source.Count() == 1) break;
    }
    return source.First();
}

char KeepTheMostCommon(int ones, int zeroes)
{
    return (ones >= zeroes ? '1' : '0');
}

char KeepTheLeastCommon(int ones, int zeroes)
{
    return (ones < zeroes ? '1' : '0');
}

