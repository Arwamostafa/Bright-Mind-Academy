using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Repository.Contract
{
    public interface IClassRepository
    {
        IEnumerable<Class> GetAll();

        Class GetById(int id);

        Class GetByName(string name);

        void Add(Class addedClass);

        void RemoveById(int id);
        void UpdateById(int id, Class updatedClass);

        void Save();
    }
}
