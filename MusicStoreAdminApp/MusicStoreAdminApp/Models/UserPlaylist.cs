
namespace MusicStoreAdminApp.Models
{
    public class UserPlaylist 
    {
        public Guid id { get; set; }
        public string OwnerId { get; set; }
        public MusicStoreUser Owner { get; set; }
        public virtual ICollection<TrackInPlaylist> TracksInPlaylist { get; set; }
    }
}
