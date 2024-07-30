using MusicStore.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service.Interface
{
    public interface ITrackService
    {
        List<Track> GetAllTracks();
        Track GetDetailsForTrack(Guid? id);
        void CreateNewTrack(Track p);
        void UpdateExistingTrack(Track p);

        void DeleteTrack(Guid id);
    }
}
