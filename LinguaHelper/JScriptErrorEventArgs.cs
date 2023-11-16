using System.Text.Json.Serialization;

namespace LinguaHelper
{
    public class JScriptErrorEventArgs
    {
        public string Message { get; set; }
        public string Name { get; set; }
        public string Stack { get; set; }

        [JsonConstructor]
        public JScriptErrorEventArgs(string message, string name, string stack)
        {
            Message = message;
            Name = name;
            Stack = stack;
        }
    }
}
