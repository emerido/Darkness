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
        
        public TResult Handle<TResult>(ICommand<TResult> command) 
        {
            if (command == null) 
                throw new ArgumentNullException(nameof(command));
            
            var handler = Resolver.Resolve(typeof(ICommandHandler<,>)
                .MakeGenericType(command.GetType(), typeof(TResult)));

            return (TResult) Invoke(handler, command);
        }
        
        public Task HandleAsync(ICommand command, CancellationToken token = default(CancellationToken))
        {
            if (command == null) 
                throw new ArgumentNullException(nameof(command));
            
            var handler = Resolver.Resolve(typeof(ICommandHandlerAsync<>)
                .MakeGenericType(command.GetType()));

            return (Task) InvokeAsync(handler, command, token);
        }

        public Task<TResult> HandleAsync<TResult>(ICommand<TResult> command, CancellationToken token = default(CancellationToken))
        {
            if (command == null) 
                throw new ArgumentNullException(nameof(command));
            
            var handler = Resolver.Resolve(typeof(ICommandHandlerAsync<,>)
                .MakeGenericType(command.GetType(), typeof(TResult)));

            return (Task<TResult>) InvokeAsync(handler, command, token);
        }

        private object Invoke(object handler, object command)
        {
            return handler.GetType()
                .GetMethod("Handle", new[] {command.GetType()})
                .Invoke(handler, new object[] {command});

        }
        
        private object InvokeAsync(object handler, object command, CancellationToken token)
        {
            return handler.GetType()
                .GetMethod("Handle", new[] {command.GetType(), token.GetType()})
                .Invoke(handler, new object[] {command, token});

        }
        
    }
}