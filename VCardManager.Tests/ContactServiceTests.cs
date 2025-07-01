using System.Linq;
using VCardManager.Core;

namespace VCardManager.Tests;

public class ContactServiceTests
{
  private const string TestFile = "data/contacts.vcf";

  private ContactService CreateService(InMemoryFileStore store, IVCardConverter? converter = null)
  {
    return new ContactService(store, converter ?? new VCardConverter(), TestFile);
  }

  private Contact SampleContact() => new Contact("Ben", "Ameryckx", "+32 453789006", "ben@testmail.com");

  [Fact]
  public void GetAll_Returns_Empty_IfNotExists()
  {
    var store = new InMemoryFileStore();
    var service = CreateService(store);
    var result = service.GetAll();

    Assert.Empty(result);
  }

  [Fact]
  public void Add_ShouldStoreContact_AsVCard()
  {
      var store = new InMemoryFileStore();
      var service = CreateService(store);
      var contact = SampleContact();

      service.Add(contact);

      var content = store.ReadAllText(TestFile);
      Assert.Contains("BEGIN:VCARD", content);
      Assert.Contains("FN:Ben Ameryckx", content);
  }

  [Fact]
  public void SearchByName_ShouldReturnMatchingContact()
  {
      var store = new InMemoryFileStore();
      var service = CreateService(store);
      service.Add(SampleContact());

      var result = service.SearchByName("ben");

      Assert.Single(result);
      Assert.Equal("Ben", result.First().FirstName);
  }

  [Fact]
  public void Delete_ShouldRemoveMatchingContact()
  {
      var store = new InMemoryFileStore();
      var service = CreateService(store);
      var contact = SampleContact();

      service.Add(contact);
      service.Delete(contact);

      var remaining = service.GetAll();
      Assert.Empty(remaining);
  }

  [Fact]
  public void Export_ShouldWriteContactToExportPath()
  {
      var store = new InMemoryFileStore();
      var service = CreateService(store);
      var contact = SampleContact();

      service.Export(contact, "export.vcf");

      var result = store.ReadAllText("export.vcf");
      Assert.Contains("EMAIL:ben@testmail.com", result);
  }
}