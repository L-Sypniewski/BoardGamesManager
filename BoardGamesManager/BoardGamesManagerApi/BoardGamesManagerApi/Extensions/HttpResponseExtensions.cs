using BoardGamesManagerCore;
using Microsoft.AspNetCore.Http;

namespace BoardGamesManagerApi.Extensions
{
    public static class HttpResponseExtensions
    {
        public static void AddPaginationHeaders(this HttpResponse response, Pagination pagination)
        {
            response.Headers.Add("Page", pagination.Page.ToString());
            response.Headers.Add("PageSize", pagination.PageSize.ToString());
            response.Headers.Add("TotalCount", pagination.TotalCount.ToString());
            response.Headers.Add("TotalPages", pagination.TotalPages.ToString());
        }
    }
}