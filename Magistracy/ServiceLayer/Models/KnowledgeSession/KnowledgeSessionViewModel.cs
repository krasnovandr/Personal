using System;
using System.Collections.Generic;
using DataLayer.Models;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class KnowledgeSessionViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Theme { get; set; }
        public string CreatorId { get; set; }

        public virtual List<UserViewModel> Users { get; set; }
        public virtual List<NodeViewModel> SessionNodes { get; set; }
    }
}
