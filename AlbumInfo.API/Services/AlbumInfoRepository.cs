﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlbumInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlbumInfo.API.Services
{
    public class AlbumInfoRepository : IAlbumInfoRepository
    {
        private AlbumInfoContext _context;
        public AlbumInfoRepository(AlbumInfoContext context)
        {
            _context = context;
        }

        public bool AlbumExists(int albumId)
        {
            return _context.Albums.Any(a => a.Id == albumId);
        }
        public Album GetAlbum(int albumId, bool includeTracks)
        {
            if (includeTracks)
                return _context.Albums.Include(a => a.Tracks).Where(a => a.Id == albumId).FirstOrDefault();

            return _context.Albums.FirstOrDefault(a => albumId == a.Id);
        }

        public IEnumerable<Album> GetAlbums()
        {
            return _context.Albums.OrderBy(a => a.Name).ToList();
        }

        public Track GetTrackForAlbum(int albumId, int trackId)
        {
            return _context.Tracks.Where(t => t.AlbumId == albumId && t.Id == trackId).FirstOrDefault();
        }

        public IEnumerable<Track> GetTracksForAlbum(int albumId)
        {
            return _context.Tracks.Where(a => a.AlbumId == albumId).ToList();
        }

        public void AddTrackForAlbum(int albumId, Track track)
        {
            var album = GetAlbum(albumId, false);
            album.Tracks.Add(track);
        }

        public void DeleteTrack(Track track)
        {
            _context.Tracks.Remove(track);
        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
