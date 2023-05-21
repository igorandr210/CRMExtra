using System;

namespace Domain.Exceptions
{
    public class InvalidParameterException : Exception
    {
        
        public InvalidParameterException(string parameterName): base(parameterName)
        {
        }
    }
}
