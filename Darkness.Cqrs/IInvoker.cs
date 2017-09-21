using System;

namespace Darkness.Cqrs
{
    public interface IInvoker
    {

        object Invoke(Type handlerType, Type actionType);
        
        object Invoke(Type handlerType, Type actionType, Type resultType);

    }
}