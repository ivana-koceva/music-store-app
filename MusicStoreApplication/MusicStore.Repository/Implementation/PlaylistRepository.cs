using Microsoft.EntityFrameworkCore;
using MusicStore.Repository.Interface;
using MusicStore.Web.Data;
using MusicStore.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repository.Implementation
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<UserPlaylist> entities;

        public PlaylistRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<UserPlaylist>();
        }

        public List<UserPlaylist> GetAllPlaylists()
        {
            return entities
                .Include(z=>z.TracksInPlaylist)
                .Include(z=>z.Owner)
                .Include("TracksInPlaylist.Track")
                .ToList();
        }

        public UserPlaylist GetDetailsForPlaylist(BaseEntity id)
        {
            return entities
                .Include(z => z.TracksInPlaylist)
                .Include(z => z.Owner)
                .Include("TracksInPlaylist.Track")
                .SingleOrDefaultAsync(z => z.Id == id.Id).Result;
        }
    };
}
