using System.Threading;
using System.Threading.Tasks;

namespace Darkness.Cqrs
{
    
    public interface ICommandBus
    {

        void Handle(ICommand command);

        TResult Handle<TResult>(ICommand<TResult> command);
        
        Task HandleAsync(ICommand command, CancellationToken token = default(CancellationToken));

        Task<TResult> HandleAsync<TResult>(ICommand<TResult> command, CancellationToken token = default(CancellationToken));

    }
    
}