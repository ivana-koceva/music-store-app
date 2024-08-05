using MusicStore.Web.Models.Domain;
using MusicStore.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repository.Interface
{
    public interface IPlaylistRepository
    {
        List<UserPlaylist> GetAllPlaylists(string? id);
        UserPlaylist GetDetailsForPlaylist(BaseEntity id);
        void Insert(UserPlaylist entity);
        void Update(UserPlaylist entity);
        void Delete(UserPlaylist entity);
        UserPlaylist Get(Guid? id);
    }
}
