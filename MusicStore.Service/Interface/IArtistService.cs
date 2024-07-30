using MusicStore.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service.Interface
{
    public interface IArtistService
    {
        List<Artist> GetAllArtists();
        Artist GetDetailsForArtist(Guid? id);
        void CreateNewArtist(Artist p);
        void UpdateExistingArtist(Artist p);

        void DeleteArtist(Guid id);
    }
}
