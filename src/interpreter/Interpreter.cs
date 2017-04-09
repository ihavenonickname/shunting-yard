using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace interpreter
{
    using BinaryOperation = Func<double, double, double>;

    class Interpreter
    {
        private static readonly Dictionary<Token, BinaryOperation> operations = new Dictionary<Token, BinaryOperation>();

        static Interpreter()
        {
            operations.Add(Token.Addiction, (left, right) => left + right);
            operations.Add(Token.Subtraction, (left, right) => left - right);
            operations.Add(Token.Multiplication, (left, right) => left * right);
            operations.Add(Token.Division, (left, right) => left / right);
            operations.Add(Token.Power, Math.Pow);
        }
        
        private void Update(Stack<double> results, Symbol symbol)
        {
            if (symbol.Token == Token.Number)
            {
                var number = double.Parse(symbol.Lexeme, CultureInfo.InvariantCulture);
                results.Push(number);

                return;
            }

            double right;
            double left;

            try
            {
                right = results.Pop();
                left = results.Pop();
            }
            catch (InvalidOperationException)
            {
                throw new SyntaxException("Too few operands");
            }

            results.Push(operations[symbol.Token](left, right));
        }

        internal double Evaluate(IEnumerable<Symbol> symbols)
        {
            var answer = symbols.Aggregate(new Stack<double>(), (results, symbol) =>
            {
                Update(results, symbol);

                return results;
            });
            
            if (answer.Count > 1)
            {
                throw new SyntaxException("Too many operands");
            }

            return answer.Pop();
        }
    }
}
