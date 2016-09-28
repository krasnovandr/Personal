using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace ServiceLayer.Models
{
    public class AddMembersViewModel
    {
        public List<ApplicationUser> Members { get; set; }
        public int SessionId { get; set; }
    }
}
