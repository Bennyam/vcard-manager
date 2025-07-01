

using VCardManager.Core;

namespace VCardManager.Tests;

public class ContactTests
{
  [Fact]
  public void Contact_Accepts_Properties()
  {
    var ben = new Contact("Ben", "Ameryckx", "+32 487308297", "ben@testmail.com");

    Assert.Equal("Ben", ben.FirstName);
    Assert.Equal("Ameryckx", ben.LastName);
    Assert.Equal("+32 487308297", ben.Phone);
    Assert.Equal("ben@testmail.com", ben.Email);
  }

  [Fact]
  public void FullName_Returns_FullName()
  {
    var lisa = new Contact("Lisa", "Martens", "+32 456879200", "lisa@testmail.com");
    var fullName = lisa.FullName;

    Assert.Equal("Lisa Martens", fullName);
  }
}

