using System;
using System.Text;

namespace interpreter
{
    internal class LexicalException : Exception
    {
        private readonly string message;

        internal string FormattedMessage
        {
            get
            {
                return message;
            }
        }

        internal LexicalException(int position, string input)
        {
            var sb = new StringBuilder();
            var offset = new string(' ', position - 1);

            sb.AppendLine();
            sb.AppendLine("Lexical error");
            sb.AppendLine($"Symbol not recognized at position {position}");
            sb.AppendLine();
            sb.AppendLine(input);
            sb.AppendLine($"{offset}^");

            message = sb.ToString();
        }
    }
}
