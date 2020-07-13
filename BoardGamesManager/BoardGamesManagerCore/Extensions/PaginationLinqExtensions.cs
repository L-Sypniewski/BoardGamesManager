using System.Collections.Generic;
using System.Linq;

namespace BoardGamesManagerCore.Extensions
{
    public static class PaginationLinqExtensions
    {
        public static IQueryable<T> Paginated<T>(this IQueryable<T> queryable, int? pageSize = null, int? page = null)
        {
            var skippedQueryable = queryable.Skip((page - 1 ?? 0) * (pageSize ?? 0));
            return pageSize.HasValue ? skippedQueryable.Take(pageSize.Value) : skippedQueryable;
        }

        public static IEnumerable<T> Paginated<T>(this IEnumerable<T> queryable, int? pageSize = null, int? page = null)
        {
            var skippedQueryable = queryable.Skip((page - 1 ?? 0) * (pageSize ?? 0));
            return pageSize.HasValue ? skippedQueryable.Take(pageSize.Value) : skippedQueryable;
        }
    }
}