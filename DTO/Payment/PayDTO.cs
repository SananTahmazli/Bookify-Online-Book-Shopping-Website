using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Payment
{
    public class PayDTO
    {
        public int CartId { get; set; }
        public double Total { get; set; }
        public int Count { get; set; }
        public double ItemPrice { get; set; }
    }
}