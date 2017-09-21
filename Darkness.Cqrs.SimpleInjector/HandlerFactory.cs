using System;
using SimpleInjector;

namespace Darkness.Cqrs.SimpleInjector
{
    public class HandlerFactory : IQueryHandlerFactory, ICommandHandlerFactory
    {
        private Container Container { get; }

        public HandlerFactory(Container container) 
            => Container = container;

        public THandler CreateHandler<THandler>() where THandler : class 
            => Container.GetInstance<THandler>();

        public object CreateHandler(Type type) 
            => Container.GetInstance(type);
    }
}