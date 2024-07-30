using MusicStore.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repository.Interface
{
    public interface IPlaylistRepository
    {
        List<UserPlaylist> GetAllPlaylists();
        UserPlaylist GetDetailsForPlaylist(BaseEntity id);
    }
}
