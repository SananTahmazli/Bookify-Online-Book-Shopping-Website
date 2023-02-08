using DTO.Book;
using Helper.RoleKeywordsHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstracts;

namespace Presentation.Controllers.Admin
{
    [Authorize(Roles = RoleKeywords.AdminRole)]
    public class AdminController : Controller
    {
        private readonly IBookService _bookService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public AdminController(IBookService bookService, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _bookService = bookService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BookDTO dto)
        {
            try
            {
                string uniqueFileName = null;

                if (string.IsNullOrEmpty(dto.CoverPhotoPath))
                {
                    dto.CoverPhotoPath = "~/photos/homepage/no-photo.png";
                }

                if (dto.CoverPhoto != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "photos/book");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.CoverPhoto.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    dto.CoverPhoto.CopyTo(new FileStream(filePath, FileMode.Create));
                    dto.CoverPhotoPath = "~/photos/book/" + uniqueFileName;
                }

                _bookService.Create(dto);
                ViewBag.Success = "The book has been successfully added to our website!";
                return View();
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.Message;
                return View();
            }
        }

        [HttpGet("Admin/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var book = _bookService.Get(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult EditDTO(BookDTO dto)
        {
            try
            {
                string uniqueFileName = null;

                if (string.IsNullOrEmpty(dto.CoverPhotoPath))
                {
                    dto.CoverPhotoPath = "~/photos/homepage/no-photo.png";
                }

                if (dto.CoverPhoto != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "photos/book");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.CoverPhoto.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    dto.CoverPhoto.CopyTo(new FileStream(filePath, FileMode.Create));
                    dto.CoverPhotoPath = "~/photos/book/" + uniqueFileName;
                }

                var book = _bookService.Update(dto);
                ViewBag.Success = "The book's data has been updated successfully!";
                return View("Edit", book);
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.Message;
                return View();
            }
        }

        [HttpGet("Admin/Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var book = _bookService.Get(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult RemoveDTO(int id)
        {
            _bookService.Delete(id);
            return RedirectToAction("GetAll", "Admin");
        }

        [HttpGet("Admin/GetAll")]
        public IActionResult GetAll()
        {
            var bookList = _bookService.GetAll().OrderByDescending(b => b.Id);
            return View(bookList);
        }

        [HttpGet("Admin/Get/{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookService.Get(id);
            return View(book);
        }
    }
}