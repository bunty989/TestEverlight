
namespace EverlightRadiology.Framework.Wrapper
{
    internal class KeyValueFileReader
    {
        private readonly Dictionary<string, string> _dictionary;


        public KeyValueFileReader(string keyValFile)
        {
            var readFile = File.ReadAllLines(
                    Path.Combine(TestConstant.PathVariables.GetBaseDirectory, keyValFile))
                .Where(x => (!string.IsNullOrWhiteSpace(x)) && (!x.Substring(0, 1).Equals("#")))
                .ToArray();
            var pairs = readFile.Select(l => new { Line = l, Pos = l.IndexOf("=", StringComparison.Ordinal) });
            // Build a dictionary of key/value pairs by splitting the string at the = sign
            _dictionary = pairs.ToDictionary(p => p.Line.Substring(0, p.Pos), 
                p => p.Line.Substring(p.Pos + 1));
        }

        public string GetValueOfKey(string key)
        {
            return _dictionary[key];
        }

        public static string ReadInWholeFile(string path) =>
            File.ReadAllText(
                Path.Combine(TestConstant.PathVariables.GetBaseDirectory, path));

        // ReSharper disable once UnusedMember.Global
        public static void WriteWholeFile(string path, string content)
        {
            File.WriteAllText(
                Path.Combine(TestConstant.PathVariables.GetBaseDirectory, path), content);
        }
    }
}
