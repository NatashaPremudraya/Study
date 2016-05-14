using System;

namespace Algebra
{
    public class ExpressionExtensionsException : Exception
    {
        public ExpressionExtensionsException(string msg) : base(msg, null) { }
        public ExpressionExtensionsException(string msg, Exception innerException) :
            base(msg, innerException)
        { }
    }
}