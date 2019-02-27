using AlbumInfo.API.Models;
using AlbumInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumInfo.API.Controllers
{
    [Route("api/albums")]
    public class AlbumsController : Controller
    {
        private IAlbumInfoRepository _albumInfoRepository;

        public AlbumsController(IAlbumInfoRepository albumInfoRepository)
        {
            _albumInfoRepository = albumInfoRepository;
        }
        [HttpGet()]
        public IActionResult GetAlbums()
        {
            //return Ok(AlbumDataStore.Current.Albums);
            var albumEntities = _albumInfoRepository.GetAlbums();

            var results = new List<AlbumWithoutTracksDto>();

            foreach (var albumEntity in albumEntities)
            {
                results.Add(new AlbumWithoutTracksDto
                {
                    Id = albumEntity.Id,
                    Name = albumEntity.Name,
                    Artist = albumEntity.Artist
                });
            }

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetAlbum(int id, bool includeTracks)
        {
            var album = _albumInfoRepository.GetAlbum(id, includeTracks);

            if (album == null)
                return NotFound();

            if (includeTracks)
            {
                var albumResult = new AlbumDto()
                {
                    Id = album.Id,
                    Name = album.Name,
                    Artist = album.Artist
                };

                foreach (var track in album.Tracks)
                {
                    albumResult.Tracks.Add(new TrackDto()
                    {
                        Id = track.Id,
                        Name = track.Name,
                        Duration = track.Duration
                    });
                }

                return Ok(albumResult);
            }

            var albumWithoutTracksResult = new AlbumWithoutTracksDto()
            {
                Id = album.Id,
                Name = album.Name,
                Artist = album.Artist
            };

            return Ok(albumWithoutTracksResult);
            //var albumToReturn = AlbumDataStore.Current.Albums.FirstOrDefault(a => a.Id == id);
            //if (albumToReturn == null)
            //    return NotFound();

            //return Ok(albumToReturn);
        }
    }
}
