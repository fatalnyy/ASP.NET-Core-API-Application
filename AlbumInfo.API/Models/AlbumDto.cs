using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumInfo.API.Models
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public int NumberOfTracks
        {
            get
            {
                return Tracks.Count;
            }
        }

        public ICollection<TrackDto> Tracks { get; set; } = new List<TrackDto>();
    }
}
