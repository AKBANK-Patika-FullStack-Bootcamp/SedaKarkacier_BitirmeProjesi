using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public int ApartmentId { get; set; }
        public int PayerId { get; set; }
        public bool IsPayed { get; set; }
        public DateTime? PayedDate { get; set; }
    }
}
