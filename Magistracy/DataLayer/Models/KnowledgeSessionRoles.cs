using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class KnowledgeSessionRole
    {
        public KnowledgeSessionRole()
        {
            Users = new HashSet<ApplicationUser>();
        }

        public int  Id { get; set; }
        public string RoleType { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

    }
}
