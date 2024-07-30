using MusicStore.Web.Models.Identity;

namespace MusicStore.Web.Models.Domain
{
    public class UserPlaylist : BaseEntity
    {
        public string OwnerId { get; set; }
        public MusicStoreUser Owner { get; set; }
        public virtual ICollection<TrackInPlaylist> TracksInPlaylist { get; set; }
    }
}
