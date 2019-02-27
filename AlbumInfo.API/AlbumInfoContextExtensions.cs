using AlbumInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumInfo.API
{
    public static class AlbumInfoContextExtensions
    {
        public static void EnsureSeedDataForContext(this AlbumInfoContext context)
        {
            if (context.Albums.Any())
                return;

            var albums = new List<Album>()
            {
                new Album()
                {
                    Name = "The Slim Shady",
                    Artist = "Eminem",
                    Tracks = new List<Track>()
                    {
                        new Track()
                        {
                            Name = "My Name Is",
                            Duration = 1.59f
                        },
                        new Track()
                        {
                            Name = "RoleModel",
                            Duration = 3.56f
                        },
                        new Track()
                        {
                            Name = "Bad Meets Evil",
                            Duration = 5.22f
                        },
                        new Track()
                        {
                            Name = "My Fault",
                            Duration = 6.43f
                        },
                    }
                },
                new Album()
                {
                    Name = "Daytona",
                    Artist = "Pusha T",
                     Tracks = new List<Track>()
                    {
                        new Track()
                        {
                            Name = "Infrared",
                            Duration = 2.13f
                        },
                        new Track()
                        {
                            Name = "Santeria",
                            Duration = 12.43f
                        },
                    }
                },
                new Album()
                {
                    Name = "To Pimp A Butterfly",
                    Artist = "Kendrick Lamar",
                     Tracks = new List<Track>()
                    {
                        new Track()
                        {
                            Name = "King Kunta",
                            Duration = 4.00f
                        },
                        new Track()
                        {
                            Name = "u",
                            Duration = 4.45f
                        },
                        new Track()
                        {
                            Name = "i",
                            Duration = 3.22f
                        },
                        new Track()
                        {
                            Name = "Alright",
                            Duration = 5.43f
                        },
                        new Track()
                        {
                            Name = "Mortal Man",
                            Duration = 3.53f
                        },
                    }
                }
            };

            context.Albums.AddRange(albums);
            context.SaveChanges();
        }
    }
}
