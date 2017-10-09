namespace Darkness.Linq.Pagination
{
    public class Paginal : IPaginal
    {
        public Paginal()
        {
        }

        public Paginal(int page, int take)
        {
            Page = page;
            Take = take;
        }

        public int Page { get; set; }
        
        public int Take { get; set; }
        
    }
}