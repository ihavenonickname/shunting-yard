using System;

namespace interpreter
{
    class BaseException : Exception
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
