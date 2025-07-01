using System.Collections.Generic;
using VCardManager.Core;

namespace VCardManager.Tests
{
    public class InMemoryFileStore : IFileStore
    {
        private readonly Dictionary<string, string> files = new();

        public bool Exists(string path)
        {
            return files.ContainsKey(path);
        }

        public string ReadAllText(string path)
        {
            return files.ContainsKey(path) ? files[path] : string.Empty;
        }

        public void WriteAllText(string path, string contents)
        {
            files[path] = contents;
        }

        public void AppendAllText(string path, string contents)
        {
            if (files.ContainsKey(path))
                files[path] += contents;
            else
                files[path] = contents;
        }
    }
}
