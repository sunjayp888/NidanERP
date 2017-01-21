using System;
using System.Linq;
using Nidan.Entity.Dto;

namespace Nidan.Data.Extensions
{
    public static class Pagination
    {

        public static PagedResult<T> Paginate<T>(this IOrderedQueryable<T> source, Paging paging)
        {
            return source.AsQueryable().Paginate(paging);
        }

        public static PagedResult<T> Paginate<T>(this IQueryable<T> source, Paging paging)
        {
            var isEmpty = source == null || !source.Any();
            if (isEmpty)
                return PagedResult<T>.Empty;

            var totalResults = source.Count();

            if (paging == null)
                return PagedResult<T>.Create(source.ToList(), 1, totalResults, 1, totalResults);

            if (paging.Page <= 0)
                paging.Page = 1;

            if (paging.PageSize <= 0)
                paging.PageSize = 10;
            
            var totalPages = (int)Math.Ceiling((double)totalResults / paging.PageSize);
            var data = source.Skip((paging.Page - 1) * paging.PageSize).Take(paging.PageSize).ToList();

            return PagedResult<T>.Create(data, paging.Page, paging.PageSize, totalPages, totalResults);
        }
    }
}
