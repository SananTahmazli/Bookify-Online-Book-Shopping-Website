using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public int UpdatedUserId { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}