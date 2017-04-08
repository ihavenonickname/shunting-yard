using System.Text;

namespace interpreter
{
    internal class SyntaxException : InterpreterException
    {
        internal SyntaxException(string message)
        {
            var sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine("Syntax error");
            sb.AppendLine(message);

            this.message = sb.ToString();
        }
    }
}
