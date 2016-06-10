using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_music_history_api_back_end.Models
{
    public class MHUser
    {
        public int MHUserId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}
