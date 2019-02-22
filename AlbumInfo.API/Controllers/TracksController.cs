using AlbumInfo.API.Models;
using AlbumInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumInfo.API.Controllers
{
    [Route("api/albums")]
    public class TracksController : Controller
    {
        private ILogger<TracksController> _logger;
        private IMailService _mailServices;

        public TracksController(ILogger<TracksController> logger, IMailService mailservices)
        {
            _logger = logger;
            _mailServices = mailservices;
        }

       [HttpGet("{albumId}/tracks")]
       public IActionResult GetTracks(int albumId)
       {
            try
            {
                var album = AlbumDataStore.Current.Albums.FirstOrDefault(a => a.Id == albumId);

                if (album == null)
                {
                    _logger.LogInformation($"The album with id {albumId} was not found.");
                    return NotFound();
                }

                return Ok(album.Tracks);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting tracks from album with id {albumId}", ex);
                return StatusCode(500, "There was an error with handling your request.");
            }
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

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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

        //[HttpPatch("{albumId}/tracks/{trackId")]
        //public IActionResult PartialUpdateTrack(int albumId, int trackId,
        //    [FromBody] JsonPatchDocument<TrackForUpdateDto> patchDoc)
        //{
        //    if (patchDoc == null)
        //        return BadRequest();

        //    var album = AlbumDataStore.Current.Albums.FirstOrDefault(a => a.Id == albumId);

        //    if (album == null)
        //        return NotFound();

        //    var trackToUpdate = album.Tracks.FirstOrDefault(t => t.Id == trackId);

        //    if (trackToUpdate == null)
        //        return NotFound();

        //    var trackToPatch =
        //        new TrackForUpdateDto()
        //        {
        //            Name = trackToUpdate.Name,
        //            Duration = trackToUpdate.Duration
        //        };

        //    patchDoc.ApplyTo(trackToPatch, ModelState);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    trackToUpdate.Name = trackToPatch.Name;
        //    trackToUpdate.Duration = trackToPatch.Duration;

        //    return NoContent();
        //}

        [HttpDelete("{albumId}/tracks/{trackId}")]
        public IActionResult DeleteTrack(int albumId, int trackId)
        {
            var album = AlbumDataStore.Current.Albums.FirstOrDefault(a => a.Id == albumId);

            if (album == null)
                return NotFound();

            var trackToDelete = album.Tracks.FirstOrDefault(t => t.Id == trackId);

            if (trackToDelete == null)
                return NotFound();

            album.Tracks.Remove(trackToDelete);
            _mailServices.Send("Track was deleted", $"Track with name {trackToDelete.Name} was deleted with id {trackToDelete.Id}");

            return NoContent();
        }
    }
}
