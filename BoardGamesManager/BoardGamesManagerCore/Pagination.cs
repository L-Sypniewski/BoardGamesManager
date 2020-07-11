using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGamesManagerCore
{
    public struct Pagination
    {
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }

        public Pagination(int totalCount, int pageSize)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            TotalPages = CalculatedTotalPages(totalCount, pageSize);
        }

        private static int CalculatedTotalPages(int totalCount, int pageSize)
        {
            if (totalCount == 0)
            {
                return 1;
            }

            return totalCount % pageSize != 0 ?
                totalCount / pageSize + 1 :
                totalCount / pageSize;
        }
    }      
}