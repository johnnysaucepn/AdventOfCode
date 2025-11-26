namespace Utilities
{
    public static class Input
    {
        public static List<string> Strings(string filename)
        {
            var lines = File.ReadAllLines(filename).ToList();
            return lines;
        }
    }
}
