using System;
using System.Linq;

namespace interpreter
{
    class Program
    {
        private static void DisplayUsage()
        {
            Console.WriteLine();
            Console.WriteLine("USAGE");
            Console.WriteLine("  interpreter <expression>");
            Console.WriteLine();
            Console.WriteLine("PARAMETERS");
            Console.WriteLine("  expression - Expression with real numbers, arithmetic operators and parentheses");
            Console.WriteLine();
            Console.WriteLine("OPERATORS");
            Console.WriteLine("  +  Addition");
            Console.WriteLine("  -  Subtraction");
            Console.WriteLine("  *  Multiplication");
            Console.WriteLine("  /  Division");
            Console.WriteLine("  ** Power");
            Console.WriteLine();
            Console.WriteLine("EXAMPLES");
            Console.WriteLine("  interpreter 1 + 2 * 3");
            Console.WriteLine("  interpreter (1 + 2) * 3");
            Console.WriteLine("  interpreter 5 ** 2 ** 3");
            Console.WriteLine("  interpreter (5 ** 2) ** 3");
            Console.WriteLine();
            Console.WriteLine("It obeys mathematical rules of precedence and associativity.");
        }

        static void Main(string[] args)
        {
            if (!args.Any() || args.Contains("/?"))
            {
                DisplayUsage();

                return;
            }

            var input = string.Join(" ", args);
            var parser = new ShuntingYard();
            var interpreter = new Interpreter();

            try
            {
                var answer = interpreter.Evaluate(parser.Parse(input));

                Console.WriteLine(answer);
            }
            catch (InterpreterException e)
            {
                Console.WriteLine(e.FormattedMessage);
            }
        }
    }
}
