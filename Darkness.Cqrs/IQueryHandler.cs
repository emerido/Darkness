using System.Threading;
using System.Threading.Tasks;

namespace Darkness.Cqrs
{
    
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query, CancellationToken token = default(CancellationToken));
    }
    
}