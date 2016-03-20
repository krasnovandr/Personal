using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set;}
        public string Value { get; set; }
        public virtual UserViewModel CommentBy { get; set; }
    }
}
