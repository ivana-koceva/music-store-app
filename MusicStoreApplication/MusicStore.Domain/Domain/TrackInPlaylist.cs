namespace MusicStore.Web.Models.Domain
{
    public class TrackInPlaylist : BaseEntity
    {
        public Guid TrackId { get; set; }
        public Track Track { get; set; }
        public Guid UserPlaylistId { get; set; }
        public UserPlaylist UserPlaylist { get; set; }
    }
}
