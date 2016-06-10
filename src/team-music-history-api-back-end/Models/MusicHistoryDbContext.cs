using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace team_music_history_api_back_end.Models
{
    public class MusicHistoryDbContext : DbContext
    {
        public MusicHistoryDbContext(DbContextOptions<MusicHistoryDbContext> options)
                : base(options)
            { }

        public DbSet<MHUser> MHUser { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Song> Song { get; set; }
    }
}
