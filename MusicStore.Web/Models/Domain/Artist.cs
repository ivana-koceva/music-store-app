namespace MusicStore.Web.Models.Domain
{
    public class Artist : BaseEntity
    {
        public string ArtistName { get; set; }
        public string ArtistDescription { get; set; }
        public string ArtistImage { get; set; }
        public ICollection<Album> Albums { get; set; }
        public ICollection<Track> Tracks { get; set; }
    }
}
