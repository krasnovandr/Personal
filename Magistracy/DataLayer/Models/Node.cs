using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public class Node
    {
        public Node()
        {
            KnowledgeSessions =  new HashSet<KnowledgeSession>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreation { get; set; }
        public int? ParentId { get; set; }
        public int Level { get; set; }

        public virtual ICollection<KnowledgeSession> KnowledgeSessions { get; set; }

    }
}
