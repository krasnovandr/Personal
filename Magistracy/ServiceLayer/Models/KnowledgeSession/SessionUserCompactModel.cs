using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class SessionUserCompactModel
    {
        public string UserName { get; set; }
        public string Id { get; set; }
        public string AvatarFilePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
