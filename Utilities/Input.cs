namespace Utilities;

public static class Input
{
    public static List<string> Strings(string filename)
    {
        var lines = File.ReadAllLines(filename)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();
        return lines;
    }

    public static List<List<string>> StringSets(string filename)
    {
        List<List<string>> allSets = [];
        List<string> currentSet = [];
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                allSets.Add(currentSet);
                currentSet = [];
            }
            else
            {
                currentSet.Add(line);
            }
        }
        // end of the file, flush the last set
        if (currentSet.Count > 0)
        {
            allSets.Add(currentSet);
            currentSet = [];
        }

        return allSets;
    }
}
