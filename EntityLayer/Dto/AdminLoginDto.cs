using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dto
{
    public class AdminLoginDto
    {
        public int AdminID { get; set; }
        public string AdminUserName { get; set; }
        public string AdminMail { get; set; }
        public string AdminPassword { get; set; }
        public string AdminRole { get; set; }
        public bool AdminStatus { get; set; }
    }
}
