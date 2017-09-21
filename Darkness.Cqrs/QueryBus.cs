using System.Threading;
using System.Threading.Tasks;

namespace Darkness.Cqrs
{
    public class QueryBus : IQueryBus
    {
        private IQueryHandlerFactory Factory { get; }

        public QueryBus(IQueryHandlerFactory factory)
        {
            Factory = factory;
        }

        public Task<TResult> Ask<TResult>(IQuery<TResult> query, CancellationToken token = default(CancellationToken))
        {
            var queryType = query.GetType();

            var handler = Factory.CreateHandler(typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResult)));

            var method = handler.GetType().GetMethod("Handle", new[] {query.GetType(), token.GetType()});

            return (Task<TResult>) method.Invoke(handler, new object[] {query, token});
        }
    }
}