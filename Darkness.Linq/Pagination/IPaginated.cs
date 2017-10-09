using System.Collections.Generic;

namespace Darkness.Linq.Pagination
{
    
    public interface IPaginated<TItems>
    {
        
        IPaginator Pager { get; set; }
        
        IEnumerable<TItems> Items { get; set; }
        
    }

    public interface IPaginated<TItems, TExtra> : IPaginated<TItems>
    {
        
        TExtra Extra { get; set; }
        
    }
    
}