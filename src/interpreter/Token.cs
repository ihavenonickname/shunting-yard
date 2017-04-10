using interpreter;

internal enum Token
{
    // Caution! Each token has a textual pattern. Some token's pattern can be
    // a substring of other's pattern. When it happens, the one with largest
    // pattern has to be placed BEFORE the other.
    // Example: Power lexeme is "**" and Multiplication's lexeme is "*",
    // therefore Power comes first.
    // Reason: If Multiplication came first, some expression with "**" would
    // be parsed as two multiplications instead of one power operation.

    [Pattern(@"-?\d+(\.\d+)?")]
    Number,

    [Pattern(@"\(")]
    LeftParenthesis,

    [Pattern(@"\)")]
    RightParenthesis,

    [Pattern(@"\*\*")]
    Power,

    [Pattern(@"\+")]
    Addition,

    [Pattern(@"\-")]
    Subtraction,

    [Pattern(@"\*")]
    Multiplication,

    [Pattern(@"/")]
    Division,

    [Pattern(@"==")]
    Equal,

    [Pattern(@"<>")]
    Different,

    [Pattern(@">=")]
    GreaterEqual,

    [Pattern(@"<=")]
    LessEqual,

    [Pattern(@"<")]
    Less,

    [Pattern(@">")]
    Greater,
}