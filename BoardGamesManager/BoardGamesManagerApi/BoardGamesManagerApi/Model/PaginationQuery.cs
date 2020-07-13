using System;
using BoardGamesManagerCore;

namespace BoardGamesManagerApi.Model
{
    public class PaginationQuery
    {
        public int? Page { get; set; }
        public int? Limit { get; set; }

        public Pagination ToPagination(int totalCount, int maxPageSize)
        {
            var limit = Limit;
            var pageSize = limit != null ? Math.Min(limit.Value, maxPageSize) : maxPageSize;
            var page = Page;
            return new Pagination(totalCount, pageSize, page ?? 1);
        }
    }
}