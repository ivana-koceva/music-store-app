namespace MusicStoreAdminApp.Models
{
    public class Track
    {
        public string TrackName { get; set; }
        public int Rating { get; set; }
        public Album Album { get; set; }
    }
}
