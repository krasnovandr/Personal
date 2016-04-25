using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class NodeIdentifyModel
    {
        public int SessionId { get; set; }
        //public int Level { get; set; }
        public int? ParentId { get; set; }
    }
}
