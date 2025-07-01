using System.Collections.Generic;
using System.Linq;

namespace VCardManager.Core
{
  public class ContactService
  {
    private readonly IFileStore fileStore;
    private readonly IVCardConverter converter;
    private readonly string filePath;

    public ContactService(IFileStore fileStore, IVCardConverter converter, string filePath)
    {
      this.fileStore = fileStore;
      this.converter = converter;
      this.filePath = filePath;
    }

    public IEnumerable<Contact> GetAll()
    {
      if (!fileStore.Exists(filePath)) return Enumerable.Empty<Contact>();

      var content = fileStore.ReadAllText(filePath);
      return converter.FromVCard(content);
    }

    public void Add(Contact contact)
    {
      var vcard = converter.ToVCard(contact);
      fileStore.AppendAllText(filePath, vcard + "\n");
    }

    public IEnumerable<Contact> SearchByName(string name)
    {
      return GetAll().Where(c => c.FullName.ToLower().Contains(name.ToLower()));
    }

    public void Delete(Contact contactToDelete)
    {
      var contacts = GetAll().ToList();
      var updated = contacts.Where(c => !(c.FirstName == contactToDelete.FirstName &&
                                          c.LastName == contactToDelete.LastName &&
                                          c.Email == contactToDelete.Email)).ToList();

      var newContent = string.Join("\n", updated.Select(converter.ToVCard));
      fileStore.WriteAllText(filePath, newContent);
    }

    public void Export(Contact contact, string exportPath)
    {
      var vcard = converter.ToVCard(contact);
      fileStore.WriteAllText(exportPath, vcard);
    }
  }
}