using MusicStore.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain.DTO
{
    public class AddToPlaylistDTO
    {
        public Guid TrackId { get; set; }
        public string TrackName { get; set; }
        public List<UserPlaylist> UserPlaylists { get; set; }
        public Guid SelectedPlaylistId { get; set; }
    }
}
