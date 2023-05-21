using System;

namespace Domain.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string id): base($"Entity with id:{id} was not found.")
        {
            
        }
    }
}