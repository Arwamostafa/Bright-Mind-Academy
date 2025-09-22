using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Service.Services.Contract
{
    public interface ITrackService
    {
        IEnumerable<Track> GetAllTracks();

        Track GetTrackById(int id);

        Track GetTrackByName(string name);

        Track AddTrack(Track addedTrack);

        string RemoveTrackById(int id);
        string UpdateTrackById(int id, Track updatedTrack);
    }
}
