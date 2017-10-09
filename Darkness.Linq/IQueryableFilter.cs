using System.Linq;

namespace Darkness.Linq
{
    public interface IQueryableFilter<T> where T : class
    {
        
        IQueryable<T> Apply(IQueryable<T> query);
        
    }
}