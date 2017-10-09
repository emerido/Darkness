using System;
using System.Linq;
using System.Threading.Tasks;
using Darkness.Linq.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Darkness.Linq.Extensions
{
    public static class PaginationExtensions
    {

        public static int Skip(this IPaginal paginal) 
            => paginal.Take * (paginal.Page - 1);
        
        public static IPaginal Validate(this IPaginal paginal)
        {
            if (paginal.Page < 1)
                throw new ArgumentException("Page parameter must be great than 0");

            if (paginal.Take < 1)
                throw new ArgumentException("Take parameter must be great than 0");
            
            return paginal;
        }
        
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, IPaginal paginal)
        {
            paginal.Validate();
            
            return query.Skip(paginal.Skip()).Take(paginal.Take);   
        }

        
        public static TPager PaginateTo<TPager, TElement>(this IQueryable<TElement> query, IPaginal paginal)
            where TPager : IPaginated<TElement>, new()
            => Paginate(query, paginal, new TPager());
        
        public static TPager PaginateTo<TPager, TElement>(this IQueryable<TElement> query, IPaginal paginal, TPager pager)
            where TPager : IPaginated<TElement>
            => Paginate(query, paginal, pager);

        public static async Task<TPager> PaginateToAsync<TPager, TElement>(this IQueryable<TElement> query, IPaginal paginal)
            where TPager : IPaginated<TElement>, new()
            => await PaginateAsync(query, paginal, new TPager());


        public static async Task<TPager> PaginateToAsync<TPager, TElement>(this IQueryable<TElement> query, TPager pager, IPaginal paginal)
            where TPager : IPaginated<TElement>
            => await PaginateAsync(query, paginal, pager);


        #region Private

        private static TPager Paginate<TPager, TElement>(IQueryable<TElement> query, IPaginal paginal, TPager pager)
            where TPager : IPaginated<TElement>
        {
            pager.Pager = new Paginator(paginal.Page, paginal.Take, query.Count());
            pager.Items = query.Paginate(paginal).ToList();

            return pager;
        }
        
        private static async Task<TPager> PaginateAsync<TPager, TElement>(IQueryable<TElement> query, IPaginal paginal, TPager pager)
            where TPager : IPaginated<TElement>
        {
            pager.Pager = new Paginator(paginal.Page, paginal.Take, await query.CountAsync());
            pager.Items = await query.Paginate(paginal).ToListAsync();

            return pager;
        }

        #endregion

    }
}