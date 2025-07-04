using Microsoft.Extensions.DependencyInjection;
using VCardManager.Core;

namespace VCardManager.CLI
{
    internal class Program
    {
        static void Main()
        {
            var services = new ServiceCollection();

            services.AddTransient<IConsole, SystemConsole>();
            services.AddTransient<ContactInputHelper>();
            services.AddTransient<Menu>();

            services.AddSingleton<IFileStore, FileSystemStore>();
            services.AddSingleton<IVCardConverter, VCardConverter>();

            string filePath = "data/contacts.vcf";

            services.AddSingleton(provider =>
            {
                var fileStore = provider.GetRequiredService<IFileStore>();
                var converter = provider.GetRequiredService<IVCardConverter>();
                return new ContactService(fileStore, converter, filePath);
            });

            var serviceProvider = services.BuildServiceProvider();

            var menu = serviceProvider.GetRequiredService<Menu>();
            menu.Run();
        }
    }
}

