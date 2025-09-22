using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Service.Services.Contract
{
    public interface IClassService
    {
        IEnumerable<Class> GetAllClasses();

        Class GetClassById(int id);

        Class GetClassByName(string name);

        Class AddClass(Class addedClass);

        string RemoveClassById(int id);
        string UpdateClassById(int id, Class updatedClass);
    }
}
