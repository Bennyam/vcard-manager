using VCardManager.Core;

namespace VCardManager.CLI
{
  internal class Program
  {
    static void Main()
    {
      IConsole console = new SystemConsole();
      IFileStore fileStore = new FileSystemStore();
      IVCardConverter converter = new VCardConverter();
      string filePath = "data/contacts.vcf";

      var service = new ContactService(fileStore, converter, filePath);
      var menu = new Menu(console, service);

      menu.Run();
    }
  }
}
