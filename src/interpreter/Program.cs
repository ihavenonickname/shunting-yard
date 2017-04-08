using System;
using System.Linq;

namespace interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!args.Any() || args.Contains("/?"))
            {
                Console.WriteLine("USAGE");
                Console.WriteLine("  interpreter <expression>");
                Console.WriteLine();
                Console.WriteLine("PARAMETERS");
                Console.WriteLine("  expression - Arithmetic expression with double numbers and operators");
                Console.WriteLine();
                Console.WriteLine("OPERATORS");
                Console.WriteLine("  +  Addition");
                Console.WriteLine("  -  Subtraction");
                Console.WriteLine("  ** Multiplication");
                Console.WriteLine("  /  Division");
                Console.WriteLine("  ^  Power");

                return;
            }

            var input = string.Join(" ", args);

            try
            {
                Console.WriteLine(new ShuntingYard().Interpret(input));
            }
            catch (InterpreterException e)
            {
                Console.WriteLine(e.FormattedMessage);
            }
        }
    }
}
