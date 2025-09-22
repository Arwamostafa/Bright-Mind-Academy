using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Repository.Contract
{
    public interface ITrackRepository
    {
        IEnumerable<Track> GetAll();

        Track GetById(int id);

        Track GetByName(string name);

        void Add(Track addedTrack);

        void RemoveById(int id);
        void UpdateById(int id, Track updatedTrack);

        void Save();
    }
}
