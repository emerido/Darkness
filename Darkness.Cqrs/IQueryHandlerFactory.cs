using System;

namespace Darkness.Cqrs
{
    
    public interface IQueryHandlerFactory
    {
        
        object CreateHandler(Type type);
        
    }
    
}