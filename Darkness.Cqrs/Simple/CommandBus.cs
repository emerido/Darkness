using System;
using System.Threading;
using System.Threading.Tasks;

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
            
            var handler = Resolver.Resolve(typeof(ICommandHandler<>)
                .MakeGenericType(command.GetType()));

            Invoke(handler, command);
        }

        public void Handle<TContext>(ICommand command, TContext context)
        {
            if (command == null) 
                throw new ArgumentNullException(nameof(command));

            var handler = Resolver.Resolve(typeof(ICommandHandler<,>)
                .MakeGenericType(command.GetType(), typeof(TContext)));
            
            Invoke(handler, command);
        }

        public Task HandleAsync(ICommand command, CancellationToken token = default(CancellationToken))
        {
            if (command == null) 
                throw new ArgumentNullException(nameof(command));
            
            var handler = Resolver.Resolve(typeof(ICommandHandlerAsync<>)
                .MakeGenericType(command.GetType()));

            return (Task) InvokeAsync(handler, command, token);

        }

        public Task HandleAsync<TContext>(ICommand command, TContext context, CancellationToken token = default(CancellationToken))
        {
            if (command == null) 
                throw new ArgumentNullException(nameof(command));
            
            var handler = Resolver.Resolve(typeof(ICommandHandlerAsync<,>)
                .MakeGenericType(command.GetType(), typeof(TContext)));

            return (Task) InvokeAsync(handler, command, token);

        }


        private static void Invoke(object handler, object command)
        {
            if (handler == null)
            {
                throw new ArgumentException("Handler not found");
            }
            handler.GetType()
                .GetMethod("Handle", new[] {command.GetType()})
                .Invoke(handler, new[] {command});

        }
        
        private static object InvokeAsync(object handler, object command, CancellationToken token)
        {
            return handler.GetType()
                .GetMethod("Handle", new[] {command.GetType(), token.GetType()})
                .Invoke(handler, new[] {command, token});

        }
        
    }
}