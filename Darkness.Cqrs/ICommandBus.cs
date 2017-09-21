using System.Threading;
using System.Threading.Tasks;

namespace Darkness.Cqrs
{
    
    public interface ICommandBus
    {

        Task Handle(ICommand command, CancellationToken token = default(CancellationToken));

        Task<TResult> Handle<TResult>(ICommand<TResult> command, CancellationToken token = default(CancellationToken));

    }
    
}