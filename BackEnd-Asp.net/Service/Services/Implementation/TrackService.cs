using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Repository;
using Repository.Contract;
using Service.Services.Contract;

namespace Service.Services.Implementation
{
    public class TrackService: ITrackService
    {
        private readonly ITrackRepository repo;

        public TrackService(ITrackRepository _repo)
        {
            this.repo = _repo;
        }

        public IEnumerable<Track> GetAllTracks()
        {
            return repo.GetAll();
        }

        public Track GetTrackById(int id)
        {
            return repo.GetById(id);
        }

        public Track GetTrackByName(string name)
        {
            return repo.GetByName(name);
        }

        public Track AddTrack(Track addedTrack)
        {
            try
            {
                repo.Add(addedTrack);
                repo.Save();
                return addedTrack;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string RemoveTrackById(int id)
        {
            try
            {
                repo.RemoveById(id);
                repo.Save();
                return "Track added Successfully";
            }
            catch(Exception ex)
            {
                return "Failed to be removed";
            }
        }
        

        
        public string UpdateTrackById(int id, Track updatedTrack)
        {
            try
            {
                repo.UpdateById(id, updatedTrack);
                repo.Save();
                return "Track updated successfully";
            }
            catch( Exception ex)
            {
                return "Failed to be updated";
            }
        }
    }
}
