using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumInfo.API.Entities
{
    public class AlbumInfoContext : DbContext
    {
        public AlbumInfoContext(DbContextOptions<AlbumInfoContext> options)
            :base(options)
        {
            Database.Migrate();
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
    }
}
