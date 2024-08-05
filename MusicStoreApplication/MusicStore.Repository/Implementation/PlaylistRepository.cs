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

        public void Delete(UserPlaylist entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public UserPlaylist Get(Guid? id)
        {
            return entities.SingleOrDefault(z => z.Id == id);
        }

        public List<UserPlaylist> GetAllPlaylists(string? id)
        {
            return entities
                .Where(z=>z.OwnerId == id)
                .Include(z=>z.TracksInPlaylist)
                .Include(z=>z.Owner)
                .Include("TracksInPlaylist.Track")
                .ToList();
        }
        public List<UserPlaylist> GetAllPlaylists()
        {
            return entities
                .Include(z => z.TracksInPlaylist)
                .Include(z => z.Owner)
                .Include("TracksInPlaylist.Track")
                .ToList();
        }
        public UserPlaylist GetDetailsForPlaylist(BaseEntity id)
        {
            return entities
                .Include(z => z.TracksInPlaylist)
                    .ThenInclude(z=>z.Track)
                    .ThenInclude(z=>z.Album)
                    .ThenInclude(z=>z.Artist)
                .Include(z => z.Owner)
                .Include("TracksInPlaylist.Track")
                .SingleOrDefaultAsync(z => z.Id == id.Id).Result;

        }

        public void Insert(UserPlaylist entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(UserPlaylist entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    };
}
