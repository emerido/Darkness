using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Darkness.Cqrs.Errors;

namespace Darkness.Cqrs.Simple
{
    public class QueryBus : IQueryBus
    {
        
        private IQueryHandlerResolver Resolver { get; }

        public QueryBus(IQueryHandlerResolver resolver)
        {
            Resolver = resolver;
        }

        public TResult Ask<TResult>(IQuery<TResult> query)
        {
            if (query == null) 
                throw new ArgumentNullException(nameof(query));

            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(TResult));

            return Invoke<TResult>(handlerType, query);
        }

        public TResult Ask<TResult, TContext>(IQuery<TResult> query, TContext context)
        {
            if (query == null) 
                throw new ArgumentNullException(nameof(query));

            var handlerType = typeof(IQueryHandler<,,>)
                .MakeGenericType(query.GetType(), typeof(TResult), typeof(TContext));

            return Invoke<TResult>(handlerType, query, context);
        }

        public Task<TResult> AskAsync<TResult>(IQuery<TResult> query, CancellationToken token = default(CancellationToken))
        {
            if (query == null) 
                throw new ArgumentNullException(nameof(query));

            var handlerType = typeof(IQueryHandlerAsync<,>)
                .MakeGenericType(query.GetType(), typeof(TResult));

            return Invoke<Task<TResult>>(handlerType, query, token);
        }

        public Task<TResult> AskAsync<TResult, TContext>(IQuery<TResult> query, TContext context, CancellationToken token = default(CancellationToken))
        {
            if (query == null) 
                throw new ArgumentNullException(nameof(query));
            
            var handlerType = typeof(IQueryHandlerAsync<,,>)
                .MakeGenericType(query.GetType(), typeof(TResult), typeof(TContext));

            return Invoke<Task<TResult>>(handlerType, query, context, token);
        }

        private TResult Invoke<TResult>(Type handlerType, params object[] args)
        {
            var handler = Resolver.Resolve(handlerType) ?? throw new HandlerNotFound(handlerType);

            var handlerMethod = handler.GetType().GetMethod("Handle", args.Select(x => x.GetType()).ToArray());

            return (TResult) handlerMethod.Invoke(handler, args);
        }

    }
}