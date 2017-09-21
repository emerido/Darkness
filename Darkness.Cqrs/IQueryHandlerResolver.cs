using System;

namespace Darkness.Cqrs
{
    
    public interface IQueryHandlerResolver
    {
        
        object Resolve(Type type);
        
    }
    
}