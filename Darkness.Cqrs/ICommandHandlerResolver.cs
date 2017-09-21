using System;

namespace Darkness.Cqrs
{
    public interface ICommandHandlerResolver
    {
        object Resolve(Type type);
    }
}