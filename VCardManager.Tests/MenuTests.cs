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

    var service = new ContactService(fileStore, converter, "testdata.vcf");
    service.Add(new Contact("Ben", "Ameryckx", "0468/29.93.00", "ben@testmail.com"));

    spyConsole.ProvideInput("1", "", "0", "");
    
    var inputHelper = new ContactInputHelper(spyConsole);
    var menu = new Menu(spyConsole, service, inputHelper);
    menu.Run();
    

    var output = string.Join("\n", spyConsole.Output);
    Assert.Contains("Ben Ameryckx", output);
    Assert.Contains("ben@testmail.com", output);
  }
}