namespace MusicStore.Web.Models.Domain
{
    public class Track : BaseEntity
    {
        public string TrackName { get; set; }
        public int Rating { get; set; }
        public Album Album { get; set; }
    }
}
