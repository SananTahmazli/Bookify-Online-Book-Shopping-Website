using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities.Base;
using DataAccess.Entities.UserEntities;

namespace DataAccess.RoleEntities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}