using System.Collections.Generic;

namespace Darkness.Linq.Pagination
{
    public class Paginated<TElement> : IPaginated<TElement>
    {
        
        public Paginated()
        {
        }

        public IPaginator Pager { get; set; }
        
        public IEnumerable<TElement> Items { get; set; }
        
    }

    public class Paginated<TItems, TExtra> : Paginated<TItems>, IPaginated<TItems, TExtra>
    {
        
        public Paginated(TExtra extra)
        {
            Extra = extra;
        }

        public TExtra Extra { get; set; }
        
    }
    
}