namespace Darkness.Linq.Pagination
{
    
    /// <summary>
    /// Base interface for paginated requests 
    /// </summary>
    public interface IPaginal
    {
        
        int Page { get; }
        
        int Take { get; }
        
    }
}