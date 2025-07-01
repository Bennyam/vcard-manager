using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VCardManager.Core
{
  public class VCardConverter : IVCardConverter
  {
    public string ToVCard(Contact contact)
    {
      return $@"BEGIN:VCARD
              FN:{contact.FirstName} {contact.LastName}
              TEL:{contact.Phone}
              EMAIL:{contact.Email}
              END:VCARD";
    }

    public IEnumerable<Contact> FromVCard(string content)
    {
      var lines = content.Split('\n')
                         .Select(line => line.Trim())
                         .Where(line => !string.IsNullOrWhiteSpace(line))
                         .ToList();

      var contacts = new List<Contact>();
      var buffer = new List<string>();

      foreach (var line in lines)
      {
        if (line == "BEGIN:VCARD")
        {
          buffer.Clear();
        }

        buffer.Add(line);

        if (line == "END:VCARD")
        {
          contacts.Add(ParseContact(buffer));
          buffer.Clear();
        }
      }

      return contacts;
    }

    private Contact ParseContact(List<string> vcardLines)
    {
      string firstName = "", lastName = "", phone = "", email = "";

      foreach (var line in vcardLines)
      {
        if (line.StartsWith("FN:"))
        {
          var names = line.Substring(3).Split(' ', 2);
          firstName = names[0];
          if (names.Length > 1) lastName = names[1];
        }
        else if (line.StartsWith("TEL:"))
        {
          phone = line.Substring(4);
        }
        else if (line.StartsWith("EMAIL:"))
        {
          email = line.Substring(6);
        }
      }

      return new Contact(firstName, lastName, phone, email);
    }
  }
}