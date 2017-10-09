using System;

namespace Darkness.Linq.Pagination
{
    public class Paginator : Paginal, IPaginator
    {
        
        public Paginator(int page, int take, int total)
        {
            Page = page;
            Take = take;
            Total = total;
            Pages = (int) Math.Ceiling((float)total / take);
        }

        public int Total { get; set; }
        
        public int Pages { get; set; }
        
    }
}