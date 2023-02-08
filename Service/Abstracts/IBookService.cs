using DataAccess.Entities.BookEntities;
using DTO.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstracts
{
    public interface IBookService : IBaseService<BookDTO, Book, BookDTO>
    {
        IEnumerable<BookDTO> GetFilter(out int prodCount, int page = 1, int pageSize = 4, BookSortOrder order = BookSortOrder.NameAscending, string search = null);
    }
}