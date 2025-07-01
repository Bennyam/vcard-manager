using System.Collections.Generic;

namespace VCardManager.Core
{
  public interface IVCardConverter
  {
    string ToVCard(Contact contact);
    IEnumerable<Contact> FromVCard(string content);
  }
}