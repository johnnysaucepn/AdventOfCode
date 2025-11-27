using System.Text;

namespace Utilities
{
    public static class Output
    {
        public static string DumpBooleanArray(bool[,] array, char falseChar = '.', char trueChar = 'O')
        {
            var sb = new StringBuilder();
            for (int y = 0; y < array.GetLength(1); y++)
            {
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    sb.Append(array[x, y] ? trueChar : falseChar);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static string DumpArray<T>(T[,] array)
        {
            var sb = new StringBuilder();
            for (int y = 0; y < array.GetLength(1); y++)
            {
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    sb.Append(array[x, y]);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
