using System.Threading;
using System.Threading.Tasks;

namespace Darkness.Cqrs
{

    public interface ICommandHandler<in TCommand> 
        where TCommand : ICommand
    {

        void Handle(TCommand command);

    }
    
    public interface ICommandHandler<in TCommand, in TContext> 
        where TCommand : ICommand where TContext : class
    {

        void Handle(TCommand command, TContext context);

    }
    
    public interface ICommandHandlerAsync<in TCommand> 
        where TCommand : ICommand
    {

        Task Handle(TCommand command, CancellationToken cancellationToken);

    }
    
    public interface ICommandHandlerAsync<in TCommand, in TContext> 
        where TCommand : ICommand where TContext : class
    {

        Task Handle(TCommand command, TContext context, CancellationToken cancellationToken);

    }
    
}