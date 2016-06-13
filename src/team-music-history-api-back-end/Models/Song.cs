using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_music_history_api_back_end.Models
{
    public class Song
    {
        public int SongId { get; set; }
        public string Name { get; set; }
        public int AlbumId { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }

        public Album Albums { get; set; }
    }
}
