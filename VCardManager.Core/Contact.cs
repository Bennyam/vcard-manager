namespace VCardManager.Core;

public class Contact
{
  public string FirstName { get; }
  public string LastName { get; }
  public string Phone { get; }
  public string Email { get; }

  public Contact(string firstName, string lastName, string phone, string email)
  {
    FirstName = firstName;
    LastName = lastName;
    Phone = phone;
    Email = email;
  }

  public string FullName => $"{FirstName} {LastName}";
}
