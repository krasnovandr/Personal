using System;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class NodeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public int Level { get; set; }
        public int? ParentId { get; set; }
    }
}
