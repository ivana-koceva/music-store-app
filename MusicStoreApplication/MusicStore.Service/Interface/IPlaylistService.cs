using MusicStore.Domain.DTO;
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
        List<UserPlaylist> GetAllPlaylists(string? id);
        List<UserPlaylist> GetAllPlaylists();
        UserPlaylist GetDetailsForPlaylist(BaseEntity id);
        bool AddToPlaylistConfirmed(TrackInPlaylist model, string userId);
        void CreateNewPlaylist(UserPlaylist p);
        void UpdateExistingPlaylist(UserPlaylist p);

        void DeletePlaylist(Guid id);
        void ChangePurchaseStatus(Guid id);
    }
}
