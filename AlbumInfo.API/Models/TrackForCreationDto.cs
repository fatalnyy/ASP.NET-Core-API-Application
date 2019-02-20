using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumInfo.API.Models
{
    public class TrackForCreationDto
    {
        public string Name { get; set; }
        public float Duration { get; set; }
    }
}
