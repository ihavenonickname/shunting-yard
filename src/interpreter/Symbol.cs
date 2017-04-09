internal struct Symbol
{
    public readonly string Lexeme;
    public readonly Token Token;

    public Symbol(string lexeme, Token token)
    {
        Lexeme = lexeme;
        Token = token;
    }
}