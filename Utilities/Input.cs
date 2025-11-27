namespace Utilities
{
    public static class Input
    {
        public static List<string> Strings(string filename)
        {
            var lines = File.ReadAllLines(filename)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();
            return lines;
        }
    }
}
