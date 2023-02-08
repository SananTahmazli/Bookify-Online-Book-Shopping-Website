using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities.Base;
using DataAccess.Entities.CartEntities;
using DataAccess.RoleEntities;

namespace DataAccess.Entities.UserEntities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Cart> Cart { get; set; } = new List<Cart>();
    }
}