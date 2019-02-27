using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumInfo.API.Models
{
    public class AlbumWithoutTracksDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
    }
}
