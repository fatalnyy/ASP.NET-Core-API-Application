using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumInfo.API.Models
{
    public class TrackDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Duration { get; set; }
    }
}
