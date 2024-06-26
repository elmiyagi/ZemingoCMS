namespace ZemingoCMS.Application.Models
{
    public class PagedParams
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public PagedParams()
        {
            PageSize = 0;
            PageSize = 10;
        }

        public PagedParams(int page, int pageSize)
        {
            if (page < 0)
                Page = 0;
            else
                Page = page;

            if (pageSize < 0 || pageSize > 1000)
                PageSize = 10;
            else
                PageSize = pageSize;
        }
    }
}
