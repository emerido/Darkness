using System;
using SimpleInjector;

namespace Darkness.Cqrs.SimpleInjector
{
    public class HandlerResolver : IQueryHandlerResolver, ICommandHandlerResolver
    {
        private Container Container { get; }

        public HandlerResolver(Container container) 
            => Container = container;

        public object Resolve(Type type) 
            => Container.GetInstance(type);
    }
}