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
            Console.WriteLine("  expression - Expression with real numbers, arithmetic operators, comparison operators and parentheses");
            Console.WriteLine();
            Console.WriteLine("ARITHMETIC OPERATORS");
            Console.WriteLine("  +  Addition");
            Console.WriteLine("  -  Subtraction");
            Console.WriteLine("  *  Multiplication");
            Console.WriteLine("  /  Division");
            Console.WriteLine("  ** Power");
            Console.WriteLine();
            Console.WriteLine("COMPARISON OPERATORS");
            Console.WriteLine("  == Equal");
            Console.WriteLine("  <> Different");
            Console.WriteLine("  >  Greater");
            Console.WriteLine("  >= Greater or equal");
            Console.WriteLine("  <  Less");
            Console.WriteLine("  <= Less or equal");
            Console.WriteLine();
            Console.WriteLine("EXAMPLES");
            Console.WriteLine("  interpreter 1 + 2 * 3");
            Console.WriteLine("  interpreter (1 + 2) * 3");
            Console.WriteLine("  interpreter 5 ** 2 ** 3");
            Console.WriteLine("  interpreter (5 ** 2) ** 3");
            Console.WriteLine("  interpreter \"1 + 2 * 3 < 9\"");
            Console.WriteLine("  interpreter (1 + 2) * 3 == 9");
            Console.WriteLine("  interpreter \"4 / 2 <> 2 / 4\"");
            Console.WriteLine();
            Console.WriteLine("It obeys mathematical rules of precedence and associativity.");
            Console.WriteLine();
            Console.WriteLine("The expression should be double-quoted when it contains \">\" or \"<\" characters, as they have special meaning in Windows CLI.");
        }

        static void Main(string[] args)
        {
            if (!args.Any() || args.Contains("/?"))
            {
                DisplayUsage();

                return;
            }

            var input = string.Join(" ", args);
            var parser = new Parser();
            var evaluator = new Evaluator();

            try
            {
                var answer = evaluator.Evaluate(parser.Parse(input));

                Console.WriteLine(answer);
            }
            catch (BaseException e)
            {
                Console.WriteLine(e.FormattedMessage);
            }
        }
    }
}
