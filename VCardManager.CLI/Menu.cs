using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using VCardManager.Core;

namespace VCardManager.CLI
{
  public class Menu
  {
    private readonly IConsole console;
    private readonly ContactService contactService;
    private readonly ContactInputHelper inputHelper;


    public Menu(IConsole console, ContactService contactService, ContactInputHelper inputHelper)
    {
      this.console = console;
      this.contactService = contactService;
      this.inputHelper = inputHelper;
    }

    public void Run()
    {
      while (true)
      {
        ShowMenu();
        var choice = console.ReadLine();
        if (choice == null) return;

        switch (choice)
        {
          case "1": ShowAll(); break;
          case "2": AddContact(); break;
          case "3": Search(); break;
          case "4": Delete(); break;
          case "5": Export(); break;
          case "0": return;
          default:
            console.WriteLine("Ongeldige keuze.");
            break;
        }

        console.WriteLine("\nDruk op Enter om verder te gaan...");
        if(console.ReadLine() == null) return;
        console.Clear();
      }
    }

    private void ShowMenu()
    {
      console.WriteLine("=== VCard Manager ===");
      console.WriteLine("1. Toon alle contacten");
      console.WriteLine("2. Voeg nieuw contact toe");
      console.WriteLine("3. Zoek contact op naam");
      console.WriteLine("4. Verwijder contact");
      console.WriteLine("5. Exporteer contact naar bestand");
      console.WriteLine("0. Afsluiten");
      console.Write("Keuze: ");
    }

    private void ShowAll()
    {
      var contacts = contactService.GetAll().ToList();

      if (!contacts.Any())
      {
        console.WriteLine("Geen contacten gevonden.");
        return;
      }

      foreach (var c in contacts)
      {
        console.WriteLine($"{c.FullName}: {c.Email}, {c.Phone}");
      }
    }

    private void AddContact()
    {
      var contact = inputHelper.PromptForContact();
      contactService.Add(contact);
      console.WriteLine("Contact toegevoegd.");
    }

    private void Search()
    {
      console.Write("Zoeken: ");
      var term = console.ReadLine();
      var matches = contactService.SearchByName(term).ToList();

      if (!matches.Any())
      {
        console.WriteLine("Geen overeenkomsten.");
        return;
      }

      foreach (var c in matches)
      {
        console.WriteLine($"- {c.FullName} ({c.Email}, {c.Phone})");
      }
    }

    private void Delete()
    {
      console.Write("Naam exact (bv. 'John Doe'): ");
      var name = console.ReadLine();
      var match = contactService.GetAll().FirstOrDefault(c => c.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));

      if (match == null)
      {
        console.WriteLine("Contact niet gevonden.");
        return;
      }

      contactService.Delete(match);
      console.WriteLine("Contact verwijderd.");
    }

    private void Export()
    {
      console.Write("Naam exact (bv. 'John Doe'): ");

      var name = console.ReadLine();
      var match = contactService.GetAll().FirstOrDefault(c => c.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));

      if (match == null)
      {
        console.WriteLine("Contact niet gevonden.");
        return;
      }

      console.Write("Bestandsnaam (bv. export.vcf): ");
      var exportPath = console.ReadLine();
      contactService.Export(match, exportPath);
      console.WriteLine("Contact geëxporteerd.");
    }
  }
}