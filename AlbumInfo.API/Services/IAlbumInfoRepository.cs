using AlbumInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumInfo.API.Services
{
    public interface IAlbumInfoRepository
    {
        bool AlbumExists(int albumId);
        IEnumerable<Album> GetAlbums();
        Album GetAlbum(int albumId, bool includeTracks);
        IEnumerable<Track> GetTracksForAlbum(int albumId);
        Track GetTrackForAlbum(int albumId, int trackId);
        void AddTrackForAlbum(int albumId, Track track);
        void DeleteTrack(Track track);
        bool Save();
    }
}
