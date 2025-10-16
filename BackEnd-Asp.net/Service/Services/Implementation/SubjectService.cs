using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Models;
using Repository;
using Repository.Contract;
using Service.Services.Contract;

namespace Service.Services.Implementation
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository repo;

        public SubjectService(ISubjectRepository _repo)
        {
            this.repo = _repo;
        }

        public List<SubjectWithUnits> GetAllSubjects()
        {
            return repo.GetAll();
        }

        public Subject GetSubjectById(int id)
        {
            return repo.GetById(id);
        }

        public Subject GetSubjectByName(string name)
        {
            return repo.GetByName(name);
        }

        public CreatedSubjectDTO AddSubject(CreatedSubjectDTO addedSubjectDTO)
        {
            try
            {
                repo.Add(addedSubjectDTO);
                //repo.Save();
                return addedSubjectDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string RemoveSubjectById(int id)
        {
            try
            {
                repo.RemoveById(id);
                repo.Save();
                return "Subject added Successfully";
            }
            catch (Exception ex)
            {
                return "Failed to be removed";
            }
        }



        public string UpdateSubjectById(int id, CreatedSubjectDTO updatedSubjectDTO)
        {
            try
            {
                repo.UpdateById(id, updatedSubjectDTO);
                repo.Save();
                return "Subject updated successfully";
            }
            catch (Exception ex)
            {
                return $"Failed to be updated: {ex.Message}";
            }
        }



    }



}