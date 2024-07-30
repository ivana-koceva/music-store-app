namespace MusicStore.Web.Models.Domain
{
    public class Album : BaseEntity
    {
        public string AlbumName { get; set; }
        public string AlbumImage { get; set; }
        public Artist Artist { get; set; }
        public string Genre { get; set; }
        public int Rating { get; set; }
        public ICollection<Track> Tracks { get; set; }
    }
}
