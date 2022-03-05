using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBSInterpreter.Models.Exceptions
{
    public class CBSReaderException : Exception
    {
        public CBSReaderException(string? message) : base(message)
        { }

        public CBSReaderException(string? message, Exception? innerException) : base(message, innerException)
        { }
    }
}
