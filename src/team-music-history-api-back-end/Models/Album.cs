using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_music_history_api_back_end.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Artist { get; set; }
        public int SongId { get; set; }
        public int MHUserId { get; set; }

        public MHUser MHUser { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
