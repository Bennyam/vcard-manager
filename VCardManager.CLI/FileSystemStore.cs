using System.IO;
using VCardManager.Core;

namespace VCardManager.CLI
{
  public class FileSystemStore : IFileStore
  {
    public bool Exists(string path)
    {
      return File.Exists(path);
    }

    public string ReadAllText(string path)
    {
      return File.ReadAllText(path);
    }

    public void WriteAllText(string path, string contents)
    {
      File.WriteAllText(path, contents);
    }

    public void AppendAllText(string path, string contents)
    {
      File.AppendAllText(path, contents);
    }
  }
}