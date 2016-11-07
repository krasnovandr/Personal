using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Interfaces;
using DataLayer.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IUnitOfWork _db;

        public CommentsService(IUnitOfWork db)
        {
            _db = db;
        }

        public void Create(CommentViewModel commentView)
        {
            var comment = Mapper.Map<CommentViewModel, Comment>(commentView);

            comment.CommentBy = _db.Users.Get(commentView.CommentBy);
            comment.CommentTo = _db.Nodes.Get(commentView.CommentTo);
            comment.Date = DateTime.Now;

            _db.Comments.Create(comment);
            _db.Save();
        }

        public List<CommentViewModel> Get(int nodeId)
        {
            var comments = _db.Nodes.Get(nodeId).Comments;

            var commentsViewModel = Mapper.Map<ICollection<Comment>, List<CommentViewModel>>(comments);

            return commentsViewModel.OrderBy(m=>m.Date).ToList();
        }
    }
}
