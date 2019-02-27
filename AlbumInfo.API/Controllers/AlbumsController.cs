using AlbumInfo.API.Models;
using AlbumInfo.API.Services;
using AutoMapper;
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
            var albumEntities = _albumInfoRepository.GetAlbums();
            var results = Mapper.Map<IEnumerable<AlbumWithoutTracksDto>>(albumEntities);

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
                var albumResult = Mapper.Map<AlbumDto>(album);

                return Ok(albumResult);
            }

            var albumWithoutTracksResult = Mapper.Map<AlbumWithoutTracksDto>(album);

            return Ok(albumWithoutTracksResult);
        }
    }
}
