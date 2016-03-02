using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace ServiceLayer.Models
{
    public class KnowledgeSessionViewModel
    {
        public DateTime CreationDate { get; set; }
        public string Theme { get; set; }
        public string CreatorId { get; set; }

        public virtual List<UserViewModel> Users { get; set; }
        public virtual List<NodeViewModel> Nodes { get; set; }
    }
}
