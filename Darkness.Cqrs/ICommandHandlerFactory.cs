using System;

namespace Darkness.Cqrs
{
    public interface ICommandHandlerFactory
    {
        object CreateHandler(Type type);
    }
}