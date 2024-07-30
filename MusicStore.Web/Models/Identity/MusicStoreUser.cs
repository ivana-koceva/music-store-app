using Microsoft.AspNetCore.Identity;
using MusicStore.Web.Models.Domain;

namespace MusicStore.Web.Models.Identity
{
    public class MusicStoreUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public ICollection<UserPlaylist?> UserPlaylists { get; set; }
    }
}
