using System.Threading;
using System.Threading.Tasks;

namespace Darkness.Cqrs
{
    
    public interface IQueryHandler<in TQuery, out TResult> 
        where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
    
    public interface IQueryHandler<in TQuery, out TResult, in TContext> 
        where TQuery : IQuery<TResult> where TContext : class
    {
        TResult Handle(TQuery query);
    }
    
    public interface IQueryHandlerAsync<in TQuery, TResult> 
        where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query, CancellationToken token = default(CancellationToken));
    }
    
    public interface IQueryHandlerAsync<in TQuery, TResult, in TContext> 
        where TQuery : IQuery<TResult> where TContext : class
    {
        Task<TResult> Handle(TQuery query, TContext context, CancellationToken token = default(CancellationToken));
    }
    
}