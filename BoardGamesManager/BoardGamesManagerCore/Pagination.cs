namespace BoardGamesManagerCore
{
    public readonly struct Pagination
    {
        public int PageSize { get; }
        public int Page { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }

        public Pagination(int totalCount, int pageSize, int page)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            Page = page > 0 ? page : 0;
            TotalPages = CalculatedTotalPages(totalCount, pageSize);
        }

        private static int CalculatedTotalPages(int totalCount, int pageSize)
        {
            if (totalCount == 0) return 1;

            return totalCount % pageSize != 0 ? totalCount / pageSize + 1 : totalCount / pageSize;
        }
    }
}