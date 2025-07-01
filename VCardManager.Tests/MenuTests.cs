using System.Collections.Generic;
using VCardManager.Core;
using VCardManager.CLI;

namespace VCardManager.Tests;

public class MenuTests
{
  [Fact]
  public void Run_ShowsAllContacts_WhenChoiceIs1()
  {
    var spyConsole = new ConsoleSpy();
    var fileStore = new InMemoryFileStore();
    var converter = new VCardConverter();
    var filePath = "data/contacts.vcf";

    var service = new ContactService(fileStore, converter, filePath);
    service.Add(new Contact("Ben", "Ameryckx", "+32 0468299300", "ben@testmail.com"));

    spyConsole.ProvideInput("1", "", "0");

    var menu = new Menu(spyConsole, service);
    menu.Run();

    var output = string.Join("\n", spyConsole.Output);
    Assert.Contains("Ben Ameryckx", output);
    Assert.Contains("ben@testmail.com", output);
  }
}