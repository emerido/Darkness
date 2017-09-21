using System.Threading;
using System.Threading.Tasks;

namespace Darkness.Cqrs
{
    public interface IQueryBus
    {
        Task<TResult> Ask<TResult>(IQuery<TResult> query, CancellationToken token);
    }
}