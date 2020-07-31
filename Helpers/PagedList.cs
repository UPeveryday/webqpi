using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routines.Api.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(List<T> Items,int count ,int pagenumber,int pagesize)
        {
            TotalCount = count;
            PageSize = pagesize;
            CurrentPage = pagenumber;
            TotalPages = (int)Math.Ceiling(count / (double)pagesize);

            AddRange(Items);
        }

        public static async  Task<PagedList<T>> Create(IQueryable<T> source,int pagenumber,int pagesize)
        {
            var count = await source.CountAsync();
            var items =await  source.Skip((pagenumber - 1) * pagesize).Take(pagesize).ToListAsync();
            return new PagedList<T>(items, count, pagenumber, pagesize);
        }
    }
}
