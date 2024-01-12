

namespace ProductService.Application.Dto
{
    public class Pagination
    {
        public int Page = 1;
        public int Limit = 20;
        public string SortBy = "ctime";
    }
}