using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
        public bool IsRead { get; set; }
        public DateTime DateTime { get; set; }
    }
}
