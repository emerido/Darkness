using System.Threading;
using System.Threading.Tasks;

namespace Darkness.Cqrs
{
    
    public interface ICommandBus
    {

        void Handle(ICommand command);

        void Handle<TContext>(ICommand command, TContext context);
        
        Task HandleAsync(ICommand command, CancellationToken token = default(CancellationToken));

        Task HandleAsync<TContext>(ICommand command, TContext context, CancellationToken token = default(CancellationToken));

    }
    
}