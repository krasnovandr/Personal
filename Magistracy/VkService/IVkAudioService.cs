using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkService
{
  public  interface IVkAudioService
  {
      string GetSongLyrics(string songTitle,string artist);
  }
}
