using System;

namespace interpreter
{
    class InterpreterException : Exception
    {
        protected string message;

        internal string FormattedMessage
        {
            get
            {
                return message;
            }
        }
    }
}
