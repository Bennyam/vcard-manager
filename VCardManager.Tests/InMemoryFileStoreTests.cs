using VCardManager.Core;
using Xunit;

namespace VCardManager.Tests
{
    public class InMemoryFileStoreTests
    {
        private readonly IFileStore fileStore = new InMemoryFileStore();
        private readonly string testPath = "test.txt";

        [Fact]
        public void WriteAllText_ShouldStoreContent()
        {
            fileStore.WriteAllText(testPath, "Hello");

            Assert.True(fileStore.Exists(testPath));
            Assert.Equal("Hello", fileStore.ReadAllText(testPath));
        }

        [Fact]
        public void AppendAllText_ShouldAddToExistingContent()
        {
            fileStore.WriteAllText(testPath, "Hello");
            fileStore.AppendAllText(testPath, " World");

            Assert.Equal("Hello World", fileStore.ReadAllText(testPath));
        }

        [Fact]
        public void ReadAllText_ShouldReturnEmptyString_WhenFileDoesNotExist()
        {
            var content = fileStore.ReadAllText("nonexistent.txt");
            Assert.Equal(string.Empty, content);
        }
    }
}
