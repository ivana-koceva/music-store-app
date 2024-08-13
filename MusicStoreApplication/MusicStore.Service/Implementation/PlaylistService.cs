using Microsoft.Extensions.Options;
using MusicStore.Domain;
using MusicStore.Repository.Implementation;
using MusicStore.Repository.Interface;
using MusicStore.Service.Interface;
using MusicStore.Web.Models.Domain;
using Stripe;
using Stripe.Checkout;
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
        private readonly IUserRepository _userRepository;
        private readonly StripeSettings _stripeSettings;

        public PlaylistService(IPlaylistRepository playlistRepository, IUserRepository userRepository, IOptions<StripeSettings> stripeSettings)
        {
            _playlistRepository = playlistRepository;
            _userRepository = userRepository;
            _stripeSettings = stripeSettings.Value;
        }

        public bool AddToPlaylistConfirmed(TrackInPlaylist model, string userId)
        {
            var loggedInUser = _userRepository.Get(userId);
            var playlists = _playlistRepository.GetAllPlaylists(userId);

            var userPlaylist = playlists.FirstOrDefault(z => z.Id == model.UserPlaylistId);
                //.FirstOrDefault(z=>z.Id == model.UserPlaylistId);
            

            if (userPlaylist.TracksInPlaylist == null)
                userPlaylist.TracksInPlaylist = new List<TrackInPlaylist>(); ;

            userPlaylist.TracksInPlaylist.Add(model);
            _playlistRepository.Update(userPlaylist);
            return true;
        }

        public void ChangePurchaseStatus(Guid id)
        {
            var playlist = _playlistRepository.Get(id);
            playlist.isPurchased = true;
            _playlistRepository.Update(playlist);
        }

        public void CreateNewPlaylist(UserPlaylist p)
        {
            _playlistRepository.Insert(p);
        }

        public void DeletePlaylist(Guid id)
        {
            var playlist = _playlistRepository.Get(id);
            _playlistRepository.Delete(playlist);
        }

        public List<UserPlaylist> GetAllPlaylists(string? id)
        {
            return _playlistRepository.GetAllPlaylists(id);
        }
        public List<UserPlaylist> GetAllPlaylists()
        {
            return _playlistRepository.GetAllPlaylists();
        }
        public UserPlaylist GetDetailsForPlaylist(BaseEntity id)
        {
            return _playlistRepository.GetDetailsForPlaylist(id);
        }

        public void UpdateExistingPlaylist(UserPlaylist p)
        {
            _playlistRepository.Update(p);
        }

    }
}
