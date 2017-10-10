using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [Range(1, int.MaxValue)]
        [Required]
        public int Page { get; set; }
        
        [Range(1, int.MaxValue)]
        [DefaultValue(10)]
        public int Take { get; set; }
        
    }
}