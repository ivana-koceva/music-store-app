namespace MusicStoreAdminApp.Models
{
    public class Album
    {
        public string AlbumName { get; set; }
        public string AlbumImage { get; set; }
        public Artist Artist { get; set; }
        public string Genre { get; set; }
        public int Rating { get; set; }
        public ICollection<Track> Tracks { get; set; }
    }
}
