using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class CommentViewModel
    {
        public string AvatarFilePath { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set;}
        public string Value { get; set; }
        public int CommentTo { get; set; }
        public string CommentBy { get; set; }
    }
}
