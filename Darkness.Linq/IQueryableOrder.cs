using System.Linq;

namespace Darkness.Linq
{
    public interface IQueryableOrder<T> where T : class
    {

        IOrderedQueryable<T> Apply(IQueryable<T> query);

    }
}