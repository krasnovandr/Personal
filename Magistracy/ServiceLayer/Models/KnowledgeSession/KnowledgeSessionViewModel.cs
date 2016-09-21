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


        //   public KnowledgeSession()
        //{
        //    Users = new HashSet<ApplicationUser>();
        //    SessionNodes = new List<SessionNode>();
        //}

        //[Key]
        //public int Id { get; set; }
        //public DateTime Date { get; set; }
        //public string Theme { get; set; }
        //public string CreatorId { get; set; }
        //public int SessionState { get; set; }

        //public virtual ICollection<ApplicationUser> Users { get; set; }
        //public virtual ICollection<SessionNode> SessionNodes { get; set; }
}
