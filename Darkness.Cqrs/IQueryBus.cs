using System.Threading;
using System.Threading.Tasks;

namespace Darkness.Cqrs
{
    public interface IQueryBus
    {
        
        TResult Ask<TResult>(IQuery<TResult> query);
        
        Task<TResult> AskAsync<TResult>(IQuery<TResult> query, CancellationToken token = default(CancellationToken));
        
    }
}