using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace interpreter
{
    using BinaryOperation = Func<double, double, object>;

    class Evaluator
    {
        private static readonly Dictionary<Token, BinaryOperation> operations = new Dictionary<Token, BinaryOperation>();

        static Evaluator()
        {
            operations.Add(Token.Addition, (left, right) => left + right);
            operations.Add(Token.Subtraction, (left, right) => left - right);
            operations.Add(Token.Multiplication, (left, right) => left * right);
            operations.Add(Token.Division, (left, right) => left / right);
            operations.Add(Token.Power, (left, right) => Math.Pow(left, right));
            operations.Add(Token.Equal, (left, right) => left == right);
            operations.Add(Token.Different, (left, right) => left != right);
            operations.Add(Token.Greater, (left, right) => left > right);
            operations.Add(Token.GreaterEqual, (left, right) => left >= right);
            operations.Add(Token.Less, (left, right) => left < right);
            operations.Add(Token.LessEqual, (left, right) => left <= right);
        }
        
        private void Update(Stack<object> results, Symbol symbol)
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
                right = (double) results.Pop();
                left = (double) results.Pop();
            }
            catch (InvalidOperationException)
            {
                throw new EvaluatorException("Too few operands");
            }
            catch (InvalidCastException)
            {
                throw new EvaluatorException("Cannot compare booleans");
            }

            results.Push(operations[symbol.Token](left, right));
        }

        internal object Evaluate(IEnumerable<Symbol> symbols)
        {
            var answer = symbols.Aggregate(new Stack<object>(), (results, symbol) =>
            {
                Update(results, symbol);

                return results;
            });
            
            if (answer.Count > 1)
            {
                throw new EvaluatorException("Too many operands");
            }

            return answer.Pop();
        }
    }
}
