using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumInfo.API.Models
{
    public class TrackForUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Range(1,10)]
        public float Duration { get; set; }
    }
}
