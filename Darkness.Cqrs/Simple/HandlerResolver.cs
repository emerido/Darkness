using System;
using Darkness.Cqrs.Errors;

namespace Darkness.Cqrs.Simple
{
    public class HandlerResolver : ICommandHandlerResolver, IQueryHandlerResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public HandlerResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object Resolve(Type type)
        {
            return _serviceProvider.GetService(type) ?? throw new HandlerNotFound(type);
        }

        
    }
}