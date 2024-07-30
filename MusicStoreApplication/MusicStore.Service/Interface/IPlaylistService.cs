using MusicStore.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service.Interface
{
    public interface IPlaylistService
    {
        List<UserPlaylist> GetAllPlaylists();
        UserPlaylist GetDetailsForPlaylist(BaseEntity id);
    }
}
