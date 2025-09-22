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
    public class ClassService : IClassService
    {

        private readonly IClassRepository repo;

        public ClassService(IClassRepository _repo)
        {
            this.repo = _repo;
        }

        public IEnumerable<Class> GetAllClasses()
        {
            return repo.GetAll();
        }

        public Class GetClassById(int id)
        {
            return repo.GetById(id);
        }

        public Class GetClassByName(string name)
        {
            return repo.GetByName(name);
        }

        public Class AddClass(Class addedClass)
        {
            try
            {
                repo.Add(addedClass);
                repo.Save();
                return addedClass;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string RemoveClassById(int id)
        {
            try
            {
                repo.RemoveById(id);
                repo.Save();
                return "Class added Successfully";
            }
            catch(Exception ex)
            {
                return "Failed to be removed";
            }
        }
        

        
        public string UpdateClassById(int id, Class updatedClass)
        {
            try
            {
                repo.UpdateById(id, updatedClass);
                repo.Save();
                return "Class updated successfully";
            }
            catch( Exception ex)
            {
                return "Failed to be updated";
            }
        }

    }
}
