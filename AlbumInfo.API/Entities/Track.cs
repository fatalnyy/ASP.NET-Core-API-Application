using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumInfo.API.Entities
{
    public class Track
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Range(1,10)]
        public float Duration { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }

        [ForeignKey("AlbumId")]
        public Album Album { get; set; }
        public int AlbumId { get; set; }
    }
}
