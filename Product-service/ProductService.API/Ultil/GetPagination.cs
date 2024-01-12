using ProductService.Application.Dto;

namespace ProductService.API.Ultil
{
    public static class GetPagination
    {
        public static Pagination Get(HttpContext httpContext)
        {
            int defaultPage = 1;
            int defaultLimit = 10;
            string sortByDefault = "ctime";

            if (!int.TryParse(httpContext.Request.Query["page"], out int page))
                page = defaultPage;
            if (!int.TryParse(httpContext.Request.Query["limit"], out int limit))
                limit = defaultLimit;
            string sortBy = httpContext.Request.Query["sortBy"].FirstOrDefault() ?? sortByDefault;

            Pagination pagination = new()
            {
                Page = page,
                Limit = limit,
                SortBy = sortBy
            };

            return pagination;
        }
    }

}