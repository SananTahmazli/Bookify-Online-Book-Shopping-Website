using DTO.Book;
using DTO.PagedResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstracts;

namespace Presentation.Controllers.Book
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("Book/Get/{id}")]
        public IActionResult Get(int id, string message = null, bool isSuccess = true)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (isSuccess)
                {
                    ViewBag.Success = message;
                }
                else
                {
                    ViewBag.Error = message;
                }
            }

            var result = _bookService.Get(id);

            if (result == null)
            {
                ViewBag.Error = "Book is not found!";
                return View();
            }
            return View(result);
        }

        [HttpGet("Book/GetAll")]
        public IActionResult GetAll(int page = 1, int pageSize = 4, BookSortOrder order = BookSortOrder.NameAscending, string search = null, bool changeSort = false)
        {
            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
            }

            int allProductsCount;
            var result = _bookService.GetFilter(out allProductsCount, page, pageSize, order, search);

            ViewBag.HasPrevious = true;
            ViewBag.HasNext = true;

            if (page <= 1)
            {
                ViewBag.HasPrevious = false;
            }
            if (page * pageSize >= allProductsCount)
            {
                ViewBag.HasNext = false;
            }

            if (changeSort)
            {
                ViewBag.NameSort = order == BookSortOrder.NameAscending ? BookSortOrder.NameDescending : BookSortOrder.NameAscending;
                ViewBag.PublishedYearSort = order == BookSortOrder.PublishedYearAscending ? BookSortOrder.PublishedYearDescending : BookSortOrder.PublishedYearAscending;
                ViewBag.PriceSort = order == BookSortOrder.PriceAscending ? BookSortOrder.PriceDescending : BookSortOrder.PriceAscending;
            }
            else
            {
                ViewBag.NameSort = order;
                ViewBag.PublishedYearSort = order;
                ViewBag.PriceSort = order;
            }

            var pagedResponse = new PagedResponseDTO<BookDTO>(page, pageSize, result);
            return View(pagedResponse);
        }
    }
}