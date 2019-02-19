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
        [HttpGet()]
        public IActionResult GetAlbums()
        {
            return Ok(AlbumDataStore.Current.Albums);
        }

        [HttpGet("{id}")]
        public IActionResult GetAlbum(int id)
        {
            var albumToReturn = AlbumDataStore.Current.Albums.FirstOrDefault(a => a.Id == id);
            if (albumToReturn == null)
                return NotFound();

            return Ok(albumToReturn);
        }
    }
}
