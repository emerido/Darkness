using System.Threading;
using System.Threading.Tasks;

namespace Darkness.Cqrs
{

    public interface ICommandHandler<in TCommand> 
        where TCommand : ICommand
    {

        void Handle(TCommand command);

    }
    
    public interface ICommandHandler<in TCommand, out TResult> 
        where TCommand : ICommand<TResult>
    {

        TResult Handle(TCommand command);

    }
    
    public interface ICommandHandlerAsync<in TCommand> 
        where TCommand : ICommand
    {

        Task Handle(TCommand command, CancellationToken cancellationToken);

    }

    public interface ICommandHandlerAsync<in TCommand, TResult>
        where TCommand : ICommand<TResult>
    {

        Task<TResult> Handle(TCommand command, CancellationToken cancellationToken);

    }
    
}