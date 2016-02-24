using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public interface ITagsRepository
    {
        void AddTag(string tagName, string songId);
        void RemoveTag(string tagId, string songId);

        void LikeTag(string tagId, string songId, string userId);
        void DisLikeTag(string tagId, string songId, string userId);
        Tag GetTag(string tagId);
        IEnumerable<Tag> GetSongTags(string songId);
    }
    public class TagsRepository : ITagsRepository
    {
        public void AddTag(string tagName, string songId)
        {

        }

        public void LikeTag(string tagId, string userId)
        {

        }

        public void RemoveTag(string tagId, string songId)
        {

        }

        public void LikeTag(string tagId, string songId, string userId)
        {
            throw new NotImplementedException();
        }

        public void DisLikeTag(string tagId, string songId, string userId)
        {
            throw new NotImplementedException();
        }


        public Tag GetTag(string tagId)
        {
            return null;
        }

        public IEnumerable<Tag> GetSongTags(string songId)
        {
            return null;
        }
    }
}
