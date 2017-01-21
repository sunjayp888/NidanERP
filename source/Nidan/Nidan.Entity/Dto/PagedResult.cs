using System.Collections.Generic;
using System.Linq;

namespace Nidan.Entity.Dto
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; }
        public int ResultsPerPage { get; }
        public int TotalPages { get; }
        public long TotalResults { get; }

        protected PagedResultBase()
        {
        }

        protected PagedResultBase(int currentPage, int resultsPerPage, int totalPages, long totalResults)
        {
            CurrentPage = currentPage > totalPages ? totalPages : currentPage;
            ResultsPerPage = resultsPerPage;
            TotalPages = totalPages;
            TotalResults = totalResults;
        }
    }

    public class PagedResult<T> : PagedResultBase
    {
        public IEnumerable<T> Items { get; }

        protected PagedResult()
        {
            Items = Enumerable.Empty<T>();
        }

        protected PagedResult(IEnumerable<T> items, int currentPage, int resultsPerPage, int totalPages, long totalResults)
                : base(currentPage, resultsPerPage, totalPages, totalResults)
        {
            Items = items;
        }

        public static PagedResult<T> Create(IEnumerable<T> items, int currentPage, int resultsPerPage, int totalPages, long totalResults) => new PagedResult<T>(items, currentPage, resultsPerPage, totalPages, totalResults);

        public static PagedResult<T> Empty => new PagedResult<T>();
    }
}
