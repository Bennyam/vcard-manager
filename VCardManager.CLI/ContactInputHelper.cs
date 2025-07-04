using System.Text.RegularExpressions;
using VCardManager.Core;

namespace VCardManager.CLI
{
    public class ContactInputHelper
    {
        private readonly IConsole console;

        public ContactInputHelper(IConsole console)
        {
            this.console = console;
        }

        public Contact PromptForContact()
        {
            console.Write("Voornaam: ");
            var firstName = console.ReadLine();

            console.Write("Achternaam: ");
            var lastName = console.ReadLine();

            var phone = PromptValidPhone();

            console.Write("Email: ");
            var email = console.ReadLine();

            return new Contact(firstName, lastName, phone, email);
        }

        private string PromptValidPhone()
        {
            while (true)
            {
                console.Write("Telefoon: ");
                var input = console.ReadLine();

                if (IsValidPhone(input))
                    return input;

                console.WriteLine("Ongeldig formaat. Probeer bv. 0487/11.22.33 of 0487112233.");
            }
        }

        private bool IsValidPhone(string input)
        {
            var pattern = @"^0?\d{3}([/.]?\d{2}){3,4}$|^0?\d{9,10}$";
            return Regex.IsMatch(input, pattern);
        }
    }
}