using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Book
{
    public enum BookSortOrder
    {
        NameAscending,
        NameDescending,
        PublishedYearAscending,
        PublishedYearDescending,
        PriceAscending,
        PriceDescending
    }

    public class BookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PublishedYear { get; set; }
        public double Price { get; set; }
        public string CoverPhotoPath { get; set; }
        public IFormFile CoverPhoto { get; set; }
    }
}