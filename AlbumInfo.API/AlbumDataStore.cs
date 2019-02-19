using AlbumInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumInfo.API
{
    public class AlbumDataStore
    {
        public static AlbumDataStore Current { get; } = new AlbumDataStore();
        public List<AlbumDto> Albums { get; set; }

        public AlbumDataStore()
        {
            Albums = new List<AlbumDto>()
            {
                new AlbumDto()
                {
                    Id = 1,
                    Name = "The Slim Shady",
                    Artist = "Eminem",
                    Tracks = new List<TrackDto>()
                    {
                        new TrackDto()
                        {
                            Id = 1,
                            Name = "My Name Is",
                            Duration = 1.59f
                        },
                        new TrackDto()
                        {
                            Id = 2,
                            Name = "RoleModel",
                            Duration = 3.56f
                        },
                        new TrackDto()
                        {
                            Id = 3,
                            Name = "Bad Meets Evil",
                            Duration = 5.22f
                        },
                        new TrackDto()
                        {
                            Id = 4,
                            Name = "My Fault",
                            Duration = 6.43f
                        },
                    }
                },
                new AlbumDto()
                {
                    Id = 2,
                    Name = "Daytona",
                    Artist = "Pusha T",
                     Tracks = new List<TrackDto>()
                    {
                        new TrackDto()
                        {
                            Id = 1,
                            Name = "Infrared",
                            Duration = 2.13f
                        },
                        new TrackDto()
                        {
                            Id = 2,
                            Name = "Santeria",
                            Duration = 12.43f
                        },
                    }
                },
                new AlbumDto()
                {
                    Id = 3,
                    Name = "To Pimp A Butterfly",
                    Artist = "Kendrick Lamar",
                     Tracks = new List<TrackDto>()
                    {
                        new TrackDto()
                        {
                            Id = 1,
                            Name = "King Kunta",
                            Duration = 4.00f
                        },
                        new TrackDto()
                        {
                            Id = 2,
                            Name = "u",
                            Duration = 4.45f
                        },
                        new TrackDto()
                        {
                            Id = 3,
                            Name = "i",
                            Duration = 3.22f
                        },
                        new TrackDto()
                        {
                            Id = 4,
                            Name = "Alright",
                            Duration = 5.43f
                        },
                        new TrackDto()
                        {
                            Id = 5,
                            Name = "Mortal Man",
                            Duration = 3.53f
                        },
                    }
                }
            };
        }
    }
}
