using AutoMapper;
using DataAccess.Db;
using DataAccess.Entities.BookEntities;
using DTO.Book;
using Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concretes
{
    public class BookService : BaseService<BookDTO, Book, BookDTO>, IBookService
    {
        public BookService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public IEnumerable<BookDTO> GetFilter(out int prodCount, int page = 1, int pageSize = 4, BookSortOrder order = BookSortOrder.NameAscending, string search = null)
        {
            var result = GetAll();

            if (!string.IsNullOrEmpty(search?.Trim()))
            {
                result = result.Where(pr => pr.Name.ToLower().Contains(search.ToLower()));
            }

            prodCount = result.Count();

            result = order switch
            {
                BookSortOrder.NameAscending => result.OrderByDescending(b => b.Name),
                BookSortOrder.PriceAscending => result.OrderBy(b => b.Price),
                BookSortOrder.PriceDescending => result.OrderByDescending(b => b.Price),
                BookSortOrder.PublishedYearAscending => result.OrderBy(b => b.PublishedYear),
                BookSortOrder.PublishedYearDescending => result.OrderByDescending(b => b.PublishedYear),
                _ => result.OrderBy(b => b.Name),
            };

            var bookList = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return bookList;
        }
    }
}