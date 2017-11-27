using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Darkness.Cqrs.Errors;

namespace Darkness.Cqrs.Simple
{
    public class CommandBus : ICommandBus
    {
        
        private ICommandHandlerResolver Resolver { get; }

        public CommandBus(ICommandHandlerResolver resolver)
        {
            Resolver = resolver;
        }
        
        public void Handle(ICommand command)
        {
            if (command == null) 
                throw new ArgumentNullException(nameof(command));
            
            var handlerType = typeof(ICommandHandler<>)
                .MakeGenericType(command.GetType());

            Invoke(handlerType, command);
        }

        public void Handle<TContext>(ICommand command, TContext context)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var handlerType = typeof(ICommandHandler<,>)
                .MakeGenericType(command.GetType(), typeof(TContext));
            
            Invoke(handlerType, command, context);
        }

        public Task HandleAsync(ICommand command, CancellationToken token = default(CancellationToken))
        {
            if (command == null) 
                throw new ArgumentNullException(nameof(command));
            
            var handlerType = typeof(ICommandHandlerAsync<>)
                .MakeGenericType(command.GetType());

            return (Task) InvokeAsync(handlerType, command, token);

        }

        public Task HandleAsync<TContext>(ICommand command, TContext context, CancellationToken token = default(CancellationToken))
        {
            if (command == null) 
                throw new ArgumentNullException(nameof(command));
            
            var handlerType = typeof(ICommandHandlerAsync<,>)
                .MakeGenericType(command.GetType(), typeof(TContext));

            return (Task) InvokeAsync(handlerType, command, context, token);

        }

        private void Invoke(Type handlerType, params object[] args)
        {
            var handler = Resolver.Resolve(handlerType) ?? throw new HandlerNotFound(handlerType);
            
            try
            {
                handler.GetType()
                    .GetMethod("Handle", args.Select(x => x.GetType()).ToArray())
                    .Invoke(handler, args);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
        
        private object InvokeAsync(Type handlerType, params object[] args)
        {
            var handler = Resolver.Resolve(handlerType) ?? throw new HandlerNotFound(handlerType);

            try
            {
                return handler.GetType()
                    .GetMethod("Handle", args.Select(x => x.GetType()).ToArray())
                    .Invoke(handler, args);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
        
    }
}