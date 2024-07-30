using MusicStore.Repository.Interface;
using MusicStore.Service.Interface;
using MusicStore.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service.Implementation
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistService(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public List<UserPlaylist> GetAllPlaylists()
        {
            return _playlistRepository.GetAllPlaylists().ToList();
        }

        public UserPlaylist GetDetailsForPlaylist(BaseEntity id)
        {
            return _playlistRepository.GetDetailsForPlaylist(id);
        }
    }
}
