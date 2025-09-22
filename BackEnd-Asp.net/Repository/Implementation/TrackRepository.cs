using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Repository.Contract;

namespace Repository.Implementation
{
    public class TrackRepository: ITrackRepository
    {
        private readonly AppDbContext context;

        public TrackRepository(AppDbContext _context)
        {
            context = _context;
        }
        public void Add(Track addedTrack)
        {
            context.Tracks.Add(addedTrack);
        }

        public IEnumerable<Track> GetAll()
        {

            return context.Tracks.ToList();
        }

        public Track GetById(int id)
        {

            return context.Tracks.Find(id);

        }

        public Track GetByName(string name)
        {
            return context.Tracks.SingleOrDefault(c => c.TrackName == name);
        }

        public void RemoveById(int id)
        {
            var removedTrack = context.Tracks.Find(id);
            if (removedTrack != null)
            {
                context.Tracks.Remove(removedTrack);
            }
        }

        public void UpdateById(int id, Track updatedTrack)
        {
            var upTrack = context.Tracks.Find(id);

            if (upTrack != null)
            {
                upTrack.TrackName = updatedTrack.TrackName;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
