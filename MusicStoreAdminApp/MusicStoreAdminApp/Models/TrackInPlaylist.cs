namespace MusicStoreAdminApp.Models
{
    public class TrackInPlaylist 
    {
        public Guid TrackId { get; set; }
        public Track Track { get; set; }
        public Guid UserPlaylistId { get; set; }
        public UserPlaylist UserPlaylist { get; set; }
    }
}
