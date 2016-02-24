using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IUploadService
    {
        void SaveSongFromVkUpdatePicture(SongViewModel song, string userid);
        void UploadSong(string fileExtension, string fileName, string pathSong, string songId,
            string absoluteSongCoverPath, string userId);

        void UploadConversationImage(string imagePath, string conversationId);
        void UploadUserImage(string imagePath, string userId);
        List<SongViewModel> GetSongVk(string userId, VkUserModel model);
    }
}
