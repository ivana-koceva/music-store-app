using Microsoft.AspNetCore.Identity;

namespace MusicStoreAdminApp.Models
{
    public class MusicStoreUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public ICollection<UserPlaylist?> UserPlaylists { get; set; }
    }
}
