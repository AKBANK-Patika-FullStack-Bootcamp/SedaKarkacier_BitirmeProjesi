using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string IdentityNum { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string CarPlateNum { get; set; }
        public int? ApartmentId { get; set; }
        public bool Admin { get; set; }
    }
}
