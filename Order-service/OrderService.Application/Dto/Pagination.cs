

namespace OrderService.Application.Dto
{
    public record Pagination
    {
        public int Page {get; set;}
        public int Limit {get; set;}
        public string SortBy {get; set;} = "ctime";
    }
}