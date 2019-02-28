using AlbumInfo.API.Models;
using AlbumInfo.API.Services;
using AutoMapper;
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
        private IAlbumInfoRepository _albumInfoRepository;
        public TracksController(ILogger<TracksController> logger, IMailService mailservices, IAlbumInfoRepository albumInfoRepository)
        {
            _logger = logger;
            _mailServices = mailservices;
            _albumInfoRepository = albumInfoRepository;
        }

       [HttpGet("{albumId}/tracks")]
       public IActionResult GetTracks(int albumId)
       {
            try
            {
                if (!_albumInfoRepository.AlbumExists(albumId))
                {
                    _logger.LogInformation($"Album with id {albumId} wasn't found when accessing tracks");
                    return NotFound();
                }

                var tracksForAlbum = _albumInfoRepository.GetTracksForAlbum(albumId);

                var tracksForAlbumResults = Mapper.Map<IEnumerable<TrackDto>>(tracksForAlbum);

                return Ok(tracksForAlbumResults);
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
            if (!_albumInfoRepository.AlbumExists(albumId))
                return NotFound();

            var track = _albumInfoRepository.GetTrackForAlbum(albumId, trackId);

            if (track == null)
                return NotFound();

            var trackResult = Mapper.Map<TrackDto>(track);

            return Ok(trackResult);
        }

        [HttpPost("{albumId}/tracks")]
        public IActionResult CreateTrack(int albumId,
            [FromBody] TrackForCreationDto track)
        {
            if (track == null)
                return BadRequest();

            if (!_albumInfoRepository.AlbumExists(albumId))
                return NotFound();

            var finalTrack = Mapper.Map<Entities.Track>(track);

            _albumInfoRepository.AddTrackForAlbum(albumId, finalTrack);

            if (!_albumInfoRepository.Save())
                return StatusCode(500, "A problem happened while handling your request!");

            var createdTrack = Mapper.Map<Models.TrackDto>(finalTrack);

            return CreatedAtRoute("GetTrack", new
            { albumId = albumId, trackId = createdTrack.Id }, createdTrack);

        }

        [HttpPut("{albumId}/tracks/{trackId}")]
        public IActionResult UpdateTrack(int albumId, int trackId,
            [FromBody] TrackForCreationDto track)
        {
            if (track == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_albumInfoRepository.AlbumExists(albumId))
                return NotFound();

            var trackEntity = _albumInfoRepository.GetTrackForAlbum(albumId, trackId);
            if (trackEntity == null)
                return NotFound();

            Mapper.Map(track, trackEntity);

            if (!_albumInfoRepository.Save())
                return StatusCode(500, "A problem happened while handling your request!");

            return NoContent();
        }

        //[HttpPatch("{albumId}/tracks/{trackId")]
        //public IActionResult PartialUpdateTrack(int albumId, int trackId,
        //    [FromBody] JsonPatchDocument<TrackForUpdateDto> patchDoc)
        //{
        //    if (patchDoc == null)
        //        return BadRequest();

        //    if (!_albumInfoRepository.AlbumExists(albumId))
        //        return NotFound();

        //    var trackEntity = _albumInfoRepository.GetTrackForAlbum(albumId, trackId);
        //    if (trackEntity == null)
        //        return NotFound();

        //    var trackToPatch = Mapper.Map<TrackForUpdateDto>(trackEntity);

        //    patchDoc.ApplyTo(trackToPatch, ModelState);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    Mapper.Map(trackToPatch, trackEntity);

        //    if (!_albumInfoRepository.Save())
        //        return StatusCode(500, "A problem happened while handling your request!");

        //    return NoContent();
        //}

        [HttpDelete("{albumId}/tracks/{trackId}")]
        public IActionResult DeleteTrack(int albumId, int trackId)
        {
            if (!_albumInfoRepository.AlbumExists(albumId))
                return NotFound();

            var trackEntity = _albumInfoRepository.GetTrackForAlbum(albumId, trackId);
            if (trackEntity == null)
                return NotFound();

            _albumInfoRepository.DeleteTrack(trackEntity);

            if (!_albumInfoRepository.Save())
                return StatusCode(500, "A problem happened while handling your request!");

            _mailServices.Send("Track was deleted", $"Track with name {trackEntity.Name} was deleted with id {trackEntity.Id}");

            return NoContent();
        }
    }
}
