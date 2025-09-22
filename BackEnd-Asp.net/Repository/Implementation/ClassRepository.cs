using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Repository.Contract;

namespace Repository.Implementation
{
    public class ClassRepository: IClassRepository
    {
        private readonly AppDbContext context;

        public ClassRepository(AppDbContext _context)
        {
            context = _context;
        }
        public void Add(Class addedClass)
        {          
            context.Classes.Add(addedClass);
        }

        public IEnumerable<Class> GetAll()
        {

            return context.Classes.ToList();
        }

        public Class GetById(int id)
        {

            return context.Classes.Find(id);

        }

        public Class GetByName(string name)
        {
            return context.Classes.SingleOrDefault(c => c.ClassName == name);
        }

        public void RemoveById(int id)
        {
            var removedClass = context.Classes.Find(id);
            if (removedClass != null)
            {
                context.Classes.Remove(removedClass);
            }
        }

        public void UpdateById(int id, Class updatedClass)
        {
            var upClass = context.Classes.Find(id);

            if (upClass != null)
            {
                    upClass.ClassName = updatedClass.ClassName;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        
    }
}
