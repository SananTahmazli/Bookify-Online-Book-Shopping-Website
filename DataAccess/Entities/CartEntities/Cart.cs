using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities.Base;
using DataAccess.Entities.BookEntities;
using DataAccess.Entities.UserEntities;

namespace DataAccess.Entities.CartEntities
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Count { get; set; }
    }
}