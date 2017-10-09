using System;
using System.Linq;
using System.Threading.Tasks;
using Darkness.Linq.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Darkness.Linq.Extensions
{
    public static class QueryableExtensions
    {
        
        public static IQueryable<T> Apply<T>(this IQueryable<T> query, IQueryableFilter<T> filter) where T : class 
            => filter.Apply(query);
        
        public static IQueryable<T> Where<T>(this IQueryable<T> query, IQueryableFilter<T> filter) where T : class 
            => filter.Apply(query);
        
        public static IOrderedQueryable<T> Apply<T>(this IQueryable<T> query, IQueryableOrder<T> order) where T : class 
            => order.Apply(query);
     
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, IQueryableOrder<T> order) where T : class 
            => order.Apply(query);
        
    }
}