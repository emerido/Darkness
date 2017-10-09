namespace Darkness.Linq.Pagination
{
    public interface IPaginator : IPaginal
    {
        
        int Total { get; set; }
        
        int Pages { get; set; }
        
    }
}