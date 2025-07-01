using System.Linq;
using VCardManager.Core;

namespace VCardManager.Tests;

public class VCardConverterTests
{
  private readonly IVCardConverter converter = new VCardConverter();

  [Fact]
  public void ToVcard_Returns_VCardString()
  {
    var ben = new Contact("Ben", "Ameryckx", "+32 456789907", "ben@testmail.com");
    var result = converter.ToVCard(ben);

    Assert.Contains("BEGIN:VCARD", result);
    Assert.Contains("FN:Ben Ameryckx", result);
    Assert.Contains("TEL:+32 456789907", result);
    Assert.Contains("EMAIL:ben@testmail.com", result);
    Assert.Contains("END:VCARD", result);
  }

  [Fact]
  public void FromVCard_Returns_Correct_Contact()
  {
    var input = @"BEGIN:VCARD
                  FN:Ben Ameryckx
                  TEL:+32 456789907
                  EMAIL:ben@testmail.com
                  END:VCARD";

    var contacts = converter.FromVCard(input).ToList();

    Assert.Single(contacts);
    var contact = contacts[0];
    Assert.Equal("Ben", contact.FirstName);
    Assert.Equal("Ameryckx", contact.LastName);
    Assert.Equal("+32 456789907", contact.Phone);
    Assert.Equal("ben@testmail.com", contact.Email);
  }
}