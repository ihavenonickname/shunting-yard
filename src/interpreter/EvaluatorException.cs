using System.Text;

namespace interpreter
{
    internal class EvaluatorException : BaseException
    {
        internal EvaluatorException(string message)
        {
            var sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine("Error while evaluating expression");
            sb.AppendLine(message);

            this.message = sb.ToString();
        }
    }
}
