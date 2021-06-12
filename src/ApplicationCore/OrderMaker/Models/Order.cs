using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.OrderMaker.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public int Qty { get; set; }
        public int MatchQty { get; set; }
        public string Status { get; set; }
    }
}
