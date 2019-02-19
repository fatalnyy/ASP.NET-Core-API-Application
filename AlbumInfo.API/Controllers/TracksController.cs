using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumInfo.API.Controllers
{
    [Route("api/albums")]
    public class TracksController : Controller
    {
       [HttpGet("{albumId}/tracks")]
       public IActionResult GetTracks(int albumId)
       {
            var album = AlbumDataStore.Current.Albums.FirstOrDefault(a => a.Id == albumId);

            if (album == null)
                return NotFound();

            return Ok(album.Tracks);
       }

        [HttpGet("{albumId}/tracks/{trackId}")]
        public IActionResult GetTrack(int albumId, int trackId)
        {
            var album = AlbumDataStore.Current.Albums.FirstOrDefault(a => a.Id == albumId);
            if (album == null)
                return NotFound();

            var track = album.Tracks.FirstOrDefault(t => t.Id == trackId);
            if (track == null)
                return NotFound();

            return Ok(track);
        }
    }
}
