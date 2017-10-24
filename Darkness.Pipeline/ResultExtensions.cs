using System;

namespace Darkness.Pipeline
{
    public static class ResultExtensions
    {


        public static Result<TResult> Try<TValue, TResult>(this Result<TValue> result, Func<TValue, TResult> func)
        {
            try
            {
                return result.Apply(func);
            }
            catch (Exception e)
            {
                return Result<TResult>.Failure(e);
            }
        }
        
        public static Result<TValue> Maybe<TValue>(this Result<TValue> source, Func<TValue, TValue> handler)
        {
            return source.IsSuccess 
                ? handler(source.Value).AsResult() 
                : source;
        }
        
        public static Result<TValue> Maybe<TValue>(this Result<TValue> source, Func<TValue, Result<TValue>> handler)
        {
            return source.IsSuccess 
                ? handler(source.Value) 
                : source;
        }
        
        public static Result<TResult> Apply<TValue, TResult>(this Result<TValue> source, Func<TValue, TResult> func)
        {
            return func(source.Value).AsResult();
        }
        
        public static Result<TResult> Apply<TValue, TResult>(this Result<TValue> source, Func<TValue, Result<TResult>> func)
        {
            return func(source.Value);
        }
        
        public static Result<TValue> AsResult<TValue>(this TValue value)
        {
            return new Result<TValue>(value);
        }
        
    }
}