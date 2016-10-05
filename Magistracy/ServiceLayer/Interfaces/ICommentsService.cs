using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Interfaces
{
    public interface ICommentsService
    {
        void Create(CommentViewModel commentView);
        List<CommentViewModel> Get(int nodeId);
    }
}
