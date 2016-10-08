using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class NodeResourceViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int NodeId { get; set; }
        public string ResourceRaw { get; set; }
        public string Resource { get; set; }

        public string AddBy { get; set; }
        public string AvatarFilePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    //public class NodeResource
    //{
    //    [Key]

    //}
}
