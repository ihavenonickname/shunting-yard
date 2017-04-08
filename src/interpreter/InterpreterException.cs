using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
