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

        public Task Handle(ICommand command, CancellationToken token = default(CancellationToken))
        {
            var handler = Resolver.Resolve(typeof(ICommandHandler<>).MakeGenericType(command.GetType()));

            return (Task) Invoke(handler, command, token);
        }

        public Task<TResult> Handle<TResult>(ICommand<TResult> command, CancellationToken token = default(CancellationToken))
        {
            var handler = Resolver.Resolve(typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult)));

            return (Task<TResult>) Invoke(handler, command, token);
        }

        private object Invoke(object handler, object command, CancellationToken token)
        {
            return handler.GetType()
                .GetMethod("Handle", new[] {command.GetType(), token.GetType()})
                .Invoke(handler, new object[] {command, token});

        }
        
    }
}