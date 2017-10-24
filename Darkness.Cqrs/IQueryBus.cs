using System.Threading;
using System.Threading.Tasks;

namespace Darkness.Cqrs
{
    public interface IQueryBus
    {
        
        TResult Ask<TResult>(IQuery<TResult> query);
        
        TResult Ask<TResult, TContext>(IQuery<TResult> query, TContext context);
        
        Task<TResult> AskAsync<TResult>(IQuery<TResult> query, CancellationToken token = default(CancellationToken));
        
        Task<TResult> AskAsync<TResult, TContext>(IQuery<TResult> query, TContext context, CancellationToken token = default(CancellationToken));
        
    }
}