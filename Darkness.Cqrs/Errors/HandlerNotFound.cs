using System;

namespace Darkness.Cqrs.Errors
{
    public class HandlerNotFound : Exception
    {
        public HandlerNotFound(Type actionType) : base($"Handler {actionType} not found")
        {
            
        }
    }
}