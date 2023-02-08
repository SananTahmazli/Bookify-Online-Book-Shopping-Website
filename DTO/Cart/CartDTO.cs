using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Cart
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public double BookPrice { get; set; }
        public string BookCoverPhotoPath { get; set; }
        public int Count { get; set; }
        public double Total => Count * BookPrice;
    }
}