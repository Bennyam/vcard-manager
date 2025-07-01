using System.Collections.Generic;
using VCardManager.Core;

namespace VCardManager.Tests
{
    public class ConsoleSpy : IConsole
    {
        public List<string> Output = new();
        public Queue<string> Input = new();

        public void Write(string message)
        {
            Output.Add(message);
        }

        public void WriteLine(string message)
        {
            Output.Add(message);
        }

        public string ReadLine()
        {
            return Input.Count > 0 ? Input.Dequeue() : string.Empty;
        }

        public void Clear()
        {
            Output.Add("[Clear]");
        }

        public void ProvideInput(params string[] values)
        {
            foreach (var val in values)
                Input.Enqueue(val);
        }
    }
}
