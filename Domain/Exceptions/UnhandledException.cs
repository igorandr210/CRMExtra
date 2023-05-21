using System;

namespace Domain.Exceptions
{
    public class UnhandledException:Exception
    {
        public UnhandledException(string message): base(message)
        {
            
        }
    }
}