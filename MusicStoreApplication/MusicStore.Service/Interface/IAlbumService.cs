using MusicStore.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service.Interface
{
    public interface IAlbumService
    {
        List<Album> GetAllAlbums();
        Album GetDetailsForAlbum(Guid? id);
        void CreateNewAlbum(Album p);
        void UpdateExistingAlbum(Album p);

        void DeleteAlbum(Guid id);
    }
}
