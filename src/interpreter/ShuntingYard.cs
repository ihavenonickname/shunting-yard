using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace interpreter
{
    internal class ShuntingYard
    {
        private enum Associativity
        {
            Left,
            Right
        }

        internal enum Token
        {
            Number,
            Addiction,
            Subtraction,
            Multiplication,
            Division,
            Power,
            LeftParentheses,
            RightParentheses
        }

        internal struct Symbol
        {
            public readonly string Lexeme;
            public readonly Token Token;
            public readonly int Position;

            public Symbol(string lexeme, Token token, int position)
            {
                Lexeme = lexeme;
                Token = token;
                Position = position;
            }
        }

        private static readonly Dictionary<Token, int> precedence = new Dictionary<Token, int>();
        private static readonly Dictionary<Token, Associativity> associativity = new Dictionary<Token, Associativity>();
        private static readonly Dictionary<Token, Regex> patterns = new Dictionary<Token, Regex>();

        static ShuntingYard()
        {
            precedence.Add(Token.LeftParentheses, 0);
            precedence.Add(Token.RightParentheses, 0);
            precedence.Add(Token.Addiction, 1);
            precedence.Add(Token.Subtraction, 1);
            precedence.Add(Token.Multiplication, 2);
            precedence.Add(Token.Division, 2);
            precedence.Add(Token.Power, 3);

            associativity.Add(Token.Addiction, Associativity.Left);
            associativity.Add(Token.Subtraction, Associativity.Left);
            associativity.Add(Token.Multiplication, Associativity.Left);
            associativity.Add(Token.Division, Associativity.Left);
            associativity.Add(Token.Power, Associativity.Right);

            Func<string, Regex> p = str => new Regex($"^{str}");

            patterns.Add(Token.Number, p(@"-?\d+(\.\d+)?"));
            patterns.Add(Token.LeftParentheses, p(@"\("));
            patterns.Add(Token.RightParentheses, p(@"\)"));
            patterns.Add(Token.Addiction, p(@"\+"));
            patterns.Add(Token.Subtraction, p(@"-"));
            patterns.Add(Token.Multiplication, p(@"\*"));
            patterns.Add(Token.Division, p(@"/"));
            patterns.Add(Token.Power, p(@"\^"));
        }

        private List<Symbol> Symbolize(string input)
        {
            var symbols = new List<Symbol>();
            var inputTrimmed = input.Trim();

            while (inputTrimmed.Length > 0)
            {
                var previousCount = symbols.Count;
                var position = input.Length - inputTrimmed.Length + 1;

                foreach (var pair in patterns)
                {
                    var m = pair.Value.Match(inputTrimmed);

                    if (m.Success)
                    {
                        symbols.Add(new Symbol(m.Value, pair.Key, position));
                        inputTrimmed = inputTrimmed.Substring(m.Value.Length);

                        break;
                    }
                }

                if (previousCount == symbols.Count)
                {
                    
                    throw new LexicalException(position, input);
                }

                inputTrimmed = inputTrimmed.Trim();
            }

            return symbols;
        }

        private bool HasHigherPrecedenceOrIsRight(Stack<Symbol> stack, Symbol symbol)
        {
            var token = symbol.Token;
            var topToken = stack.Peek().Token;

            var isHigher = precedence[token] > precedence[topToken];
            var isEqual = precedence[token] == precedence[topToken];
            var isRight = associativity[token] == Associativity.Right;

            return isHigher || (isEqual && isRight);
        }

        private void Shunt(Stack<Symbol> operators, Stack<Symbol> output, Symbol symbol)
        {
            var token = symbol.Token;

            if (token == Token.Number)
            {
                output.Push(symbol);
            }
            else if (token == Token.LeftParentheses)
            {
                operators.Push(symbol);
            }
            else if (token == Token.RightParentheses)
            {
                while (operators.Peek().Token != Token.LeftParentheses)
                {
                    output.Push(operators.Pop());
                }

                operators.Pop();
            }
            else if (!operators.Any() || operators.Peek().Token == Token.LeftParentheses)
            {
                operators.Push(symbol);
            }
            else if (HasHigherPrecedenceOrIsRight(operators, symbol))
            {
                operators.Push(symbol);
            }
            else
            {
                while (operators.Any() && !HasHigherPrecedenceOrIsRight(operators, symbol))
                {
                    output.Push(operators.Pop());
                }

                operators.Push(symbol);
            }
        }

        private List<Symbol> Parse(string input)
        {
            var operators = new Stack<Symbol>();
            var output = new Stack<Symbol>();

            foreach (var symbol in Symbolize(input))
            {
                Shunt(operators, output, symbol);
            }

            return output.Reverse().Concat(operators).ToList();
        }

        public double Interpret(string input)
        {
            var result = new Stack<double>();

            foreach (var symbol in Parse(input))
            {
                var token = symbol.Token;

                if (token == Token.Number)
                {
                    result.Push(double.Parse(symbol.Lexeme, CultureInfo.InvariantCulture));
                    continue;
                }
                
                if (result.Count < 2)
                {
                    throw new SyntaxException("Too many operators");
                }

                var right = result.Pop();
                var left = result.Pop();
                
                switch (token)
                {
                    case Token.Addiction:
                        result.Push(left + right);
                        break;

                    case Token.Subtraction:
                        result.Push(left - right);
                        break;

                    case Token.Multiplication:
                        result.Push(left * right);
                        break;

                    case Token.Division:
                        result.Push(left / right);
                        break;

                    case Token.Power:
                        result.Push(Math.Pow(left, right));
                        break;
                }
            }
            
            if (result.Count > 1)
            {
                throw new SyntaxException("Too many operands");
            }

            return result.Pop();
        }
    }
}
