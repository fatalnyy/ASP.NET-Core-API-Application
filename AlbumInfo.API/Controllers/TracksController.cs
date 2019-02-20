using AlbumInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{albumId}/tracks/{trackId}", Name = "GetTrack")]
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

        [HttpPost("{albumId}/tracks")]
        public IActionResult CreateTrack(int albumId,
            [FromBody] TrackForCreationDto track)
        {
            if (track == null)
                return BadRequest();

            var album = AlbumDataStore.Current.Albums.FirstOrDefault(a => a.Id == albumId);

            if (album == null)
                return NotFound();

            var lastTrackId = AlbumDataStore.Current.Albums.SelectMany(a => a.Tracks).Max(p => p.Id);

            var finalTrack = new TrackDto()
            {
                Id = ++lastTrackId,
                Name = track.Name,
                Duration = track.Duration
            };

            album.Tracks.Add(finalTrack);

            return CreatedAtRoute("GetTrack", new
            { albumId = albumId, trackId = finalTrack.Id }, finalTrack);

        }

        [HttpPut("{albumId}/tracks/{trackId}")]
        public IActionResult UpdateTrack(int albumId, int trackId,
            [FromBody] TrackForCreationDto track)
        {
            if (track == null)
                return BadRequest();

            var album = AlbumDataStore.Current.Albums.FirstOrDefault(a => a.Id == albumId);

            if (album == null)
                return NotFound();

            var trackToUpdate = album.Tracks.FirstOrDefault(t => t.Id == trackId);

            if (trackToUpdate == null)
                return NotFound();

            trackToUpdate.Name = track.Name;
            trackToUpdate.Duration = track.Duration;

            return NoContent();
        }

        [HttpPatch("{albumId}/tracks/{trackId")]
        public IActionResult PartialUpdateTrack(int albumId, int trackId,
            [FromBody] JsonPatchDocument<TrackForCreationDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var album = AlbumDataStore.Current.Albums.FirstOrDefault(a => a.Id == albumId);

            if (album == null)
                return NotFound();

            var trackToUpdate = album.Tracks.FirstOrDefault(t => t.Id == trackId);

            if (trackToUpdate == null)
                return NotFound();

            var trackToPatch =
                new TrackForCreationDto()
                {
                    Name = trackToUpdate.Name,
                    Duration = trackToUpdate.Duration
                };
        }
    }
}
