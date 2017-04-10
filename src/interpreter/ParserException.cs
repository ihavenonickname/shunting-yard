using System.Text;

namespace interpreter
{
    internal class ParserException : BaseException
    {
        internal ParserException(int position, string input)
        {
            var sb = new StringBuilder();
            var offset = new string(' ', position - 1);

            sb.AppendLine();
            sb.AppendLine("Error while parsing expression");
            sb.AppendLine("Character not recognized at position " + position);
            sb.AppendLine();
            sb.AppendLine(input);
            sb.AppendLine(offset + "^");

            message = sb.ToString();
        }
    }
}
