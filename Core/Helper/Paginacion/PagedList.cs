using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper.Paginacion
{
    public class PagedList<T> : List<T>
    {

        public int currentPage { get; private set; }
        public int totalPages { get; private set; }
        public int pageSize { get; private set; }
        public int totalCount { get; private set; }
        public bool hasPrevious => (currentPage > 1);
        public bool hasNext => (currentPage < totalPages);


        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            this.totalCount = count;
            this.pageSize = pageSize;
            this.currentPage = pageNumber;
            this.totalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {

            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

    }
}
