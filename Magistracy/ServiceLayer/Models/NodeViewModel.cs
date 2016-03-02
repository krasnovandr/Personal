using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class NodeViewModel
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public int Level { get; set; }
        public int? ParentId { get; set; }
    }
}
